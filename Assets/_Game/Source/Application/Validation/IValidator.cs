namespace _Game.Source.Application.Validation
{
    public interface IValidator<TContext>
    {
        bool Validate(TContext context);
    }
}