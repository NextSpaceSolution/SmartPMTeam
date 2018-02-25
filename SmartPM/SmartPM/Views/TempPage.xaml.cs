using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using SmartPM.Models.Timesheet;
using Xamarin.Forms;
using SmartPM.Models;
using Xamarin.Forms.Xaml;
using SmartPM.Services;

namespace SmartPM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TempPage : ContentPage
    {
        private ObservableCollection<ProjectTImeline> _items = new ObservableCollection<ProjectTImeline>();
        private ObservableCollection<ProjectInfo> data1 = new ObservableCollection<ProjectInfo>();
        ProjectInfo data2 = new ProjectInfo();
        public ObservableCollection<ProjectTImeline> Items
        {
            get { return _items; }
            set => _items = value;

        }


        public TempPage(string id)
        {
            InitializeComponent();
            //Gettimeline();
            string pid = id;
            RenderReqTimelineLog(pid);
            RenderAPI(pid);
        

        }
        public async void RenderAPI(string pid)
        {
            try
            {
                var jsonResult = await ProjectService.GetProInfo(pid);
                data1 = JsonConvert.DeserializeObject<ObservableCollection<ProjectInfo>>(jsonResult);


                foreach (var item in data1)
                {
                    data2.projectName = item.projectName;

                }
                Headerx.Text = data2.projectName;
            }
            catch { }
        }

        public async void RenderReqTimelineLog(string pid)
        {
            try
            {
                var jsonResult = await ProjectService.reqTimelineLog(pid);
                Items = JsonConvert.DeserializeObject<ObservableCollection<ProjectTImeline>>(jsonResult);
                if (Items != null)
                    listItems.ItemsSource = Items;
                else
                    Title = "ยังไม่มีข้อมูล";
            }
            catch { }
        }       
    }
}
	
