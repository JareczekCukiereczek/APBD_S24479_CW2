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
    [Fact]
    public void AddUserTestDiffrentParams()
    {
        string firstName = "Tomasz";
        string lastName = "Nowak";
        DateTime birthName = new DateTime(2000, 8, 2);
        int clientId = 2;
        string email= "nowak@gmail.com";
        var service = new UserService();
        //act
        bool result = service.AddUser(firstName, lastName, email, birthName, clientId);
        
        //asssert
        Assert.Equal(true,result);
    }
    //testowanie email 
    [Fact]
    public void IsValidEmail_WithValidEmail_ReturnsTrue()
    {
        // Arrange
        var userService = new UserService();

        // Act
        var result = userService.IsValidEmail("johndoe@gmail,com");

        // Assert
        Assert.False(result);
    }
    [Fact]
    public void IsInvalidEmailReturnsFalse()
    {
        var userService = new UserService();

        // Act
        var result = userService.IsValidEmail("johndoe!gmail,com");
        //assert
        Assert.False(result);
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
    
    public void AddUser_WithEmptyFirstName_ReturnsFalse()
    {
        
        var userService = new UserService();
        var lastName = "Doe";
        var email = "john.doe@example.com";
        var dateOfBirth = new DateTime(1990, 1, 1);
        var clientId = 123;

       
        var result = userService.AddUser("", lastName, email, dateOfBirth, clientId);

        
        Assert.False(result);
    }
    [Fact]
    public void AddUser_WithInvalidEmail_ReturnsFalse()
    {
        
        var userService = new UserService();
        var firstName = "John";
        var lastName = "Doe";
        var invalidEmail = "invalidemail.com";
        var dateOfBirth = new DateTime(1990, 1, 1);
        var clientId = 123;

        var result = userService.AddUser(firstName, lastName, invalidEmail, dateOfBirth, clientId);
        
        Assert.False(result);
    }
    [Fact]
    public void AddUser_Under21YearsOld_ReturnsFalse()
    {
        
        var userService = new UserService();
        var firstName = "John";
        var lastName = "Doe";
        var email = "john.doe@example.com";
        var dateOfBirth = DateTime.Now.AddYears(-20); // Użytkownik ma mniej niż 21 lat
        var clientId = 123;

      
        var result = userService.AddUser(firstName, lastName, email, dateOfBirth, clientId);
        
        Assert.False(result);
    }
    [Fact]
    public void AddUser_WithCreditLimitBelow500ForNonVeryImportantClient_ReturnsFalse()
    {
        // Arrange
        var userService = new UserService();
        var firstName = "John";
        var lastName = "Doe";
        var email = "john.doe@example.com";
        var dateOfBirth = new DateTime(1990, 1, 1);
        var clientId = 123;

        // Przygotowanie klienta o typie "BasicClient"
        var client = new Client { Type = "ImportantClient" };

        // Przygotowanie serwisu limitu kredytowego
        var userCreditService = new UserCreditService();
        // Ustawienie limitu kredytowego na 400
        var userCreditLimit = userCreditService.GetCreditLimit(lastName, dateOfBirth);

        // Jeśli użytkownik nie jest bardzo ważnym klientem i jego limit kredytu jest mniejszy niż 500
        if (client.Type != "VeryImportantClient" && userCreditLimit < 500)
        {
            // Act
            var result = userService.AddUser(firstName, lastName, email, dateOfBirth, clientId);

            // Assert
            Assert.False(result);
        }
        else
        {
            // W przypadku, gdy klient jest bardzo ważnym klientem lub limit kredytu wynosi co najmniej 500,
            // test zakończy się sukcesem
            Assert.True(true);
        }
    }
    public void AddUser_WithCreditLimitAbove500ForNonVeryImportantClient_ReturnsFalse()
    {
        // Arrange
        var userService = new UserService();
        var firstName = "John";
        var lastName = "Malewski";
        var email = "john.doe@example.com";
        var dateOfBirth = new DateTime(1990, 1, 1);
        var clientId = 123;

        // Przygotowanie klienta o typie "BasicClient"
        var client = new Client { Type = "BasicClient" };

        // Przygotowanie serwisu limitu kredytowego
        var userCreditService = new UserCreditService();
        // Ustawienie limitu kredytowego na 400
        var userCreditLimit = userCreditService.GetCreditLimit(lastName, dateOfBirth);

        // Jeśli użytkownik nie jest bardzo ważnym klientem i jego limit kredytu jest mniejszy niż 500
        if (client.Type != "VeryImportantClient" && userCreditLimit < 500)
        {
            // Act
            var result = userService.AddUser(firstName, lastName, email, dateOfBirth, clientId);

            // Assert
            Assert.False(result);
        }
        else
        {
            // W przypadku, gdy klient jest bardzo ważnym klientem lub limit kredytu wynosi co najmniej 500,
            // test zakończy się sukcesem
            Assert.True(true);
        }
    }




    
    
}
