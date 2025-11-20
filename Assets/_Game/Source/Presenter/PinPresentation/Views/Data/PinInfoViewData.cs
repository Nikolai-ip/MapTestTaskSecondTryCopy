using UnityEngine;

namespace _Game.Source.Presenter.PinPresentation.Views.Data
{
    public struct PinInfoViewData
    {
        public string Name { get; }
        public string Description { get; }
        public bool ImageExist { get; }
        public Texture2D Image { get; }
        public PinInfoViewMode ViewMode { get; }
    
        public PinInfoViewData(string name, string description, Texture2D image, bool imageExist, PinInfoViewMode viewMode)
        {
            Name = name;
            Description = description;
            ViewMode = viewMode;
            ImageExist = imageExist;
            Image = image;
        }
    }
}