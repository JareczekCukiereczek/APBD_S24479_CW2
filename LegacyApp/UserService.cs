using System;

namespace LegacyApp
{
    public class UserService
    {
        private readonly ClientRepository _clientRepository;
        private readonly UserCreditService _userCreditService;

        public UserService()
        {
            _clientRepository = new ClientRepository();
            _userCreditService = new UserCreditService();
        }

        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
                return false;

            if (!IsValidEmail(email))
                return false;

            if (!IsOver21YearsOld(dateOfBirth))
                return false;

            var client = _clientRepository.GetById(clientId);
            if (client == null)
                return false;

            var user = CreateUser(firstName, lastName, email, dateOfBirth, client);

            if (client.Type == "VeryImportantClient")
                user.HasCreditLimit = false;
            else
            {
                int creditLimit = _userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                if (client.Type == "ImportantClient")
                    creditLimit *= 2;
                user.CreditLimit = creditLimit;
            }

            if (user.HasCreditLimit && user.CreditLimit < 500)
                return false;

            UserDataAccess.AddUser(user);
            return true;
        }

        private bool IsValidEmail(string email)
        {
            return email.Contains("@") && email.Contains(".");
        }

        private bool IsOver21YearsOld(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day))
                age--;
            return age >= 21;
        }

        private User CreateUser(string firstName, string lastName, string email, DateTime dateOfBirth, Client client)
        {
            return new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };
        }
    }
}
