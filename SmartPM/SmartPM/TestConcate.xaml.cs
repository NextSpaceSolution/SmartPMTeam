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
        List<string> itemes = new List<string>
        {
            "ALON","BLON","CLON","ABLON","ACLON","ABCLON","DLON","ZLON","JLON","AKLON","1LON"
        };
		public TestConcate ()
		{
			InitializeComponent ();
            SerachList.ItemsSource = itemes;
		}

        private void MainSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = MainSearch.Text;
            SerachList.ItemsSource = 
                itemes.Where(name => 
                name.ToLower().Contains(keyword.ToLower()));
        }
	}
}