using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartPM
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NextConcate : ContentPage
	{/*
		public NextConcate (string did)
		{
			InitializeComponent ();
            t1.Text = did;
       
		}*/
        
        public NextConcate(string pid,string uid, string tid, string fid,string aid,string did, string ts, string te, string cts, string cte)
        {
            InitializeComponent();
            p1.Text = pid;
            p2.Text = uid;
            p3.Text = tid;
            p4.Text = fid;
            p5.Text = aid;
            t1.Text = did;
            t2.Text = ts;
            t3.Text = te;
            t4.Text = cts;
            t5.Text = cte;
        }
    }
}