using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartPM.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace SmartPM.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GlobalTodoTimelineItemScreen : ContentPage
	{

		public GlobalTodoTimelineItemScreen ()
		{
			InitializeComponent ();
		}

        async void OnSaveActivated(object sender, EventArgs e)
        {
            var todoItem = (TodoTimelineModel)BindingContext;
            ObservableCollection<TodoTimelineModel> items = new ObservableCollection<TodoTimelineModel>() { new TodoTimelineModel(todoItem) };
            await Navigation.PopAsync();

        }
       
        /* async void OnDeleteActivated(object sender, EventArgs e)
         {

             var todoItem = (TodoTimelineModel)BindingContext;
             await Navigation.PopAsync();

         }

         async void OnCancelActivated(object sender, EventArgs e)
         {
             await Navigation.PopAsync();
         }
         */
    }
}