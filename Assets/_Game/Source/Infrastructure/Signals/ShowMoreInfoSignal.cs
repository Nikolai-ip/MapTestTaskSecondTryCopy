using _Game.Source.Application.PinUseCases;

namespace _Game.Source.Infrastructure.Signals
{
    public struct ShowMoreInfoSignal
    {
        public PinComponent Pin { get; private set; }

        public ShowMoreInfoSignal(PinComponent pin)
        {
            Pin = pin;
        }
    }
}