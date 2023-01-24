namespace domain.models;

public interface ISchenduleRepository : IRepository<Schendule>
{
    IEnumerable<Schendule> GetSchenduleByDate(Doctor doctor, DateOnly date);
}
