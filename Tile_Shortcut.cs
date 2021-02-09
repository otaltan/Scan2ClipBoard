using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Service.QuickSettings;
using Java.Lang;

namespace Scan2ClipBoard
{
    [Service(Name = "com.otaltan.Scan2ClipBoard.Tile_Shortcut",
             Permission = Android.Manifest.Permission.BindQuickSettingsTile,
             Label = "S2C",
             Icon = "@drawable/Tile_S2C")]
    [IntentFilter(new[] { ActionQsTile })]

    public class Tile_Shortcut : TileService
    {
        //First time tile is added to quick settings
        public override void OnTileAdded()
        {
            base.OnTileAdded();
        }

        //Called each time tile is visible
        public override void OnStartListening()
        {
            base.OnStartListening();

            //Tile associated with the service
            var tile = QsTile;

            //Update label, icon, description, and state
            tile.Icon = Icon.CreateWithResource(this, Resource.Drawable.Tile_S2C);
            tile.Label = "S2C";

            //Set state here and UI will respond automatically
            tile.State = TileState.Inactive;
            tile.UpdateTile();
        }

        //Called when tile is no longer visible
        public override void OnStopListening()
        {
            base.OnStopListening();
        }

        //Called when tile is removed by the user
        public override void OnTileRemoved()
        {
            base.OnTileRemoved();
        }

        public override void OnClick()
        {
            base.OnClick();

            //Tile associated with the service
            var tile = QsTile;

            //Update label, icon, description, and state
            tile.Icon = Icon.CreateWithResource(this, Resource.Drawable.Tile_S2C);
            tile.Label = "S2C";

            //Set state here and UI will respond automatically
            tile.State = TileState.Active;
            tile.UpdateTile();

            if (IsLocked)
            {
                // Start the app after unlocking the phone
                UnlockAndRun(new Runnable(() =>
                {
                    //Start the App
                    Intent i = new Intent(this, typeof(MainActivity));
                    i.AddFlags(ActivityFlags.NewTask);
                    StartActivityAndCollapse(i);
                }));
            }
            else
            {
                //Start the App
                Intent i = new Intent(this, typeof(MainActivity));
                i.AddFlags(ActivityFlags.NewTask);
                StartActivityAndCollapse(i);
            }

            //Set state here and UI will respond automatically
            tile.State = TileState.Inactive;
            tile.UpdateTile();
        }
    }
}