using APBD_zajecia11.DAL;
using APBD_zajecia11.DTO;
using APBD_zajecia11.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Exception = System.Exception;
using Patient = APBD_zajecia11.Models.Patient;

namespace APBD_zajecia11.Services.Prescription;

public class PrescriptionService : IPrescriptionService
{
    private readonly DatabaseContext _databaseContext;
    public PrescriptionService(DatabaseContext context)
    {
        _databaseContext = context;
    }

    public async Task<int> Add(AddPrescriptionDTO addPrescriptionDto, CancellationToken cancellationToken)
    {
        await using var transaction = await _databaseContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            ValidatePrescriptionData(addPrescriptionDto);
            var patient = await GetOrCreatePatientAsync(addPrescriptionDto.PatientDto, cancellationToken);
            await ValidateMedicamentsExistAsync(addPrescriptionDto.Medicaments, cancellationToken);
            var prescription = await CreatePrescriptionAsync(addPrescriptionDto, patient.IdPatient, cancellationToken);
            await AddPrescriptionMedicamentsAsync(addPrescriptionDto.Medicaments, prescription.IdPrescription, cancellationToken);
            await _databaseContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return prescription.IdPrescription;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    private static void ValidatePrescriptionData(AddPrescriptionDTO addPrescriptionDto)
    {
        if (addPrescriptionDto.Medicaments.Count > 10)
        {
            throw new ArgumentException("Medicament count is too big!");
        }

        if (addPrescriptionDto.DueDate < addPrescriptionDto.Date)
        {
            throw new ArgumentException("Due date has passed!");
        }
    }

    private async Task<Models.Patient> GetOrCreatePatientAsync(DTO.PatientDTO dtoPatientDto, CancellationToken cancellationToken)
    {
        var doesPatientExist = _databaseContext.Patients.AnyAsync(patient => patient.IdPatient == dtoPatientDto.IdPatient, cancellationToken);
        var newPatient = new Models.Patient()
        {
            FirstName = dtoPatientDto.FirstName,
            LastName = dtoPatientDto.LastName,
            BirthDate = dtoPatientDto.BirthDate,
        };
        
        if (! await doesPatientExist)
        {
            await _databaseContext.Patients.AddAsync(newPatient, cancellationToken);
            await _databaseContext.SaveChangesAsync(cancellationToken); // uzywamy save changes, aby baza danych wygenerowala ID dla patient
        }
        else
        {
            newPatient.IdPatient = dtoPatientDto.IdPatient;
        }
        return newPatient;
    }

    private async Task ValidateMedicamentsExistAsync(ICollection<DTO.MedicamentDTO> medicaments, CancellationToken cancellationToken)
    {
        foreach (var medicament in medicaments)
        {
            var doesMedicationExist = _databaseContext.Medicaments.AnyAsync(med => med.IdMedicament == medicament.IdMedicament, cancellationToken);
            if (! await doesMedicationExist)
            {
                throw new ArgumentException("Medicament doesn't exist!");
            }
        }
    }

    private async Task<Models.Prescription> CreatePrescriptionAsync(AddPrescriptionDTO addPrescriptionDto, int idPatient, CancellationToken cancellationToken)
    {
        var prescription = new Models.Prescription()
        {
            Date = addPrescriptionDto.DueDate,
            DueDate = addPrescriptionDto.DueDate,
            IdPatient = idPatient,
            IdDoctor = addPrescriptionDto.IdDoctor
        };

        await _databaseContext.Prescriptions.AddAsync(prescription, cancellationToken);
        await _databaseContext.SaveChangesAsync(cancellationToken); // uzywamy save changes, aby baza danych wygenerowala ID dla prescription

        return prescription;
    }

    private async Task AddPrescriptionMedicamentsAsync(ICollection<DTO.MedicamentDTO> medicaments, int idPrescription, CancellationToken cancellationToken)
    {
        foreach (var medicament in medicaments)
        {
            await _databaseContext.PrescriptionMedicaments.AddAsync(new Prescription_Medicament()
            {
                IdMedicament = medicament.IdMedicament,
                IdPrescription = idPrescription,
                Dose = medicament.Dose,
                Details = medicament.Details
            }, cancellationToken);
        }
    }
}
