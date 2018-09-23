using System;
using System.Windows;

namespace Dialogs
{
    public class DialogsService : IDialogsService
    {
        private readonly string informationCaption = "Information";
        private readonly string warningCaption = "Warning";
        private readonly string errorCaption = "Error";
        private readonly string questionCaption = "Question";
        
        public void ShowInfo(string message)
        {
            MessageBox.Show(message, informationCaption, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void ShowWarning(string message)
        {
            MessageBox.Show(message, warningCaption, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public void ShowError(string error)
        {
            MessageBox.Show(error, errorCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void ShowException(Exception exception)
        {
            ShowException(string.Empty, exception);
        }

        public void ShowException(string message, Exception exception)
        {
            var text = string.Empty;
            if (!string.IsNullOrEmpty(message))
                text += message + Environment.NewLine;
            text += exception.ToString();
            MessageBox.Show(text, errorCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public bool? ShowYesNoQuestion(string question)
        {
            var dr = MessageBox.Show(question, questionCaption, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (dr == MessageBoxResult.Yes)
                return true;
            else if (dr == MessageBoxResult.No)
                return false;
            else
                return null;
        }
    }
}
