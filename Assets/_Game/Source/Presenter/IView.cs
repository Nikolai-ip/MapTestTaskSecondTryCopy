using System;
using Cysharp.Threading.Tasks;

namespace _Game.Source.Presenter
{
    public interface IView<in TViewData>
    {
        void SetData(TViewData data);
    }

    public interface IViewAsync<in TViewData>
    {
        UniTask SetData(TViewData data);
    }
    public interface IViewEnableableAsync<in TViewData> : IViewAsync<TViewData>, IViewEnableable { }
    public interface IViewEnableable<in TViewData> : IView<TViewData>, IViewEnableable { }
    public interface IViewEnableable
    {
        void Show();
        void Hide();
    }
    public interface IViewInteractable<out TViewCallback>
    {
        event Action<TViewCallback> Callback;
    }

    public interface IViewAction
    {
        event Action Action;
    }
}