using System.Threading.Tasks;
using Microsoft.ProjectOxford.Common.Contract;
using Microsoft.ProjectOxford.Vision.Contract;
using Xamarin.Forms;

namespace CognitiveDemo
{
    public class EmotionDetectionViewModel : BaseViewModel
    {
        public EmotionDetectionViewModel()
        {
            this.CaptureCommand = new Command(this.OnCaptureClicked);
            this.FaceImage = ImageSource.FromResource("CognitiveDemo.Resources.Analog_Camera.png");
        }

        public Command CaptureCommand { get; protected set; }

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

        private EmotionScores selectedScores;
        public EmotionScores SelectedScores
        {
            get
            {
                return this.selectedScores;
            }

            set
            {
                if (this.selectedScores != value)
                {
                    this.selectedScores = value;
                    this.OnPropertyChanged(nameof(this.SelectedScores));
                }
            }
        }

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
                }
            }
        }

        private async void OnCaptureClicked()
        {
            if (!IsCameraAvailable)
            {
                this.ShowErrorMessage?.Invoke("Camera is not available");
                return;
            }

            var emotion = await this.DetectEmotion();
            if (emotion != null)
            {
                this.SelectedScores = emotion.Scores;
            }
        }

        private async Task<Emotion> DetectEmotion()
        {
            using (var media = await this.TakePhoto())
            {
                if (media != null)
                {
                    var stream = media.GetStream();
                    this.FaceImage = ImageSource.FromStream(() => { return stream; });

                    /*
                    try { var emotions = await App.EmotionClient.RecognizeAsync(stream); }
                    catch(Exception ex){}*/
                    /*
                    try { var desc = await App.VisionClient.DescribeAsync(stream); }
                    catch (Exception ex) { }*/

                    this.SelectedAnalysisResult = await App.VisionClient.DescribeAsync(stream);
                }
            }

            return null;
        }
    }
}

