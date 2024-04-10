using LegacyApp;

namespace LegacyAppTests;

public class UserServiceTest
{
    //test ogolny
    [Fact]
    public void AddUserTest()
    {
        string firstName = "John";
        string lastName = "Doe";
        DateTime birthName = new DateTime(1980, 1, 1);
        int clientId = 1;
        string email= "doe";
        var service = new UserService();
        //act
        bool result = service.AddUser(firstName, lastName, email, birthName, clientId);
        
        //asssert
        Assert.Equal(false,result);
    }
    //testowanie email 
    [Fact]
    public void IsValidEmail_WithValidEmail_ReturnsTrue()
    {
        // Arrange
        var userService = new UserService();

        // Act
        var result = userService.IsValidEmail("johndoe@gmail.com");

        // Assert
        Assert.True(result);
    }
    [Fact]
    public void IsInvalidEmailReturnsFalse()
    {
        var userService = new UserService();

        // Act
        var result = userService.IsValidEmail("johndoe!gmail,com");
        //assert
        Assert.True(result);
    }
    
    //testowanie rocznika
    [Fact]
    public void IsOver21YearsOld_ReturnsTrue()
    {
        // Arrange
        var userService = new UserService();

        
        var dateOfBirth = DateTime.Now.AddYears(-23);

        // Act
        var result = userService.IsOver21YearsOld(dateOfBirth);

        // Assert
        Assert.True(result);
    }
    [Fact]
    public void IsOver21YearsOld_ReturnsFalse()
    {
        // Arrange
        var userService = new UserService();
        var dateOfBirth = DateTime.Now.AddYears(-20);

        // Act
        var result = userService.IsOver21YearsOld(dateOfBirth);

        // Assert
        Assert.False(result);
    }
}
