using FluentValidation;
using wms_praktyki_yosi_api.Enitities;

namespace wms_praktyki_yosi_api.Models.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(MagazinesDbContext dbContext)
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithErrorCode("100")
                .EmailAddress().WithErrorCode("101");
            RuleFor(x => x.Password)
                .NotEmpty().WithErrorCode("110")
                .MinimumLength(6).WithErrorCode("111")
                .Matches("[A-Z]").WithErrorCode("112")
                .Matches("[a-z]").WithErrorCode("112")
                .Matches(@"\d").WithErrorCode("113")
                .Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]").WithErrorCode("114")
                .Matches("^[^£# “”]*$").WithErrorCode("115");

            RuleFor(x => x.ConfirmPassword)
                .Equal(e => e.Password).WithErrorCode("116");

            /*RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var emailInUse = dbContext.Users.Any(u => u.Email == value);
                    if (emailInUse)
                    {
                        context.AddFailure("Email", "That email is taken");
                    }
                });*/
        }
    }
}
