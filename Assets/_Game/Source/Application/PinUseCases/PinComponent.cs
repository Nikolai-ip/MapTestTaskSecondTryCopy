using System;
using _Game.Source.Domain;
using _Game.Source.Infrastructure.Signals;
using MessagePipe;
using UnityEngine;
using Zenject;

namespace _Game.Source.Application.PinUseCases
{
    public class PinComponent : MonoBehaviour, IDisposable
    {
        private Pin _pin;
        public ReadOnlyPin Pin { get; private set; }
        public event Action OnInit;
        public event Action<ReadOnlyPin> OnPinTextDataChanged;
        [Inject] private IPublisher<PinDataChanged> _pinDataChangedPublisher;
     
        public void Initialize(Pin pin)
        {
            _pin = pin;
            Pin = new ReadOnlyPin(pin);
            OnInit?.Invoke();
        }

        public void SetPosition(Vector2 pinPosition)
        {
            _pin.Position = pinPosition;
            _pinDataChangedPublisher?.Publish(new PinDataChanged(_pin));
        }
        public void SetNewTextData(string newName, string description)
        {
            _pin.Name = newName;   
            _pin.Description = description;
            OnPinTextDataChanged?.Invoke(Pin);
            _pinDataChangedPublisher.Publish(new PinDataChanged(_pin));
        }

        public void SetImage(string imageName)
        {
            _pin.Image = imageName;
            _pinDataChangedPublisher.Publish(new PinDataChanged(_pin));
        }

        public void ApplySetPinVisitor(IPinSetterVisitor visitor)
        {
            visitor.SetPin(_pin);
        }

        public void Dispose()
        {
            _pin = null;
            Pin = new ReadOnlyPin();
        }
    }
}