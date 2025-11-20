using _Game.Source.Presenter.PinPresentation.Views.Data;
using UnityEngine;

namespace _Game.Source.Presenter.PinPresentation.Views.PinInfoModeViews
{
    public abstract class PinInfoModeView: MonoBehaviour, IViewEnableable<PinInfoViewData>
    {
        [SerializeField] private GameObject[] _toggleObjects;
        public abstract string Name { get; }
        public abstract string Description { get; }

        public abstract void SetData(PinInfoViewData viewData);
        public void Show()
        {
            foreach (var o in _toggleObjects)
                o.SetActive(true);
        }

        public void Hide()
        {
            foreach (var o in _toggleObjects)
                o.SetActive(false);
        }

        public abstract void Dispose();
    }
}