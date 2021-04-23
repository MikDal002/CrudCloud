using Firebase.Storage;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ZwinnyCRUD.Mobile.Views
{
    public partial class FirebasePage : ContentPage
    {
        MediaFile file;
        public FirebasePage()
        {
            InitializeComponent();
            imgBanner.Source = ImageSource.FromResource("ZwinnyCRUD.Mobile.images.xamarin-logo.png");
            imgChoosed.Source = ImageSource.FromResource("ZwinnyCRUD.Mobile.images.default.jpg");

        }
        private async void btnPick_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            try
            {
                file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                });
                if (file == null)
                    return;
                imgChoosed.Source = ImageSource.FromStream(() =>
                {
                    var imageStram = file.GetStream();
                    return imageStram;
                });
                await StoreImages(file.GetStream());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async void btnStore_Clicked(object sender, EventArgs e)
        {
            await StoreImages(file.GetStream());
        }

        public async Task<string> StoreImages(Stream imageStream)
        {
            var storageImage = await new FirebaseStorage("zwinnycrud.appspot.com")
                .Child("ZwinnyCRUD")
                .Child($"{Path.GetFileNameWithoutExtension(Path.GetRandomFileName())}.jpg")
                .PutAsync(imageStream);
            string imgurl = storageImage;
            return imgurl;
        }
    }
}