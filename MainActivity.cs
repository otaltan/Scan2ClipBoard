﻿using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Graphics;
using Android.Util;
using Plugin.Media;
using System.Text;
using System.Collections.Generic;
using Android.Gms.Vision;
using Android.Gms.Vision.Texts;
using Xamarin.Essentials;
using Android.Content.PM;

namespace Scan2ClipBoard
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : AppCompatActivity
    {
        ImageView imageView;
        Button captureButton;
        Button uploadButton;
        Button selectedButton;
        Button selectAllButton;
        Frame frame;
        SparseArray items;
        ListView listnames;
        List<string> itemlist;
        List<string> selecteditems = new List<string>();
        public StringBuilder search_string;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            captureButton = FindViewById<Button>(Resource.Id.captureButton);
            imageView = FindViewById<ImageView>(Resource.Id.imageView1);
            uploadButton = FindViewById<Button>(Resource.Id.loadButton);
            selectedButton = FindViewById<Button>(Resource.Id.copyButton);
            selectAllButton = FindViewById<Button>(Resource.Id.selectAllButton);

            // Get and Show App version number
            TextView currentVersion = FindViewById<TextView>(Resource.Id.txtVersion);
            var version = AppInfo.VersionString;
            currentVersion.Text = "Version: " + version;

            // Button Clicks
            captureButton.Click += delegate { TakePhoto(); };
            uploadButton.Click += delegate { UploadPhoto(); };
            selectedButton.Click += delegate { CopySelected(); };
            selectAllButton.Click += delegate { CopyAll(); };
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        async void TakePhoto()
        {
            await CrossMedia.Current.Initialize();

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Full,
                Name = "myimage.jpg",
                Directory = "sample"
            });

            if (file == null)
            {
                return;
            }

            // Convert file to byte array and set the resulting bitmap to imageview
            byte[] imageArray = System.IO.File.ReadAllBytes(file.Path);
            Bitmap bitmap = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
            imageView.SetImageBitmap(bitmap);

            // Detect characters of the image
            RecogText(bitmap);
        }

        async void UploadPhoto()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                Toast.MakeText(this, "Please choose a picture", ToastLength.Short).Show();
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Full
            });

            // Convert file to byte array, to bitmap and set it to ImageView
            byte[] imageArray = System.IO.File.ReadAllBytes(file.Path);
            Bitmap bitmap = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
            imageView.SetImageBitmap(bitmap);

            // Detect characters of the image
            RecogText(bitmap);
        }

        private void RecogText(Bitmap bitmap)
        {
            TextRecognizer txtRecognizer = new TextRecognizer.Builder(ApplicationContext).Build();

            if (!txtRecognizer.IsOperational)
            {
                Log.Error("Error", "Detector dependencies are not yet available");
            }
            else
            {
                frame = new Frame.Builder().SetBitmap(bitmap).Build();
                items = txtRecognizer.Detect(frame);
                
                // Create item list
                itemlist = new List<string>();

                // Add items to list
                for (int i = 0; i < items.Size(); i++)
                {
                    TextBlock item = (TextBlock)items.ValueAt(i);
                    itemlist.Add(item.Value);
                }

                // If no characters were found, print message 
                if (itemlist.Count == 0)
                {
                    Toast.MakeText(this, "No characters found", ToastLength.Long).Show();
                }

                // Fill the listView with the items of itemlist
                listnames = FindViewById<ListView>(Resource.Id.listRecogTxt);
                ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItemActivated1, itemlist);
                listnames.Adapter = adapter;
                listnames.ChoiceMode = ChoiceMode.Multiple;
                listnames.ItemClick += Listnames_ItemClick;

                // Rotate Image and restart detecting characters
                imageView.Click += delegate
                {
                    // Rotate Image by 90°
                    BitmapFactory.Options o2 = new BitmapFactory.Options();
                    o2.InSampleSize = 2;
                    Matrix matrix = new Matrix();
                    matrix.PostRotate(90);
                    
                    // Here you will get the image bitmap which has changed orientation
                    bitmap = Bitmap.CreateBitmap(bitmap, 0, 0, bitmap.Width, bitmap.Height, matrix, true);
                    imageView.SetImageBitmap(bitmap);

                    frame = new Frame.Builder().SetBitmap(bitmap).Build();
                    items = txtRecognizer.Detect(frame);
                    
                    // Create item list
                    itemlist = new List<string>();

                    // Add items to list
                    for (int i = 0; i < items.Size(); i++)
                    {
                        TextBlock item = (TextBlock)items.ValueAt(i);
                        itemlist.Add(item.Value);
                    }

                    // If no characters were found, print message 
                    if (itemlist.Count == 0)
                    {
                        Toast.MakeText(this, "No characters found", ToastLength.Long).Show();
                    }

                    // Fill the listView with the items of itemlist
                    listnames = FindViewById<ListView>(Resource.Id.listRecogTxt);
                    ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItemActivated1, itemlist);
                    listnames.Adapter = adapter;
                    listnames.ChoiceMode = ChoiceMode.Multiple;
                    listnames.ItemClick += Listnames_ItemClick;
                };
            }
        }

        private void Listnames_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            // If selecteditems is empty then add the selected item to selecteditems
            // If the selected item is not in selecteditems than add it 
            // If the selected item is already in selecteditems than remove it
            if (selecteditems.ToString().Length == 0)
            {
                selecteditems.Add((string)listnames.GetItemAtPosition(e.Position));
            }
            else if (!selecteditems.Contains((string)listnames.GetItemAtPosition(e.Position)))
            {
                selecteditems.Add((string)listnames.GetItemAtPosition(e.Position));
            }
            else
            {
                selecteditems.Remove((string)listnames.GetItemAtPosition(e.Position));
            }
        }

        private void CopySelected()
        {
            search_string = new StringBuilder();

            for (int i = 0; i < selecteditems.Count; i++)
            {
                search_string.Append(selecteditems[i] + " ");
            }
            string search_string2 = search_string.ToString();

            Copy2Clip(search_string2);         
        }

        private void CopyAll()
        {
            search_string = new StringBuilder();

            for (int i = 0; i < itemlist.Count; i++)
            {
                search_string.Append(itemlist[i] + " ");
            }
            string search_string2 = search_string.ToString();

            Copy2Clip(search_string2);
        }

        async void Copy2Clip(string search_string2)
        {
            await Clipboard.SetTextAsync(search_string2);
            
            Toast.MakeText(this, "Copied to Clipboard", ToastLength.Long).Show();
            Finish();
        }
    }
}