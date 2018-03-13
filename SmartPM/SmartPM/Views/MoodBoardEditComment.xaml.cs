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
using Newtonsoft.Json.Linq;


namespace SmartPM.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MoodBoardEditComment : ContentPage
	{

        CommentModel comment = new CommentModel();
        public string userID { get; set; }
		public MoodBoardEditComment (CommentModel model)
		{
			InitializeComponent ();

            userID = model.userId;

            editcomment.Text = model.commentDetail;

            comment.cid = model.cid;

            comment.bNumber = model.bNumber;
        }

        protected async void Save(object sender, EventArgs e)
        {
            RenderAPI();
            var nav = new NavigationPage(new MoodBoard(userID)) { BarBackgroundColor = Color.FromHex("#354b60"), BarTextColor = Color.White };
            await App.Current.MainPage.Navigation.PushModalAsync(nav);


        }

        protected async void Cancle(object sender, EventArgs e)
        {
            var nav = new NavigationPage(new MoodBoard(userID)) { BarBackgroundColor = Color.FromHex("#354b60"), BarTextColor = Color.White };
            await App.Current.MainPage.Navigation.PushModalAsync(nav);

        }

        public async void RenderAPI()
        {
            comment.commentDetail = editcomment.Text;
            try
            {
                await MoodBoardService.EditComment(userID, comment.commentDetail,comment.bNumber,comment.cid);
            }

            catch
            {
                DisplayAlert("Alert", "Cannot", "OK");
            }
        }
    }
}