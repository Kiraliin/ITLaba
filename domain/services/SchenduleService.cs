using domain.models;

namespace domain.services;

public class SchenduleService
{
    private ISchenduleRepository _repository;
    private IDoctorRepository _doctorRepository;

    public SchenduleService(ISchenduleRepository repo, IDoctorRepository doctorRepo)
    {
        _repository = repo;
        _doctorRepository = doctorRepo;
    }

    public async Task<Result<IEnumerable<Schendule>>> GetByDoctor(Doctor doctor, DateOnly date)
    {
        if (!await _doctorRepository.Exists(doctor.Id))
            return Result.Fail<IEnumerable<Schendule>>("Doctor doesn't exists");
        if (!_doctorRepository.IsValid(doctor))
            return Result.Fail<IEnumerable<Schendule>>("Doctor is not invalid");

        return Result.Ok<IEnumerable<Schendule>>(await _repository.GetSchenduleByDate(doctor, date));
    }

    public async Task<Result<Schendule>> Add(Schendule schendule)
    {
        if (!await _doctorRepository.Exists(schendule.DoctorId))
            return Result.Fail<Schendule>("Doctor doesn't exists");

        if (await _repository.Exists(schendule.Id))
            return Result.Fail<Schendule>("Schendule already exists");

        await _repository.Create(schendule);
        return Result.Ok<Schendule>(schendule);
    }

    public async Task<Result<Schendule>> Update(Schendule schendule)
    {
        if (!await _repository.Exists(schendule.Id))
            return Result.Fail<Schendule>("Schendule Doesn't exists");

        await _repository.Update(schendule);
        return Result.Ok<Schendule>(schendule);
    }

    public async Task<Result<Schendule>> Delete(Schendule schendule)
    {
        if (!await _repository.Exists(schendule.Id))
            return Result.Fail<Schendule>("Schendule Doesn't exists");

        await _repository.Delete(schendule.Id);
        return Result.Ok<Schendule>(schendule);
    }
}