using UnityEngine;

namespace _Game.Source.Presenter.PinPresentation.Views.Data
{
    public struct PinViewCallBack
    {
        public enum ActionType
        {
            LabelToggle,
            ChangePosition
        }

        public ActionType Action { get; private set; }
        public Vector2 Position { get; set; }

        public PinViewCallBack(ActionType action)
        {
            Action = action;
            Position =  Vector2.zero;
        }
    }
}