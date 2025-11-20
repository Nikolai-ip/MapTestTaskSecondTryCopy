using _Game.Source.Presenter.PinPresentation.Views.Data;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Source.Presenter.PinPresentation.Views.PinInfoModeViews
{
    public class EditModeView: PinInfoModeView
    {
        [SerializeField] private TMP_InputField _nameInputField;
        [SerializeField] private TMP_InputField _descriptionInputField;
        [SerializeField] private RawImage _pinImage;
        public override string Name => _nameInputField.text;
        public override string Description => _descriptionInputField.text;
        public override void SetData(PinInfoViewData viewData)
        {
            _nameInputField.text = viewData.Name;
            _descriptionInputField.text = viewData.Description;
            if (viewData.ImageExist)
                _pinImage.texture = viewData.Image;
        }

        public override void Dispose()
        {
            _nameInputField.text = "";
            _descriptionInputField.text = "";
            _pinImage.texture = null;
        }
    }
}