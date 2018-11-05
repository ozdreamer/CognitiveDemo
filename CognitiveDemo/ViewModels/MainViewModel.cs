using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace CognitiveDemo
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand SignInCommand { set; get; }

        public ICommand SignUpCommand { set; get; }

        public MainViewModel()
        {
            this.SignInCommand = new Command(() => this.Navigate?.Invoke(new SignInPage()));
            this.SignUpCommand = new Command(() => this.Navigate?.Invoke(new SignUpPage()));
        }
    }
}
