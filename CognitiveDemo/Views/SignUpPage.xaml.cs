namespace CognitiveDemo
{
    public partial class SignUpPage : BasePage
    {
        public SignUpViewModel ViewModel => this.BindingContext as SignUpViewModel;

        public SignUpPage() : base(new SignUpViewModel())
        {
            this.BindingContext = this.BaseViewModel;
            this.InitializeComponent();
        }
    }
}