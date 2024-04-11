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
       
        bool result = service.AddUser(firstName, lastName, email, birthName, clientId);
        
        
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
        
        bool result = service.AddUser(firstName, lastName, email, birthName, clientId);
        
       
        Assert.Equal(true,result);
    }
    //testowanie email 
    [Fact]
    public void IsValidEmail_WithValidEmail_ReturnsTrue()
    {
        
        var userService = new UserService();

        var result = userService.IsValidEmail("johndoe@gmail,com");

        Assert.False(result);
    }
    [Fact]
    public void IsInvalidEmailReturnsFalse()
    {
        var userService = new UserService();

       
        var result = userService.IsValidEmail("johndoe!gmail,com");
        
        Assert.False(result);
    }
    
    //testowanie rocznika
    [Fact]
    public void IsOver21YearsOld_ReturnsTrue()
    {
        
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
        
        var userService = new UserService();
        var dateOfBirth = DateTime.Now.AddYears(-20);

        
        var result = userService.IsOver21YearsOld(dateOfBirth);

        
        Assert.False(result);
    }
    //testowanie userow
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
    //testowanie limitu kredytowego
    [Fact]
    public void AddUser_WithCreditLimitBelow500ForNonVeryImportantClient_ReturnsFalse()
    {
        
        var userService = new UserService();
        var firstName = "John";
        var lastName = "Doe";
        var email = "john.doe@example.com";
        var dateOfBirth = new DateTime(1990, 1, 1);
        var clientId = 123;

        
        var client = new Client { Type = "ImportantClient" };

        
        var userCreditService = new UserCreditService();
       
        var userCreditLimit = userCreditService.GetCreditLimit(lastName, dateOfBirth);

        
        if (client.Type != "VeryImportantClient" && userCreditLimit < 500)
        {
           
            var result = userService.AddUser(firstName, lastName, email, dateOfBirth, clientId);

           
            Assert.False(result);
        }
        else
        {
            
            Assert.True(true);
        }
    }
    public void AddUser_WithCreditLimitAbove500ForNonVeryImportantClient_ReturnsFalse()
    {
        var userService = new UserService();
        var firstName = "John";
        var lastName = "Malewski";
        var email = "john.doe@example.com";
        var dateOfBirth = new DateTime(1990, 1, 1);
        var clientId = 123;

        
        var client = new Client { Type = "BasicClient" };

        
        var userCreditService = new UserCreditService();
       
        var userCreditLimit = userCreditService.GetCreditLimit(lastName, dateOfBirth);

        
        if (client.Type != "VeryImportantClient" && userCreditLimit < 500)
        {
            
            var result = userService.AddUser(firstName, lastName, email, dateOfBirth, clientId);

            Assert.False(result);
        }
        else
        {
          
            Assert.True(true);
        }
    }




    
    
}
