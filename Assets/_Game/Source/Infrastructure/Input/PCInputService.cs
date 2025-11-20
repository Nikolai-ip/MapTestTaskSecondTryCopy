using System;
using UnityEngine;
using Zenject;

namespace _Game.Source.Infrastructure.Input
{
    public class PCInputService: IInputService, ITickable
    {
        public event Action<Vector2> OnPointerDown;
        public event Action<Vector2> OnPointerUp;
        public Vector2 PointerPosition { get; private set; }
        private readonly Camera _camera;

        public PCInputService(Camera camera)
        {
            _camera = camera;
        }

        public void Tick()
        {
            PointerPosition = _camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                OnPointerDown?.Invoke(PointerPosition);
            }

            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                OnPointerUp?.Invoke(PointerPosition);
            }
        }
    }
}