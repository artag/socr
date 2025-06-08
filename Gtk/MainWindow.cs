using System;
using System.Threading;
using Gdk;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace Gtk
{
    class MainWindow : Window
    {
        [UI]
        private Button _buttonCapture = null;
        [UI]
        private Label _labelStatusText = null;
        private ScreenRegion _screenRegion;

        public MainWindow()
            : this(new Builder("MainWindow.glade"))
        {
        }

        private MainWindow(Builder builder)
            : base(builder.GetRawOwnedObject("MainWindow"))
        {
            builder.Autoconnect(this);

            DeleteEvent += Window_DeleteEvent;
            _buttonCapture.Clicked += ButtonCaptureClicked;
            _labelStatusText.Text = "Ready";
        }

        private ScreenRegion ScreenRegion =>
            LazyInitializer.EnsureInitialized(
                ref _screenRegion,
                CreateScreenRegionWindow);

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            ScreenRegion?.Close();
            Application.Quit();
        }

        private void ButtonCaptureClicked(object sender, EventArgs a)
        {
            ScreenRegion.ShowAll();
            // w.Decorated = false;
            // w.Opacity = 0.5;
            // Window w = new Window("My first GTK# Application! ");
            // w.Resize(200,200);
            // var myLabel = new Label();
            // myLabel.Text = "Hello World!!!!";
            // w.Add(myLabel);
            // w.ShowAll();


            // Gdk.Window window = Gdk.Global.DefaultRootWindow;
            // if (window!=null)
            // {
            //     var pixBuf = new Pixbuf(window, 10, 10, 1000, 500);
            //     var dt = DateTime.Now;
            //     pixBuf.Save($"/home/agart/1/{dt.ToString("HH-mm-ss")}.png", "png");
            // }


            // Gdk.Window rootWindow = Gdk.Window.GetDefaultRootWindow();
            // int width, height;
            // this.GetSize(out width, out height);
            //
            // Pixbuf pixbuf = new Pixbuf(Colorspace.Rgb, false, 8, width, height);
            // pixbuf = pixbuf.GetPixelsWithLength(1000);
            //
            // if(pixbuf != null){
            //     pixbuf.Save(filename, "png");
            //     Console.WriteLine("Screenshot saved to " + filename);
            // } else {
            //     Console.WriteLine("Failed to capture screenshot.");
            // }


            // _counter++;
            // _label1.Text = "Hello World! This button has been clicked " + _counter + " time(s).";
        }

        private ScreenRegion CreateScreenRegionWindow()
        {
            var r = new ScreenRegion();
            return r;
        }
    }
}
