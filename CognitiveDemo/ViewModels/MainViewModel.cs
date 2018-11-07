using System;
using Xamarin.Forms;

namespace CognitiveDemo
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            this.LoginPageCommand = new Command(this.OpenLoginPage);
            this.EmotionPageCommand = new Command(this.OpenEmotionPage);
        }

        private void OpenEmotionPage()
        {
            this.Navigate?.Invoke(new EmotionDetectionPage());
        }

        private void OpenLoginPage()
        {
            this.Navigate?.Invoke(new SignInPage());
        }

        public Command LoginPageCommand { get; protected set; }

        public Command EmotionPageCommand { get; protected set; }
    }
}
