using System;
using Gdk;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace Gtk
{
    class MainWindow : Window
    {
        [UI] private Label _label1 = null;
        [UI] private Button _button1 = null;

        private int _counter;

        public MainWindow() : this(new Builder("MainWindow.glade")) { }

        private MainWindow(Builder builder) : base(builder.GetRawOwnedObject("MainWindow"))
        {
            builder.Autoconnect(this);

            DeleteEvent += Window_DeleteEvent;
            _button1.Clicked += Button1_Clicked;
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }

        private void Button1_Clicked(object sender, EventArgs a)
        {

            Window w = new Window("My first GTK# Application! ");
            w.Resize(200,200);
            var myLabel = new Label();
            myLabel.Text = "Hello World!!!!";
            w.Add(myLabel);
            w.ShowAll();
            w.Decorated = false;
            w.Opacity = 0.5;

            Gdk.Window window = Gdk.Global.DefaultRootWindow;
            if (window!=null)
            {
                var pixBuf = new Pixbuf(window, 10, 10, 1000, 500);
                var dt = DateTime.Now;
                pixBuf.Save($"/home/agart/1/{dt.ToString("HH-mm-ss")}.png", "png");
            }


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
    }
}
