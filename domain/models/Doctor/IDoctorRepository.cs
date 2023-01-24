namespace domain.models;

public interface IDoctorRepository : IRepository<Doctor>
{
    public IEnumerable<Doctor> GetBySpec(Specialization spec);
}
