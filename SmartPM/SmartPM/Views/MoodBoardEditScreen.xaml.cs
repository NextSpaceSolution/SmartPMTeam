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
    public partial class MoodBoardEditScreen : ContentPage
    {
        NewsModels edit = new NewsModels();
        public string userId { get; set; }
        public MoodBoardEditScreen(string Id, NewsModels model)
        {
            InitializeComponent();

            userId = Id;

            EditTitle.Text = model.subject;
            EditDetail.Text = model.note;

            edit.bnumber = model.bnumber;
        }
        public async void RenderAPI()
        {
            edit.subject = EditTitle.Text;
            edit.note = EditDetail.Text;

            try
            {
                await MoodBoardService.EditMoodItem(userId, edit.subject, edit.note, edit.bnumber);
            }

            catch
            {
                DisplayAlert("Alert", "Cannot", "OK");
            }
        }

    }
}