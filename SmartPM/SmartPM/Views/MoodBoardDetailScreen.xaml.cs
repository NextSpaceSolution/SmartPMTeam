using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartPM.Models.Timesheet;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SmartPM.Models;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using SmartPM.Services;

namespace SmartPM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]


    public partial class MoodBoardDetailScreen : ContentPage
    {
        public string userId { get; set; }

        CommentModel newcom = new CommentModel();
        
        NewsModels newmodel = new NewsModels();
        public ObservableCollection<NewsModels> News { get; set; }
        public ObservableCollection<CommentModel> com { get; set; }

        public MoodBoardDetailScreen(string Id, NewsModels model)
        {
            InitializeComponent();
      
            userId = Id;

               title.Text = model.subject;
               detail.Text = model.note;
               time.Text = model.time.ToString();
               name.Text = model.name;
 
            newmodel.name = model.name;
            newmodel.note = model.note;
            newmodel.subject = model.subject;
            newmodel.time = model.time;
            newmodel.bnumber = model.bnumber;

            RenderAPI();
        }

        protected async void toolBarEdit(object sender, EventArgs e)
        {
            string id = "99";
            if (id == "99" || id == "50")
            {


                var nav = new NavigationPage(new MoodBoardEditScreen(userId, newmodel)) { BarBackgroundColor = Color.FromHex("#354b60"), BarTextColor = Color.White };
                await App.Current.MainPage.Navigation.PushModalAsync(nav);
            }

            else
            {
                DisplayAlert("Alert", "คุณไม่สามารถแก้ไขโพสได้", "OK");
            }
        }


        public async void RenderAPI()
        {
            var jsonResult = await MoodBoardService.getComment(newmodel.bnumber);
            com = JsonConvert.DeserializeObject<ObservableCollection<CommentModel>>(jsonResult);
            commentlist.ItemsSource = com;
            this.IsBusy = false;

        }

        public async void RenderAPIComment()
        {
            newcom.commentDetail = commentDetail.Text;

            try
            {
                await MoodBoardService.NewComment(userId, newcom.commentDetail, newmodel.bnumber);
            }
            catch
            {
                DisplayAlert("Alert", "Cannot", "OK");
            }
        }

        private async void comment(object sender, EventArgs e)
        {
            RenderAPIComment();
            commentDetail.Text = "";
        }

        protected void Refesh(object sender, EventArgs e)
        {
            RenderAPI();
            commentlist.EndRefresh();
        }

    }
}