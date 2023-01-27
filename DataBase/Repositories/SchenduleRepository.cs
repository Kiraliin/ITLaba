using DataBase.Converters;
using domain.models;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Repositories;

public class SchenduleRepository : ISchenduleRepository
{
    private readonly ApplicationContext _context;

    public SchenduleRepository(ApplicationContext context)
    {
        _context = context;
    }


    public async Task<Schendule> Create(Schendule item)
    {
        await _context.Schendules.AddAsync(item.ToModel());
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task<Schendule> Get(int id)
    {
        var schendule = await _context.Schendules.FirstOrDefaultAsync(s => s.Id == id);
        return schendule.ToDomain();
    }
    public async Task<IEnumerable<Schendule>> List()
    {
        return await _context.Schendules.Select(schenduleModel => schenduleModel.ToDomain()).ToListAsync();
    }

    public async Task<bool> Exists(int id)
    {
        return await _context.Schendules.AnyAsync(s => s.Id == id);
    }

    public async Task<bool> Delete(int id)
    {
        var schendule = await _context.Schendules.FirstOrDefaultAsync(s => s.Id == id);
        if (schendule == default)
            return false; // not deleted
        _context.Schendules.Remove(schendule);
        await _context.SaveChangesAsync();
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

    public async Task<Schendule> Update(Schendule entity)
    {
        _context.Schendules.Update(entity.ToModel());
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<IEnumerable<Schendule>> GetSchenduleByDate(Doctor doctor, DateOnly date)
    {
        return await _context.Schendules.Where(s =>
                     s.DoctorId == doctor.Id &&
                     s.StartTime.Date == date.ToDateTime(new TimeOnly())) 
            .Select(s => s.ToDomain()).ToListAsync();                     
    }
}