using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Acr.UserDialogs;
using Plugin.DownloadManager;
using Plugin.DownloadManager.Abstractions;
using System.Linq;
using System.IO;

namespace ZwinnyCRUD.Mobile.Droid
{
    [Activity(Label = "ZwinnyCRUD.Mobile", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            UserDialogs.Init(this);
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Downloaded();

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void Downloaded()
        {
            CrossDownloadManager.Current.PathNameForDownloadedFile =
                new Func<IDownloadFile, string>(file =>
                {
                    string[] fileName = file.Url.Split("\\");
                    return System.IO.Path.Combine(ApplicationContext.GetExternalFilesDir(
                        Android.OS.Environment.DirectoryDownloads).AbsolutePath, fileName[6]);
                });
        }
    }
}