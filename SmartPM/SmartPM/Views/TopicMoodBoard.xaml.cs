using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartPM.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TopicMoodBoard : ContentPage
	{
		public TopicMoodBoard ()
		{
			InitializeComponent ();
		}

        protected async void toolBarCreate(object sender, EventArgs e)
        {
            var nav = new NavigationPage(new MoodBoard()) { BarBackgroundColor = Color.FromHex("#354b60"), BarTextColor = Color.White };
            await App.Current.MainPage.Navigation.PushModalAsync(nav);
        }
        protected async void toolBarCancle(object sender, EventArgs e)
        {
            var nav = new NavigationPage(new MoodBoard()) { BarBackgroundColor = Color.FromHex("#354b60"), BarTextColor = Color.White };
            await App.Current.MainPage.Navigation.PushModalAsync(nav);
        }
    }
}