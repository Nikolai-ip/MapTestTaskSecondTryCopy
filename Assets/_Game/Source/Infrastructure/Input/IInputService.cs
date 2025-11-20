using System;
using UnityEngine;

namespace _Game.Source.Infrastructure.Input
{
    public interface IInputService
    {
        public event Action<Vector2> OnPointerDown;
        public event Action<Vector2> OnPointerUp; 
        public Vector2 PointerPosition { get; }
    }
}