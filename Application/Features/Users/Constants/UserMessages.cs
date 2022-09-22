using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Constants
{
    public class UserMessages
    {
        public const string FirstNameIsRequired = "First name is required";
        public const string LastNameIsRequired = "LastName is required";
        public const string EmailAddressIsRequired = "Email address is required";
        public const string EmailAddressIsNotValid = "Email address is not valid";
        public const string PasswordIsRequired = "Password is required";
        public const string UserNotFound = "User not found";
        public const string UserEmailAlreadyExists = "User with this email address already exists";
        public const string PasswordIsNotCorrect = "Password is not correct";
    }
}
