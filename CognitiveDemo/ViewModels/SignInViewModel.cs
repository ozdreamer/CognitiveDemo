using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace CognitiveDemo
{
    public class SignInViewModel : BaseViewModel
    {
        #region Properties

        private string email;
        public string Email
        {
            get { return this.email; }
            set
            {
                this.email = value;
                this.OnPropertyChanged(nameof(this.Email));
            }
        }

        private string password;
        public string Password
        {
            get { return this.password; }
            set
            {
                this.password = value;
                this.OnPropertyChanged(nameof(this.Password));
            }
        }

        public ICommand SubmitCommand { protected set; get; }

        public ICommand ClearCommand { protected set; get; }

        #endregion

        public SignInViewModel()
        {
            this.SubmitCommand = new Command(OnSubmit);
            this.ClearCommand = new Command(OnClear);
        }

        private async void OnSubmit()
        {
            var existingUser = App.DbManager.GetUser(this.Email);
            if (existingUser == null)
            {
                this.ShowErrorMessage?.Invoke("User doesn't exists");
                return;
            }

            if (existingUser.Password != this.Password)
            {
                this.ShowErrorMessage?.Invoke("Invalid password");
                return;
            }

            bool result = await this.IdentifyPerson(Guid.Parse(existingUser.UserId));
            if (!result)
            {
                this.ShowErrorMessage?.Invoke("User couldn't be recognized");
                return;
            }

            this.Navigate?.Invoke(new ProductsPage());
        }

        private async Task<bool> IdentifyPerson(Guid personId)
        {
            if (!CrossMedia.IsSupported || !CrossMedia.Current.IsCameraAvailable)
            {
                return true;
            }

            var initializeResult = await CrossMedia.Current.Initialize();
            if (!initializeResult)
            {
                this.ShowErrorMessage?.Invoke("Can't initialize camera.");
                return false;
            }

            using (var media = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions { }))
            {
                if (media == null)
                {
                    return false;
                }

                var faces = await App.FaceClient.DetectAsync(media.GetStream());
                var faceIds = faces.Select(x => x.FaceId).ToArray();
                if (faceIds.Any())
                {
                    var result = await App.FaceClient.IdentifyAsync(Constants.PersonGroupId, faceIds);
                    foreach (var identifyResult in result)
                    {
                        if (identifyResult.Candidates.Length > 0)
                        {
                            var candidateId = identifyResult.Candidates[0].PersonId;
                            if (candidateId == personId)
                            {
                                return true;
                            }
                        }
                    }
                }

                return false;
            }
        }

        private void OnClear()
        {
            this.Email = null;
            this.Password = null;
        }
    }
}
