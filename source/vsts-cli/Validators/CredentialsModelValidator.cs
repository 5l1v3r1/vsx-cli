using FluentValidation;
using vsx.Models;

namespace vsx.Validators
{
    public class CredentialsModelValidator : AbstractValidator<CredentialsModel>
    {
        public CredentialsModelValidator()
        {
            RuleFor(credentials => credentials.AccountName).NotNull();
            RuleFor(credentials => credentials.AccountName).NotEmpty();
            RuleFor(credentials => credentials.PersonalAccessToken).NotNull();
            RuleFor(credentials => credentials.PersonalAccessToken).NotEmpty();
        }
    }
}
