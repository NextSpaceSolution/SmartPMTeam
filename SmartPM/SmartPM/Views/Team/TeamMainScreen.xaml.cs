﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartPM.Views.Team
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TeamMainScreen : TabbedPage
	{
		public TeamMainScreen ()
		{
			InitializeComponent ();
		}
	}
}