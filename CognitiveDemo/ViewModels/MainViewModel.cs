using System;
using Xamarin.Forms;

namespace CognitiveDemo
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            this.LoginPageCommand = new Command(this.OpenLoginPage);
            this.DescriptionPageCommand = new Command(this.OpenDescriptionPage);
        }

        private void OpenDescriptionPage()
        {
            this.Navigate?.Invoke(new DescriptionPage());
        }

        private void OpenLoginPage()
        {
            this.Navigate?.Invoke(new SignInPage());
        }

        public Command LoginPageCommand { get; protected set; }

        public Command DescriptionPageCommand { get; protected set; }
    }
}
