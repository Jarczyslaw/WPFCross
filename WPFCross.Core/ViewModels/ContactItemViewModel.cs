namespace WPFCross.Core.ViewModels
{
    public class ContactItemViewModel
    {
        public string Initials
        {
            get { return string.IsNullOrEmpty(Title) ? "C" : Title[0].ToString(); }
        }

        public string Title { get; set; }
    }
}
