using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartPM.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SmartPM.Services;
namespace SmartPM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TopicMoodBoard : ContentPage
    {
        NewsModels create = new NewsModels();
        public string userID { get; set; }
        public TopicMoodBoard(string ID)
        {
            InitializeComponent();
            userID = ID;



        }
        public async void RenderAPI()
        {

            create.subject = title.Text;
            create.note = detail.Text;




            try
            {
                await MoodBoardService.NewTopicAsync(userID, create.subject, create.note);
            }
            catch
            {
                DisplayAlert("Alert", "Cannot", "OK");
            }
        }

        protected async void toolBarCreate(object sender, EventArgs e)
        {

            RenderAPI();
            var nav = new NavigationPage(new MoodBoard(userID)) { BarBackgroundColor = Color.FromHex("#354b60"), BarTextColor = Color.White };
            await App.Current.MainPage.Navigation.PushModalAsync(nav);
        }
        protected async void toolBarCancle(object sender, EventArgs e)
        {
            // var nav = new NavigationPage(new MoodBoard()) { BarBackgroundColor = Color.FromHex("#354b60"), BarTextColor = Color.White };
            await App.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}