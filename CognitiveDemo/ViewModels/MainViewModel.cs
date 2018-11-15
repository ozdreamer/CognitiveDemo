using System;
using Xamarin.Forms;

namespace CognitiveDemo
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            this.LoginPageCommand = new Command(() => this.Navigate?.Invoke(new SignInPage()));
            this.DescriptionPageCommand = new Command(() => this.Navigate?.Invoke(new DescriptionPage()));
        }

        public Command LoginPageCommand { get; protected set; }

        public Command DescriptionPageCommand { get; protected set; }
    }
}
