using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartPM.Models;
using Xamarin.Forms;
using SmartPM.Views.Team;
using Xamarin.Forms.Xaml;

namespace SmartPM.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TaskScreen : ContentPage
	{

        private AuthenModel userAccount = new AuthenModel();

        public TaskScreen ()
		{
			InitializeComponent ();
            List<TaskModel> task = new List<TaskModel>
            {
                new TaskModel
                {
                    taskId = "t001",
                    projectnumber = "p001",
                    taskname = "Gettering Requirement",
                    taskstart = "31/01/2018",
                    taskend = "01/02/2018",
                    actualstart = "01/02/2018",
                    actualend = "01/02/2018",
                    variant = "0",
                     team = "Employee1,dummyEmployee",
                     backclr = "#4CAF50",
                     picture = "thumTime"
                },
                new TaskModel
                {
                    taskId = "t002",
                    projectnumber = "p001",
                    taskname = "System Analysis And Design",
                    taskstart = "02/02/2018",
                    taskend = "05/02/2018",
                    actualstart = "03/02/2018",
                    actualend = "05/02/2018",
                    variant = "2",
                    team = "Employee2,dummyEmployee",
                     backclr = "#4CAF50",
                     picture = "thumTime"
                },
                new TaskModel
                {
                     taskId = "t003",
                    projectnumber = "p001",
                    taskname = "Development",
                    taskstart = "02/02/2018",
                    taskend = "05/02/2018",
                    actualstart = "03/02/2018",
                    actualend = "05/02/2018",
                    variant = "2",
                    team = "Employee3,dummyEmployee",
                    backclr = "#4CAF50",
                     picture = "thumTime"
                },
                  new TaskModel
                {
                     taskId = "t004",
                    projectnumber = "p001",
                    taskname = "Tesing",
                    taskstart = "02/02/2018",
                    taskend = "05/02/2018",
                    actualstart = "03/02/2018",
                    actualend = "05/02/2018",
                    variant = "2",
                    team = "Employee4,dummyEmployee",
                    backclr = "#4CAF50",
                     picture = "thumTime"
                },
                    new TaskModel
                {
                     taskId = "t005",
                    projectnumber = "p001",
                    taskname = "Deploy",
                    taskstart = "02/02/2018",
                    taskend = "05/02/2018",
                    actualstart = "03/02/2018",
                    actualend = "05/02/2018",
                    variant = "2",
                    team = "Employee5,dummyEmployee",
                    backclr = "#c8cd20",
                    picture = "thumTime"
                },
            };
            Tasklist.ItemsSource = task;

		}
        private async void tasklist_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new TaskFunctionScreen());
        }

        private async void logout(object sender, EventArgs e)
        {

            userAccount = null;
            App.Current.MainPage = new LoginScreen();
        }
    }
}