using domain.models;
using Moq;
namespace UnitTests;

public class UserServiceTests
{
    private readonly UserService _userService;
    private readonly Mock<IUserRepository> _repository;

    public UserServiceTests()
    {
        _repository = new Mock<IUserRepository>();
        _userService = new UserService(_repository.Object);
    }

    public User GetUser(string username)
    {
        return new User(username, "123", 1, "+79241669106", "Cool", Role.Patient);
    }


    [Fact]
    public void LoginIsEmptyOrNull()
    {
        var response = _userService.GetByLogin(string.Empty);
        Assert.False(response.Success);
        Assert.Equal("Empty login", response.Error);
    }

    [Fact]
    public void LoginNotFound()
    {
        _repository.Setup(repo => repo.GetByLogin(It.IsAny<string>()))
            .Returns(() => null);

        var response = _userService.GetByLogin("mikhailova");

        Assert.False(response.Success);
        Assert.Equal("User with this login doesn't exists", response.Error);
    }
    [Fact]
    public void LoginFound()
    {
        _repository.Setup(repo => repo.ExistLogin(It.Is<string>(s => s == "mikhailova")))
            .Returns(true);
        _repository.Setup(repo => repo.GetByLogin(It.Is<string>(s => s == "mikhailova")))
            .Returns(GetUser("mikhailova"));

        var response = _userService.GetByLogin("mikhailova");

        Assert.True(response.Success);
    }

    [Fact]
    public void CreateAlreadyExists()
    {
        _repository.Setup(repo => repo.ExistLogin(It.Is<string>(s => s == "mikhailova"))) // id
            .Returns(true);

        _repository.Setup(repo => repo.IsValid(It.IsAny<User>()))
            .Returns(true);

        var response = _userService.CreateUser(GetUser("mikhailova"));

        Assert.False(response.Success);
        Assert.Equal("User with that username already exists", response.Error);
    }

    [Fact]
    public void CreateEmptyUsername()
    {
        _repository.Setup(repo => repo.IsValid(It.Is<User>(user => string.IsNullOrEmpty(user.Username))))
            .Returns(false);

        var response = _userService.CreateUser(GetUser(""));

        Assert.False(response.Success);
        Assert.Equal("User data is not valid", response.Error);
    }

    [Fact]
    public void CreateOk()
    {
        _repository.Setup(repo => repo.ExistLogin(It.IsAny<string>()))
            .Returns(false);
        _repository.Setup(repo => repo.IsValid(It.IsAny<User>()))
            .Returns(true);

        var response = _userService.CreateUser(GetUser("mikhailova"));

        Assert.True(response.Success);
    }

    [Fact]
    public void CheckExistEmptyLogin()
    {
        var response = _userService.CheckExist("", "password");
        Assert.False(response.Success);
        Assert.Equal("Empty login", response.Error);

    }

    [Fact]
    public void CheckExistEmptyPassword()
    {
        var response = _userService.CheckExist("login", "");
        Assert.False(response.Success);
        Assert.Equal("Empty password", response.Error);
    }

    [Fact]
    public void CheckExistLoginPasswordOk()
    {
        _repository.Setup(repo => repo.ExistLogin(
                It.Is<string>(u => u == "mikhailova"),
                It.Is<string>(p => p == "123")
            )
        ).Returns(true);

        var response = _userService.CheckExist("mikhailova", "123");

        Assert.True(response.Success);
    }






}