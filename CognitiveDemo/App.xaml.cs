using System;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Emotion;
using Microsoft.ProjectOxford.Vision;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CognitiveDemo
{
    public partial class App : Application
    {
        public static DatabaseManager DbManager = new DatabaseManager();

        public static VisionServiceClient VisionClient = new VisionServiceClient(Constants.VisionServiceSubscriptionKey1, Constants.VisionServiceEndPoint);

        public static FaceServiceClient FaceClient = new FaceServiceClient(Constants.FaceServiceSubscriptionKey1, Constants.FaceServiceEndPoint);

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
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
