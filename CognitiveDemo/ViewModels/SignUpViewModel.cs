﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace CognitiveDemo
{
    public class SignUpViewModel : BaseViewModel
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

        public ICommand SubmitCommand { protected set; get; }

        public ICommand ClearCommand { protected set; get; }

        #endregion

        public SignUpViewModel()
        {
            this.SubmitCommand = new Command(OnSubmit);
            this.ClearCommand = new Command(OnClear);
        }

        private async void OnSubmit()
        {
            if (this.Password != this.RepeatPassword)
            {
                this.ShowErrorMessage?.Invoke("Passwords didn't match");
                return;
            }

            var existingUser = App.DbManager.GetAllUsers().FirstOrDefault(x => x.Email == this.Email);
            if (existingUser != null)
            {
                this.ShowErrorMessage?.Invoke("User exists");
                return;
            }

            // Face API Code.
            var userId = await this.TrainFace(this.Email);
            if (userId != Guid.Empty)
            {
                var newUser = new User { UserId = userId.ToString(), Email = this.Email, Password = this.Password };
                if (App.DbManager.SaveUser(newUser) == 0)
                {
                    this.ShowErrorMessage?.Invoke("Failed to create the user");
                    return;
                }
            }

            this.Navigate?.Invoke(new ProductsPage());
        }

        private async Task<Guid> TrainFace(string personName)
        {
            if (!CrossMedia.IsSupported || !CrossMedia.Current.IsCameraAvailable)
            {
                return Guid.NewGuid();
            }

            var personGroups = await App.FaceClient.GetPersonGroupsAsync();
            if (!personGroups.Any(x => x.PersonGroupId == Constants.PersonGroupId))
            {
                await App.FaceClient.CreatePersonGroupAsync(Constants.PersonGroupId, "rpm-person-group");
            }

            var persons = await App.FaceClient.GetPersonsAsync(Constants.PersonGroupId);
            var personId = persons.FirstOrDefault(x => x.Name == personName)?.PersonId;
            if (!personId.HasValue)
            {
                var result = await App.FaceClient.CreatePersonAsync(Constants.PersonGroupId, personName);
                if (result == null || result.PersonId == Guid.Empty)
                {
                    this.ShowErrorMessage?.Invoke("Coudn't create a person.");
                    return Guid.Empty;
                }

                personId = result.PersonId;
            }

            if (!personId.HasValue)
            {
                this.ShowErrorMessage?.Invoke("Coudn't create a person.");
                return Guid.Empty;
            }

            var initializeResult = await CrossMedia.Current.Initialize();
            if (!initializeResult)
            {
                this.ShowErrorMessage?.Invoke("Can't initialize camera.");
                return Guid.Empty;
            }

            using (var media = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions { }))
            {
                if (media == null)
                {
                    return Guid.Empty;
                }

                var result = await App.FaceClient.AddPersonFaceAsync(Constants.PersonGroupId, personId.Value, media.GetStream());
                if (result.PersistedFaceId != Guid.Empty)
                {
                    await App.FaceClient.TrainPersonGroupAsync(Constants.PersonGroupId);
                    return personId.Value;
                }

                return Guid.Empty;
            }
        }

        private void OnClear()
        {
            this.Email = null;
            this.Password = null;
            this.RepeatPassword = null;
        }
    }
}
