using DataBase.Converters;
using domain.models;

namespace DataBase.Repositories;

public class SchenduleRepository : ISchenduleRepository
{
    private readonly ApplicationContext _context;

    public SchenduleRepository(ApplicationContext context)
    {
        _context = context;
    }


    public Schendule Create(Schendule item)
    {
        _context.Schendules.Add(item.ToModel());
        return item;
    }

    public Schendule? Get(int id)
    {
        return _context.Schendules.FirstOrDefault(s => s.Id == id).ToDomain();
    }
    public IEnumerable<Schendule> List()
    {
        return _context.Schendules.Select(schenduleModel => schenduleModel.ToDomain()).ToList();
    }

    public bool Exists(int id)
    {
        return _context.Schendules.Any(s => s.Id == id);
    }

    public bool Delete(int id)
    {
        var schendule = _context.Schendules.FirstOrDefault(s => s.Id == id);
        if (schendule == default)
            return false; // not deleted
        _context.Schendules.Remove(schendule);
        return true;
    }

    public bool IsValid(Schendule entity)
    {
        if (entity.Id < 0)
            return false;

        if (entity.StartTime >= entity.EndTime)
            return false;

        return true;
    }

    public Schendule Update(Schendule entity)
    {
        _context.Schendules.Update(entity.ToModel());
        return entity;
    }

    public IEnumerable<Schendule> GetSchenduleByDate(Doctor doctor, DateOnly date)
    {
        return _context.Schendules.Where(s =>
                     s.DoctorId == doctor.Id &&
                     s.StartTime.Date == date.ToDateTime(new TimeOnly())) // Cast DateOnly -> DateTime
            .Select(s => s.ToDomain());                                   // with time - 00:00
    }
}