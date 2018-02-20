using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin;

namespace SmartPM.Helpers
{
    public class DisplayAlert
    {
        public void DisplayMessageAction(string msg)
        {
            App.Current.MainPage.DisplayAlert("Notification", msg, "Ok", "Cancle");
        }
    }
}
