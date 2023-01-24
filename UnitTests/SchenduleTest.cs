namespace UnitTests;

using domain.models;

using Moq;
public class SchenduleServiceTests
{
    private readonly SchenduleService _schenduleService;
    private readonly Mock<IDoctorRepository> _doctorRepository;
    private readonly Mock<ISchenduleRepository> _schenduleRepository;

    public SchenduleServiceTests()
    {
        _doctorRepository = new Mock<IDoctorRepository>();
        _schenduleRepository = new Mock<ISchenduleRepository>();
        _schenduleService = new SchenduleService(_schenduleRepository.Object, _doctorRepository.Object);
    }

    Doctor GetDoctor()
    {
        return new Doctor(1, "Er", new Specialization(1, "dentist"));
    }

    Schendule GetSchendule()
    {
        return new Schendule(1, 1, new DateTime(2002, 04, 26, 16, 40, 0), new DateTime(2002, 04, 26, 16, 40, 0));
    }

    [Fact]

    public void GetByDoctorNotExist()
    {
        _doctorRepository.Setup(repo => repo.Exists(It.Is<int>(id => id == 1)))
            .Returns(false);

        _doctorRepository.Setup(repo => repo.IsValid(It.IsAny<Doctor>()))
            .Returns(true);

        var res = _schenduleService.GetByDoctor(GetDoctor(), new DateOnly());

        Assert.False(res.Success);
        Assert.Equal("Doctor doesn't exists", res.Error);
    }

    [Fact]
    public void GetByDoctorNotValid()
    {
        _doctorRepository.Setup(repo => repo.Exists(It.Is<int>(id => id == 1)))
            .Returns(true);

        _doctorRepository.Setup(repo => repo.IsValid(It.IsAny<Doctor>()))
            .Returns(false);

        var res = _schenduleService.GetByDoctor(GetDoctor(), new DateOnly());

        Assert.False(res.Success);
        Assert.Equal("Doctor is not invalid", res.Error);
    }

    [Fact]
    public void GetByDoctorOk()
    {
        _doctorRepository.Setup(repo => repo.Exists(It.Is<int>(id => id == 1)))
            .Returns(true);

        _doctorRepository.Setup(repo => repo.IsValid(It.IsAny<Doctor>()))
            .Returns(true);

        var res = _schenduleService.GetByDoctor(GetDoctor(), new DateOnly());

        Assert.True(res.Success);
    }

    [Fact]
    public void AddSchenduleDoctorIsNotExists()
    {
        _doctorRepository.Setup(repo => repo.Exists(It.Is<int>(id => id == 1)))
            .Returns(false);
        _doctorRepository.Setup(repo => repo.IsValid(It.IsAny<Doctor>()))
            .Returns(true);

        var res = _schenduleService.Add(GetSchendule());

        Assert.False(res.Success);
        Assert.Equal("Doctor doesn't exists", res.Error);

    }

    [Fact]
    public void AddSchenduleAlreadyExists()
    {
        _doctorRepository.Setup(repo => repo.Exists(It.Is<int>(id => id == 1)))
            .Returns(true);

        _schenduleRepository.Setup(repo => repo.Exists(It.Is<int>(id => id == 1)))
            .Returns(true);

        var res = _schenduleService.Add(GetSchendule());

        Assert.False(res.Success);
        Assert.Equal("Schendule already exists", res.Error);
    }

    [Fact]
    public void AddSchenduleOk()
    {
        _doctorRepository.Setup(repo => repo.Exists(It.Is<int>(id => id == 1)))
            .Returns(true);

        _schenduleRepository.Setup(repo => repo.Exists(It.Is<int>(id => id == 1)))
            .Returns(false);

        var res = _schenduleService.Add(GetSchendule());
        Assert.True(res.Success);
    }

    [Fact]
    public void UpdateSchenduleIsNotExists()
    {
        _schenduleRepository.Setup(repo => repo.Exists(It.Is<int>(id => id == 1)))
            .Returns(false);

        var res = _schenduleService.Update(GetSchendule());
        Assert.False(res.Success);
        Assert.Equal("Schendule Doesn't exists", res.Error);
    }

    [Fact]
    public void UpdateSchenduleOk()
    {
        _schenduleRepository.Setup(repo => repo.Exists(It.Is<int>(id => id == 1)))
            .Returns(true);

        var res = _schenduleService.Update(GetSchendule());
        Assert.True(res.Success);
    }

    [Fact]

    public void DeleteNotExists()
    {
        _schenduleRepository.Setup(repo => repo.Exists(It.Is<int>(id => id == 1)))
            .Returns(false);

        var res = _schenduleService.Delete(GetSchendule());
        Assert.False(res.Success);
        Assert.Equal("Schendule Doesn't exists", res.Error);
    }

    [Fact]
    public void DeleteOk()
    {
        _schenduleRepository.Setup(repo => repo.Exists(It.Is<int>(id => id == 1)))
            .Returns(true);

        var res = _schenduleService.Delete(GetSchendule());
        Assert.True(res.Success);
    }

}
