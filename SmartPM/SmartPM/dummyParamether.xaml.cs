﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartPM
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class dummyParamether : ContentPage
	{
		public dummyParamether (string u, string p)
		{
			InitializeComponent ();
            Para1.Text = u;
            Para2.Text = p;
		}
	}
}