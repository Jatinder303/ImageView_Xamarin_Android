using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Android.Widget;
using Android.Views;
using Android.Graphics.Drawables;
using Android.Graphics;
using Android.Content;

namespace ImageView_Xamarin_Android
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        ImageView imgCity;
        Button btnAuckland;
        Button btnWellington;
        Button btnChristchurch;
        Button btnHamilton;
        Button btnShare;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            imgCity = FindViewById<ImageView>(Resource.Id.imgCity);
            btnAuckland = FindViewById<Button>(Resource.Id.btnAuckland);
            btnWellington = FindViewById<Button>(Resource.Id.btnWellington);
            btnHamilton = FindViewById<Button>(Resource.Id.btnHamilton);
            btnChristchurch = FindViewById<Button>(Resource.Id.btnChristchurch);

            btnShare = FindViewById<Button>(Resource.Id.btnShare);

            btnAuckland.Click += OnBtnAucklandClick;
            btnWellington.Click += OnBtnWellingtonClick;
            btnHamilton.Click += OnBtnHamiltonClick;
            btnChristchurch.Click += OnBtnChristchurchClick;

            btnShare.Click += OnBtnShareClick;
        }

		public void OnBtnShareClick(object sender, EventArgs e)
		{
			Intent shareIntent = new Intent(Intent.ActionSend);
			shareIntent.SetType("image/*");
			imgCity.DrawingCacheEnabled = true;

			Bitmap imgbmp = imgCity.GetDrawingCache(true);
			//BitmapDrawable drawable = (BitmapDrawable)imgCity.getDrawable();
			//Bitmap bitmap = drawable.Bitmap;

			Android.Net.Uri uri = getLocalBitmapUri(imgbmp);
			shareIntent.PutExtra(Intent.ExtraStream, uri);
			StartActivity(Intent.CreateChooser(shareIntent, "Share via"));
		}

		public Android.Net.Uri getLocalBitmapUri(Bitmap bmp)
		{
			Toast.MakeText(this, "This is Android", ToastLength.Long);
			Android.Net.Uri bmpUri = null;
			try
			{

				String storagePath = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
				Java.IO.File storageDirectory = new Java.IO.File(storagePath);
				String filePath = storageDirectory.ToString() + "\\" + "APPNAME.png";

				using (var os = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
				{
					bmp.Compress(Bitmap.CompressFormat.Png, 100, os);
				}
				bmpUri = Android.Net.Uri.Parse(filePath);

			}
			catch (Exception ex)
			{
				System.Console.Write(ex.Message);
			}

			return bmpUri;
		}



		public void OnBtnAucklandClick(object sender, EventArgs e)
		{
			imgCity.SetImageResource(Resource.Drawable.auckland);
		}

		public void OnBtnWellingtonClick(object sender, EventArgs e)
		{
			imgCity.SetImageResource(Resource.Drawable.wellington);
		}

		public void OnBtnHamiltonClick(object sender, EventArgs e)
		{
			imgCity.SetImageResource(Resource.Drawable.hamilton);
		}

		public void OnBtnChristchurchClick(object sender, EventArgs e)
		{
			imgCity.SetImageResource(Resource.Drawable.christchurch);
		}
		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}