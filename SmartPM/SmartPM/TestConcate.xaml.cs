using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartPM
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TestConcate : ContentPage
	{
        DateTime _date { get; set; }
        TimeSpan _time { get; set; }
        string date { get; set; }
        string time { get; set; }
        string _concate { get; set; }
		public TestConcate ()
		{
			InitializeComponent ();         
		}
        public async void Next(object sender, EventArgs e)
        {
            _date = datepick.Date;
            _time = timepick.Time;

            date = _date.ToString("yyyy-MM-dd");
            time = _time.ToString(@"hh\:mm\:ss");

           // string us = _date.ToString(new CultureInfo("en -US"));
            //string tus = _time.ToString(new CultureInfo("en-US"));
            _concate = date+"T"+time;
            await Navigation.PushAsync(new NextConcate(_concate));
        }
	}
}