using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CognitiveDemo
{
    public class SignUpViewModel : BaseViewModel
    {
        public SignUpViewModel()
        {
            this.Title = "Sign UP";
            this.FaceImage = ImageSource.FromResource("CognitiveDemo.Resources.Face_Detection_Icon.png");
            this.SignUpCommand = new Command(OnSignUpClicked);
            this.ClearCommand = new Command(OnClearClicked);
        }

        #region Properties

        private string name;
        public string Name
        {
            get { return this.name; }
            set
            {
                this.name = value;
                this.OnPropertyChanged(nameof(this.Name));
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

        private string repeatPassword;
        public string RepeatPassword
        {
            get { return this.repeatPassword; }
            set
            {
                this.repeatPassword = value;
                this.OnPropertyChanged(nameof(this.RepeatPassword));
            }
        }

        ImageSource faceImage;
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

        public ICommand SignUpCommand { protected set; get; }

        public ICommand ClearCommand { protected set; get; }

        #endregion

        private async void OnSignUpClicked()
        {
            if (this.Password != this.RepeatPassword)
            {
                this.Dialog.Alert("Passwords didn't match");
                return;
            }

            var existingUser = App.DbManager.GetAllUsers().FirstOrDefault(x => x.Name == this.Name);
            if (existingUser != null)
            {
                this.Dialog.Alert("User exists");
                return;
            }

            var userId = Guid.NewGuid();
            if (IsCameraAvailable)
            {
                userId = await this.TrainFace(this.Name);
                this.Dialog.Loading().Hide();
            }

            if (userId != Guid.Empty)
            {
                var newUser = new User { UserId = userId.ToString(), Name = this.Name, Password = this.Password };
                if (App.DbManager.SaveUser(newUser) == 0)
                {
                    this.Dialog.Alert("Failed to create the user");
                    return;
                }
            }

            this.Navigate?.Invoke(new ProductsPage(this.Name));
        }

        /// <summary>
        /// Trains the face.
        /// </summary>
        /// <returns>The face.</returns>
        /// <param name="personName">Person name.</param>
        private async Task<Guid> TrainFace(string personName)
        {
            // Take a photo using the phone camera.
            using (var media = await this.TakePhoto())
            {
                this.Dialog.Loading("Processing");

                // Find the person group. Get 5 of them that starts with the same string.
                var personGroups = await App.FaceClient.ListPersonGroupsAsync();

                // Create if not available.
                if (!personGroups.Any(x => x.PersonGroupId == Constants.PersonGroupId))
                {
                    await App.FaceClient.CreatePersonGroupAsync(Constants.PersonGroupId, "rpm-person-group");
                }

                // Check for an existing persion with the given name.
                var persons = await App.FaceClient.GetPersonsAsync(Constants.PersonGroupId);
                var personId = persons.FirstOrDefault(x => x.Name == personName)?.PersonId;

                // Create a person record under the group.
                if (!personId.HasValue)
                {
                    var result = await App.FaceClient.CreatePersonAsync(Constants.PersonGroupId, personName);
                    personId = result.PersonId;
                }

                if (media != null)
                {
                    // Add the photo against the person in the cognitive service.
                    var result = await App.FaceClient.AddPersonFaceAsync(Constants.PersonGroupId, personId.Value, media.GetStream());
                    if (result.PersistedFaceId != Guid.Empty)
                    {
                        // Train the group. Need to be done after every update.
                        await App.FaceClient.TrainPersonGroupAsync(Constants.PersonGroupId);
                    }
                }

                return personId ?? Guid.Empty;
            }
        }

        private void OnClearClicked()
        {
            this.Name = null;
            this.Password = null;
            this.RepeatPassword = null;
        }
    }
}
