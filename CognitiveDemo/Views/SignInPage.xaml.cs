namespace CognitiveDemo
{
    public partial class SignInPage : BasePage
    {
        public SignInViewModel ViewModel => this.BindingContext as SignInViewModel;

        public SignInPage() : base(new SignInViewModel())
        {
            this.BindingContext = this.BaseViewModel;
            this.InitializeComponent();
        }
    }
}