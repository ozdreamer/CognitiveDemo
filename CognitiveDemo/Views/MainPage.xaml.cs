namespace CognitiveDemo
{
    public partial class MainPage : BasePage
    {
        public MainViewModel ViewModel => this.BindingContext as MainViewModel;

        public MainPage() : base(new MainViewModel())
        {
            this.BindingContext = this.BaseViewModel;
            this.InitializeComponent();
        }
    }
}
