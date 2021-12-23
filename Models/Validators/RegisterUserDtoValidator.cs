using FluentValidation;
using RecipeLib.Entities;

namespace RecipeLib.Models.Validators;

public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
{
    public RegisterUserDtoValidator(AppDbContext dbContext)
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Nickname).NotEmpty();
        RuleFor(x => x.Password).MinimumLength(6).MaximumLength(64);
        RuleFor(x => x.ConfirmPassword).Equal(e => e.Password);
        RuleFor(x => x.Email).Custom((value, context) =>
        {
            if (dbContext.Users.Any(u => u.Email == value))
                context.AddFailure("Email", "That email is in use");

        });
    }
}