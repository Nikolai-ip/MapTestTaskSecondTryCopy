using UnityEngine;

namespace _Game.Source.Presenter.PinPresentation.Views.Data
{
    public struct PinViewData
    {
        public Vector3 Position { get; private set;  }
        public bool IsLabelEnabled { get; private set; }
        public bool SetPosition { get; private set; }
        public string Name { get; private set; }

        public static PinViewData OnSetPosition(Vector3 position)
        {
            return new  PinViewData() { Position = position, SetPosition = true };
        }

        public static PinViewData SetLabelData(bool isLabelEnabled, string name)
        {
            return new PinViewData(){ IsLabelEnabled = isLabelEnabled, Name = name};
        }


    }
}