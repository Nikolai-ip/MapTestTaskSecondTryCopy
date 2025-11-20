using _Game.Source.Presenter.UIElements;
using TMPro;
using UnityEngine;

namespace _Game.Source.Presenter.PinPresentation.Views
{
    public class PinLabelPopUp: PopUpWindow
    {
        [SerializeField] private TextMeshProUGUI _nameText;
        
        public void SetName(string labelName)
        {
            _nameText.text = labelName;    
        }
    }
}