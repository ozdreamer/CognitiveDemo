using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace CognitiveDemo
{
    public class SignUpViewModel : BaseViewModel
    {
        public Action<string> ShowErrorMessage;

        private string email;
        public string Email
        {
            get { return this.email; }
            set
            {
                this.email = value;
                this.OnPropertyChanged(nameof(this.Email));
            }
        }

        private string password;
        public string Password
        {
            get { return this.password; }
            set
            {
                this.password = value;
                this.OnPropertyChanged(nameof(this.Password));
            }
        }

        private string repeatPassword;
        public string RepeatPassword
        {
            get { return this.repeatPassword; }
            set
            {
                this.repeatPassword = value;
                this.OnPropertyChanged(nameof(this.RepeatPassword));
            }
        }

        public ICommand SubmitCommand { protected set; get; }

        public ICommand ClearCommand { protected set; get; }

        public SignUpViewModel()
        {
            this.SubmitCommand = new Command(OnSubmit);
            this.ClearCommand = new Command(OnClear);
        }

        private void OnSubmit()
        {
            if (this.Password != this.RepeatPassword)
            {
                //this.ShowErrorMessage.Invoke("Passwords didn't match");
            }
        }

        private void OnClear()
        {
            this.Email = null;
            this.Password = null;
            this.RepeatPassword = null;
        }
    }
}
