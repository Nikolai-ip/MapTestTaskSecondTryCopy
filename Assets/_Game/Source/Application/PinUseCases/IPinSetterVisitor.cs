using _Game.Source.Domain;

namespace _Game.Source.Application.PinUseCases
{
    public interface IPinSetterVisitor
    {
        void SetPin(Pin pin);
    }
}