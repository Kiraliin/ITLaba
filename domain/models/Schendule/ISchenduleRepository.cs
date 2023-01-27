namespace domain.models;

public interface ISchenduleRepository : IRepository<Schendule>
{
    Task<IEnumerable<Schendule>> GetSchenduleByDate(Doctor doctor, DateOnly date);
}