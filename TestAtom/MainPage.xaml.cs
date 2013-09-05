using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace TestAtom
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        private util.cAtom carregador;
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            carregador = new util.cAtom("http://www.vicentfernandez.cat/feeds/posts/default", 10);
            carregador.CarregarDades += callBackCarregador;
            carregador.Run();
        }
        private void callBackCarregador()
        {
            string html = "<html><body>";
            foreach (var titol in carregador.getTitle())
            {
                html += "<h2>" + titol + "</h2>";
            }
            html += "</body></html>";
            visualitzador.NavigateToString(html);
        }
       
    }
}