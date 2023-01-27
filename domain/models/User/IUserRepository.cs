namespace domain.models;
public interface IUserRepository : IRepository<User>
{
    Task<bool> ExistLogin(string login, string password);
    Task<bool> ExistLogin(string login);
    Task<User?> GetByLogin(string login);

}