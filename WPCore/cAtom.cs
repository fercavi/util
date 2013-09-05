using System;
using System.Net;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;
using System.Collections.Generic;

namespace util
{
    public class cAtom
    {
        private string sURL;
        private List<string> sTitle=new List<string>();
        private List<string> sValue = new List<string>();
        public delegate void tCarregarDades();
        public  tCarregarDades CarregarDades=null;
        public delegate void tErrorConnexio();
        public tErrorConnexio ErrorConnexio=null;
        private int nMaxElements;
        
        public cAtom(string surl, int MaxElements=10){
            this.sURL = surl;
            this.nMaxElements = MaxElements;
        }
        public void Run(){
            
            WebClient wc = new System.Net.WebClient();
            wc.DownloadStringCompleted += ObtindreRss;
            wc.DownloadStringAsync(new Uri(sURL));
        }
        public void ObtindreRss(object sender, System.Net.DownloadStringCompletedEventArgs e)
        {
            int i=0;
            if (e.Error == null)
            {
                try
                {
                    XDocument xdoc = XDocument.Parse(e.Result, LoadOptions.None);
                    XElement xele = xdoc.Root;
                    sTitle.Clear();
                    sValue.Clear();
                    //todo, extraure els elements entry a xele2
                    foreach (XElement xele2 in xele.Elements())
                    {
                        if (xele2.Name.LocalName == "entry")
                        {
                            if (i++ <= nMaxElements)
                            {
                                foreach (XElement xele3 in xele2.Elements())
                                {
                                    if (xele3.Name.LocalName == "title")
                                        sTitle.Add(xele3.Value);
                                    if (xele3.Name.LocalName == "content")
                                        sValue.Add(xele3.Value);
                                }
                            }
                        }
                    }

                    if (CarregarDades != null)
                    {
                        CarregarDades();
                    }
                }
                catch (Exception ex)
                {
                    if (ErrorConnexio != null)
                    {
                        ErrorConnexio();
                    }
                }
            }
             
        }

        public List<string> getTitle()
        {
            return sTitle;
        }
        public List<string> getValue()
        {
            return sValue;
        }
    }
}
