using _Game.Source.Domain;

namespace _Game.Source.Infrastructure.Signals
{
    public struct PinDataChanged
    {
        public Pin Pin { get; private set; }

        public PinDataChanged(Pin pin)
        {
            this.Pin = pin;
        }
    }
}