using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace testAndroid
{
    [Activity(Label = "testAndroid", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        int count = 1;
        util.cAtom carregador;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);
            Android.Webkit.WebView wc = FindViewById<Android.Webkit.WebView>(Resource.Id.webView1);
            //button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
            button.Click += delegate
            {
                carregador = new util.cAtom("http://www.vicentfernandez.cat/feeds/posts/default", 10);
                carregador.CarregarDades += CallbackCarregarDades;
                carregador.Run();
            };
        }

        private void CallbackCarregarDades()
        {
             Android.Webkit.WebView wc = FindViewById<Android.Webkit.WebView>(Resource.Id.webView1);
             string html = "<html><body>";
             foreach (var titol in carregador.getTitle())
             {
                 html += "<h2>" + titol + "</h2>";
             }
             html += "</body></html>";
             wc.LoadData(html, "text/html; charset=UTF-8", "ISO-8859-1");
        }
    }
}

