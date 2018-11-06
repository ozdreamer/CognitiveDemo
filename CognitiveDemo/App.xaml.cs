using System;
using Microsoft.ProjectOxford.Face;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CognitiveDemo
{
    public partial class App : Application
    {
        public static DatabaseManager DbManager = new DatabaseManager();

        public static FaceServiceClient FaceClient = new FaceServiceClient(Constants.FaceServiceSubscriptionKey);

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new SignInPage());
        }

        protected override void OnStart()
        {
            base.OnStart();
        }

        protected override void OnSleep()
        {
            base.OnSleep();
        }

        protected override void OnResume()
        {
            base.OnResume();
        }
    }
}
