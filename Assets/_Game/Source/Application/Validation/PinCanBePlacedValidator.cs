using _Game.Source.Application.PinUseCases;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Game.Source.Application.Validation
{
    public class PinCanBePlacedValidator: IValidator<PinCanBePlacedContext>
    {
        private readonly float _validationRadius;
        private readonly EventSystem _eventSystem;

        public PinCanBePlacedValidator(float validationRadius, EventSystem eventSystem)
        {
            _validationRadius = validationRadius;
            _eventSystem = eventSystem;
        }

        public bool Validate(PinCanBePlacedContext context)
        {
            var col = Physics2D.OverlapCircle(context.MousePosition, _validationRadius);
            bool pointOverUi = _eventSystem.IsPointerOverGameObject();
            return !pointOverUi && (col == null || !col.TryGetComponent(out PinComponent pin));
        }
    }

    public struct PinCanBePlacedContext
    {
        public Vector2 MousePosition { get; }

        public PinCanBePlacedContext(Vector2 mousePosition)
        {
            MousePosition = mousePosition;
        }
    }
}