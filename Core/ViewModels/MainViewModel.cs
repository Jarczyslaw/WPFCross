using Logging;
using MvvmCross.Commands;

namespace Core.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public string Test { get; set; } = "Test Test Test";

        private IMvxCommand testCommand;
        public IMvxCommand TestCommand
        {
            get
            {
                if (testCommand == null)
                    testCommand = new MvxCommand(() =>
                    {
                        //loggingService.Debug("Test command Test command Test command Test command Test command Test command");
                        throw new System.Exception("Exception message");
                    });
                return testCommand;
            }
        }

        private ILoggingService loggingService;

        public MainViewModel(ILoggingService loggingService)
        {
            this.loggingService = loggingService;
        }
    }
}
