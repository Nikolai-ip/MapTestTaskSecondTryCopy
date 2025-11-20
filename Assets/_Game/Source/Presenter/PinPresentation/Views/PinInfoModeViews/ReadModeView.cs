using _Game.Source.Presenter.PinPresentation.Views.Data;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Source.Presenter.PinPresentation.Views.PinInfoModeViews
{
    public class ReadModeView: PinInfoModeView
    {
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private RawImage _pinImage;
        public override string Name => _nameText.text;
        public override string Description => _descriptionText.text;
        public override void SetData(PinInfoViewData viewData)
        {
            _nameText.text = viewData.Name;
            _descriptionText.text = viewData.Description;
            if (viewData.ImageExist)
                _pinImage.texture = viewData.Image;
        }

        public override void Dispose()
        {
            _nameText.text = "";
            _descriptionText.text = "";
            _pinImage.texture = null;
        }
    }
}