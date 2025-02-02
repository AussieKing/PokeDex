using FluentValidation;
using PokedexApp.Models;

namespace PokedexApp.Validators
{
    public class PokemonValidator : AbstractValidator<Pokemon>
    {
        public PokemonValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(p => p.Height).GreaterThan(0).WithMessage("Height must be greater than 0.");
            RuleFor(p => p.Weight).GreaterThan(0).WithMessage("Weight must be greater than 0.");
            RuleFor(p => p.BaseExperience)
                .GreaterThan(0)
                .WithMessage("Base Experience must be greater than 0.");
            RuleForEach(p => p.Types).SetValidator(new PokemonTypeValidator());
            ;
        }
    }

    public class PokemonTypeValidator : AbstractValidator<PokemonType>
    {
        public PokemonTypeValidator()
        {
            RuleFor(pt => pt.Type).NotEmpty().WithMessage("Type is required.");
            RuleFor(pt => pt.Type.Name).NotEmpty().WithMessage("Type Name is required.");
        }
    }
}
