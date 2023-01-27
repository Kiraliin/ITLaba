using DataBase;
using DataBase.Repositories;
using domain.models;

namespace UnitTests;

public class DBtest
{
    private readonly ApplicationContextFactory _dbFactory;

    public DBtest()
    {
        _dbFactory = new ApplicationContextFactory();
    }

    [Fact]
    public async Task UserRepositoryCreate()
    {
        var context = _dbFactory.CreateDbContext();
        var repo = new UserRepository(context);
        var user = new User("Alex", "123", 1, "+79241669106", "Alexey Pak", Role.Patient);
        await repo.Create(user);

        Assert.True(await repo.ExistLogin(user.Username));
        await repo.Delete(user.Id);
    }

    [Fact]
    public async Task UserRepositoryNotExists()
    {
        var context = _dbFactory.CreateDbContext();
        var repo = new UserRepository(context);
        var user = new User("Alex", "123", 1, "+79241669106", "Alexey Pak", Role.Patient);
        await repo.Create(user);

        Assert.False(await repo.ExistLogin("Lana"));
        await repo.Delete(user.Id);

    }

    [Fact]
    public async Task UserRepositoryPgTest()
    {

        var context = _dbFactory.CreateDbContext();
        var repo = new UserRepository(context);

        var assertList = new List<User>();
        var user = new User("Alex", "123", 1, "+79241669106", "Alexey Pak", Role.Patient);
        assertList.Add(user);
        await repo.Create(user);
        user = new User("Dima", "123", 2, "+79241669106", "Dima Pak", Role.Patient);
        assertList.Add(user);
        await repo.Create(user);


        var testList = (await repo.List()).ToList();
        for (var i = 0; i < assertList.Count; ++i)
        {
            Assert.Equal(testList[i].Id, assertList[i].Id);
            Assert.Equal(testList[i].FullName, assertList[i].FullName);
            await repo.Delete(testList[i].Id);
        }
    }

    //[Fact]
    public async Task DoctorRepositoryPgTest()
    {
        // Write here any test
        var context = _dbFactory.CreateDbContext();
        var repo = new DoctorRepository(context);
        var spec = new Specialization(1, "Dentist");
        await repo.Create(new Doctor(1, "Alex", spec));
        var list = (await repo.GetBySpec(spec)).ToList();
        Assert.Equal(list[0].Id, 1);
    }

    [Fact]
    public async Task SchenduleRepositoryPgTest()
    {
        var context = _dbFactory.CreateDbContext();
        var repo = new SchenduleRepository(context);
        var schendule = new Schendule(1, 1,
            new DateTime(2022, 12, 15, 15, 0, 0, 0),
            new DateTime(2022, 12, 15, 15, 30, 0, 0));

        await repo.Create(schendule);

        var spec = new Specialization(1, "Proktolog");
        var test = repo.GetSchenduleByDate(new Doctor(1, "Vasua", spec), new DateOnly(2022, 12, 15)).Result.ToList()[0];

        Assert.True(test.Id == schendule.Id && test.DoctorId == schendule.DoctorId);

        await repo.Delete(schendule.Id);
    }
}
