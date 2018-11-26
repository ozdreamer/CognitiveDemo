using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Microsoft.ProjectOxford.Common.Contract;
using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;
using Xamarin.Forms;

namespace CognitiveDemo
{
    public class DescriptionViewModel : BaseViewModel
    {
        public DescriptionViewModel()
        {
            this.Title = "Description";
            this.CaptureCommand = new Command(this.OnCaptureClicked);
            this.FaceImage = ImageSource.FromResource("CognitiveDemo.Resources.Face_Detection_Icon.png");
        }

        public Command CaptureCommand { get; protected set; }

        private AnalysisResult selectedAnalysisResult;
        public AnalysisResult SelectedAnalysisResult
        {
            get
            {
                return this.selectedAnalysisResult;
            }

            set
            {
                if (this.selectedAnalysisResult != value)
                {
                    this.selectedAnalysisResult = value;
                    this.OnPropertyChanged(nameof(this.SelectedAnalysisResult));
                    this.OnPropertyChanged(nameof(this.Description));
                    this.OnPropertyChanged(nameof(this.Color));
                    this.OnPropertyChanged(nameof(this.Adult));
                    this.OnPropertyChanged(nameof(this.Age));
                    this.OnPropertyChanged(nameof(this.Gender));
                    this.OnPropertyChanged(nameof(this.Category));
                }
            }
        }

        private ImageSource faceImage;
        public ImageSource FaceImage
        {
            get
            {
                return this.faceImage;
            }

            set
            {
                if (this.faceImage != value)
                {
                    this.faceImage = value;
                    this.OnPropertyChanged(nameof(this.FaceImage));
                }
            }
        }

        public string Description => this.SelectedAnalysisResult?.Description?.Captions?.FirstOrDefault()?.Text ?? string.Empty;

        public Xamarin.Forms.Color Color => Xamarin.Forms.Color.FromHex(this.SelectedAnalysisResult?.Color?.AccentColor ?? "FFFFFF");

        public string Adult => this.SelectedAnalysisResult?.Adult?.AdultScore.ToString("0.00") ?? string.Empty;

        public string Age => this.SelectedAnalysisResult?.Faces.FirstOrDefault()?.Age.ToString() ?? string.Empty;

        public string Gender => this.SelectedAnalysisResult?.Faces.FirstOrDefault()?.Gender ?? string.Empty;

        public string Category => this.SelectedAnalysisResult?.Categories.FirstOrDefault()?.Name ?? string.Empty;

        private async void OnCaptureClicked()
        {
            if (!IsCameraAvailable)
            {
                this.Dialog.Alert("Camera is not available");
                return;
            }

            this.SelectedAnalysisResult = await this.PerformAnalysis();
            this.Dialog.Loading().Hide();
        }

        private async Task<AnalysisResult> PerformAnalysis()
        {
            using (var media = await this.TakePhoto())
            {
                if (media != null)
                {
                    this.Dialog.Loading("Processing");
                    this.FaceImage = ImageSource.FromStream(media.GetStream);
                    return await App.VisionClient.AnalyzeImageAsync(media.GetStream(), new[]{ VisualFeature.Adult, VisualFeature.Color, VisualFeature.Description, VisualFeature.ImageType, VisualFeature.Faces, VisualFeature.Categories });
                }
            }

            return null;
        }
    }
}
