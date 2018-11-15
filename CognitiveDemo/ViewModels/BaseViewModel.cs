using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace CognitiveDemo
{
	public class BaseViewModel : INotifyPropertyChanged
    {
        public bool IsCameraAvailable => CrossMedia.IsSupported && CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported;

        private string title = string.Empty;
		public const string TitlePropertyName = "Title";
		public string Title
		{
			get { return title; }
			set { SetProperty(ref title, value, TitlePropertyName); }
		}

		private string subTitle = string.Empty;
		public const string SubtitlePropertyName = "Subtitle";
		public string Subtitle
		{
			get { return subTitle; }
			set { SetProperty(ref subTitle, value, SubtitlePropertyName); }
		}

		private string icon = null;
		public const string IconPropertyName = "Icon";
        public string Icon
		{
			get { return icon; }
			set { SetProperty(ref icon, value, IconPropertyName); }
		}

		private bool isBusy;
		public const string IsBusyPropertyName = "IsBusy";
		public bool IsBusy
		{
			get { return isBusy; }
			set { SetProperty(ref isBusy, value, IsBusyPropertyName); }
		}

		private bool canLoadMore = true;
		public const string CanLoadMorePropertyName = "CanLoadMore";
		public bool CanLoadMore
		{
			get { return canLoadMore; }
			set { SetProperty(ref canLoadMore, value, CanLoadMorePropertyName); }
		}

		protected void SetProperty<T>(
			ref T backingStore, T value,
			string propertyName,
			Action onChanged = null)
		{
			if (EqualityComparer<T>.Default.Equals(backingStore, value))
				return;

			backingStore = value;

			if (onChanged != null)
				onChanged();

			OnPropertyChanged(propertyName);
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public void OnPropertyChanged(string propertyName)
		{
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

        protected async Task<MediaFile> TakePhoto()
        {
            return await CrossMedia.Current.Initialize()
                ? await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions { DefaultCamera = CameraDevice.Front, ModalPresentationStyle = MediaPickerModalPresentationStyle.FullScreen })
                : null;
        }

        protected IUserDialogs Dialog => UserDialogs.Instance;

        public Action<BasePage> Navigate;
    }
}