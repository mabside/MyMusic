using FluentValidation;
using MyMusic.Api.Resources;

namespace MyMusic.Api.Validators
{
    class SaveArtistResourceValidator : AbstractValidator<SaveArtistResource>
    {
        public SaveArtistResourceValidator()
        {
            RuleFor(a => a.Name).NotEmpty().MaximumLength(50);
        }
    }

}