using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.AudioRecorder;
using Xamarin.Forms;

namespace CognitiveDemo
{
    public class SpeechToTextViewModel : BaseViewModel
    {
        public SpeechToTextViewModel()
        {
            this.Title = "Speech to Text";
            this.RecordCommand = new Command(this.RecordVoice);

            this.recorder = new AudioRecorderService
            {
                StopRecordingAfterTimeout = true,
                TotalAudioTimeout = TimeSpan.FromSeconds(15),
                AudioSilenceTimeout = TimeSpan.FromSeconds(2)
            };

            this.authenticationClient = new HttpClient();
            this.authenticationClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Constants.SpeechServiceSubscriptionKey1);
        }

        private string speech;
        public string Speech
        {
            get
            {
                return this.speech;
            }
            set
            {
                this.speech = value;
                this.OnPropertyChanged(nameof(this.Speech));
            }
        }

        public ICommand RecordCommand { protected set; get; }

        private readonly HttpClient authenticationClient;

        private AudioRecorderService recorder;

        private async Task<string> FetchTokenAsync(string fetchUri)
        {
            UriBuilder uriBuilder = new UriBuilder(fetchUri);
            uriBuilder.Path += "/issueToken";
            var result = await this.authenticationClient.PostAsync(uriBuilder.Uri.AbsoluteUri, null);
            return await result.Content.ReadAsStringAsync();
        }

        private async void RecordVoice()
        {
            using (var stream = this.RecordAudio())
            {

            }
        }

        /*public async Task<SpeechResult> RecognizeSpeechAsync(string filename)
        {

            // Read audio file to a stream
            var file = await PCLStorage.FileSystem.Current.LocalStorage.GetFileAsync(filename);
            var fileStream = await file.OpenAsync(PCLStorage.FileAccess.Read);

            // Send audio stream to Bing and deserialize the response
            string requestUri = GenerateRequestUri(Constants.SpeechRecognitionEndpoint);
            string accessToken = authenticationService.GetAccessToken();
            var response = await SendRequestAsync(fileStream, requestUri, accessToken, Constants.AudioContentType);
            var speechResult = JsonConvert.DeserializeObject<SpeechResult>(response);

            fileStream.Dispose();
            return speechResult;
        }*/

        private Stream RecordAudio()
        {
            var recordTask = this.recorder.StartRecording();
            Stream stream = null;
            recordTask.ConfigureAwait(true).GetAwaiter().OnCompleted(() => stream = recorder.GetAudioFileStream());
            Task.WaitAll(recordTask);
            return stream;
        }
    }
}
