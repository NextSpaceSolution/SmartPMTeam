using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartPM.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartPM.Views.Team
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TaskFunctionScreen : ContentPage
	{
		public TaskFunctionScreen ()
		{
			InitializeComponent ();
            List<TaskFunctionModel> taskfunc = new List<TaskFunctionModel>
            {
                new TaskFunctionModel
                {
                    taskId = "t001",
                    projectNumber = "p001",
                    functionId = "001",
                    functionName = "pai pob look ka",
                    functionstart = "N/A",
                    functionend = "N/A",
                    actualstart = "N/A",
                    actualend = "N/A",
                    variant = "N/A",
                    team = "employ1",
                    backclr = "#4CAF50",
                     picture = "thumTime"

                },
                new TaskFunctionModel
                {
                     taskId = "t001",
                    projectNumber = "p001",
                    functionId = "002",
                    functionName = "keb kwam tong karn",
                    functionstart = "N/A",
                    functionend = "N/A",
                    actualstart = "N/A",
                    actualend = "N/A",
                    variant = "N/A",
                     team = "employ2",
                    backclr = "#4CAF50",
                     picture = "thumTime"

                },
                new TaskFunctionModel
                {
                    taskId = "t001",
                    projectNumber = "p001",
                    functionId = "003",
                    functionName = "saroob kwam tong karn",
                    functionstart = "N/A",
                    functionend = "N/A",
                    actualstart = "N/A",
                    actualend = "N/A",
                    variant = "N/A",
                     team = "employ3",
                     backclr = "#4CAF50",
                     picture = "thumTime"


                },
                  new TaskFunctionModel
                {
                      taskId = "t001",
                    projectNumber = "p001",
                    functionId = "004",
                    functionName = "Requirement Analysis",
                    functionstart = "N/A",
                    functionend = "N/A",
                    actualstart = "N/A",
                    actualend = "N/A",
                    variant = "N/A",
                     team = "employ4",
                     backclr = "#4CAF50",
                     picture = "thumTime"

                },
                    new TaskFunctionModel
                {
                     taskId = "t001",
                    projectNumber = "p001",
                    functionId = "005",
                    functionName = "dummy Function",
                    functionstart = "N/A",
                    functionend = "N/A",
                    actualstart = "N/A",
                    actualend = "N/A",
                    variant = "N/A",
                     team = "employ5",
                     backclr = "#4CAF50",
                     picture = "thumTime"

                },
            };
            Taskflist.ItemsSource = taskfunc;
        }
	}
}