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
	public partial class PMNewteamScreen : ContentPage
	{
        public string Tmember { get; set; }
		public PMNewteamScreen ()
		{
			InitializeComponent ();
            TeamModels lis = new TeamModels();
            {
                lis.managername = "ProjectManager 001";
                lis.picturemanager = "luffy";
                BindingContext = lis;
            }
            
            member1.Items.Add("USERID : P101 MR One");
            member1.Items.Add("USERID : P202 MR Two");
            member1.Items.Add("USERID : P303 MR Three");


            member2.Items.Add("USERID : P101 MR One");
            member2.Items.Add("USERID : P202 MR  Two");
            member2.Items.Add("USERID : P303 MR Three");

            member3.Items.Add("USERID : P101 MR One");
            member3.Items.Add("USERID : P202 MR  Two");
            member3.Items.Add("USERID : P303 MR Three");
        }

        private void member_SelectedIndexChanged1(object sender, EventArgs e)
        {
            Tmember = member1.Items[member1.SelectedIndex];

        }

        private void member_SelectedIndexChanged2(object sender, EventArgs e)
        {
            Tmember = member2.Items[member2.SelectedIndex];

        }

        private void member_SelectedIndexChanged3(object sender, EventArgs e)
        {
            Tmember = member3.Items[member3.SelectedIndex];

        }

        private void Click_submit(object sender, EventArgs e)
        {

        }
    }
}