namespace _Game.Source.Presenter.PinPresentation.Views.Data
{
    public struct PinInfoViewCallback
    {
        public enum ActionType
        {
            Edit,
            EditFinished,
            Close,
            LoadImage
        }
        public string NewName { get; private set; }
        public string NewDescription { get; private set; }
        public ActionType Action;

        public static PinInfoViewCallback OnEditFinished(string name, string description)
        {
            return new PinInfoViewCallback() {NewName = name, NewDescription = description, Action = ActionType.EditFinished};
        }

        public static PinInfoViewCallback SwithEditMode()
        {
            return new PinInfoViewCallback(){Action = ActionType.Edit};
        }

        public static PinInfoViewCallback OnClose()
        {
            return new PinInfoViewCallback(){Action = ActionType.Close};
        }
        

        public static PinInfoViewCallback LoadImage(string currentViewName, string currentViewDescription)
        {
            return new PinInfoViewCallback(){Action = ActionType.LoadImage, NewName = currentViewName, NewDescription = currentViewDescription};
        }
    }
}