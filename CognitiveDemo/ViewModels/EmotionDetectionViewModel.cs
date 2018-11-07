using System.Linq;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Common.Contract;
using Plugin.Media;
using Plugin.Media.Abstractions;
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
                    this.FaceImage = ImageSource.FromStream(media.GetStream);

                    var emotions = await App.EmotionClient.RecognizeAsync(media.GetStream());
                    return emotions.FirstOrDefault();
                }
            }

            return null;
        }
    }
}

