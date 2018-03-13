using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net;
using SmartPM.Models;
using Xamarin.Forms.Xaml;
using System;

namespace SmartPM.Views.PM
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PMTeamdetailScreen : ContentPage
	{
		public PMTeamdetailScreen ()
		{
			InitializeComponent ();

            TeamModels lis = new TeamModels();
            {
                lis.managername = "ProjectManager 001";
                lis.picturemanager = "luffy";
                lis.pictureteam1 = "userTemp";
                lis.pictureteam2 = "userTemp";
                lis.pictureteam3 = "userTemp";
                lis.pictureteam4 = "userTemp";
                lis.pictureteam5 = "userTemp";
                lis.employeename1 = "Employee1";
                lis.employeename2 = "Employee2";
                lis.employeename3 = "Employee3";
                lis.employeename4 = "Employee4";
                lis.employeename5 = "Employee5";
                lis.managername = "ProjectManager1";
                BindingContext = lis;
            }


        }
        private async void Edit_Team(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new PMEditteamScreen());
        }
    }
}