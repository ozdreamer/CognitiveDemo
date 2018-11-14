﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CognitiveDemo
{
    public class SignInViewModel : BaseViewModel
    {
        public SignInViewModel()
        {
            this.Title = "Sign In";
            this.FaceImage = ImageSource.FromResource("CognitiveDemo.Resources.Face_Detection_Icon.png");
            this.SignInCommand = new Command(OnSignInClicked);
            this.SignUpCommand = new Command(OnSignUpClicked);
        }

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

        public ICommand SignInCommand { protected set; get; }

        public ICommand SignUpCommand { protected set; get; }

        public bool IsLoginRequired => !IsCameraAvailable;

        #endregion

        private async void OnSignInClicked()
        {
            string userEmail = null;

            // When user login is required (no camera available)
            if (this.IsLoginRequired)
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

                userEmail = existingUser.Email;
            }

            // Face API code.
            if (IsCameraAvailable)
            {
                var personIds = await this.IdentifyPersons();

                // No person identified or the give user's face has not been recognized.
                if (!personIds.Any())
                {
                    this.ShowErrorMessage?.Invoke("User couldn't be recognized");
                    return;
                }

                var users = App.DbManager.GetAllUsers().ToList();
                foreach (var personId in personIds)
                {
                    var identifiedUser = App.DbManager.GetUser(personId);
                    if (identifiedUser != null)
                    {
                        userEmail = identifiedUser.Email;
                        break;
                    }
                }

                if (userEmail == null)
                {
                    this.ShowErrorMessage?.Invoke("No user found with the given face");
                    return;
                }
            }

            this.Navigate?.Invoke(new ProductsPage(userEmail));
        }

        /// <summary>
        /// Identifies the persons.
        /// </summary>
        /// <returns>The persons.</returns>
        private async Task<IEnumerable<Guid>> IdentifyPersons()
        {
            var persons = new List<Guid>();

            // Take a photo using the phone camera.
            using (var media = await this.TakePhoto())
            {
                if (media != null)
                {
                    this.FaceImage = ImageSource.FromStream(media.GetStream);

                    // Detect the faces from the photo.
                    var faces = await App.FaceClient.DetectAsync(media.GetStream());
                    var faceIds = faces.Select(x => x.FaceId).ToArray();

                    // If a face has been detected and if there is someone in the rpm group.
                    if (faceIds.Any() && (await App.FaceClient.ListPersonGroupsAsync()).Any(x => x.PersonGroupId == Constants.PersonGroupId))
                    {
                        // Identify the person with given list of faces.
                        var identifyResults = await App.FaceClient.IdentifyAsync(Constants.PersonGroupId, faceIds);
                        foreach (var result in identifyResults)
                        {
                            if (result.Candidates.Length > 0)
                            {
                                persons.AddRange(result.Candidates.Select(x => x.PersonId));
                            }
                        }
                    }
                }

                return persons;
            }
        }

        private void OnSignUpClicked()
        {
            this.Navigate?.Invoke(new SignUpPage());
        }
    }
}