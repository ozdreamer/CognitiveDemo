namespace CognitiveDemo
{
    public partial class MainPage : BasePage
    {
        private MainViewModel PageViewModel => this.BaseViewModel as MainViewModel;

        public MainPage() : base(new MainViewModel())
        {
            this.BindingContext = this.BaseViewModel;
            this.InitializeComponent();
        }
    }
}