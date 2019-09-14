using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Pages;

namespace DataPagesDemo
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : Xamarin.Forms.Pages.ListDataPage
    {
        string _json;
        public MainPage()
        {
            InitializeComponent();

            using (WebClient wc = new WebClient())
            {
                _json = wc.DownloadString("https://services.odata.org/V3/Northwind/Northwind.svc/Products?$format=json");
            }

            JObject o = JObject.Parse(_json);
            if (o != null)
            {
                List<JToken> tokens = o.Children().ToList();             
                JToken token1 = tokens[1];
                var value = token1.Value<JProperty>();
                _json = value.First.ToString();                     
            }

            JsonDataSource jsonDataSource = new JsonDataSource();
            StringJsonSource stringJsonSource = new StringJsonSource();
            stringJsonSource.Json = _json;
            jsonDataSource.Source = stringJsonSource;
            this.DataSource = jsonDataSource;
        }
    }
}
