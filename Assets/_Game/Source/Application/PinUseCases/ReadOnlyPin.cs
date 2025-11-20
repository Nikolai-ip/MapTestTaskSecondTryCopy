using System;
using _Game.Source.Domain;
using UnityEngine;

namespace _Game.Source.Application.PinUseCases
{
    public readonly struct ReadOnlyPin
    {
        public Guid Id => _pin.Id;
        public Vector2 Position => _pin.Position;
        public string Name => _pin.Name;
        public string Description => _pin.Description;
        public string Image => _pin.Image;
        private readonly Pin _pin;

        public ReadOnlyPin(Pin pin)
        {
            _pin = pin;
        }
    }
}