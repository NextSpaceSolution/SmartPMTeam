using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartPM.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartPM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TempPage : ContentPage
    {
        private ObservableCollection<TempTimelineModel> _items = new ObservableCollection<TempTimelineModel>()
        {
            new TempTimelineModel
            {
                _date = "5/2/2018",
                _header = "เริ่มโปรเจค",
                _descrips = "เริ่มโปรเจค NextSpace"

            },
            new TempTimelineModel
            {
                _date = "7/2/2018",
                _header = "ไปพบลูกค้า",
                _descrips = "คุยกับใคร"
            }
        };
        public ObservableCollection<TempTimelineModel> Items
        {
            get { return _items; }
            set => _items = value;

        }


        public TempPage(string p1, string p2, string p3)
        {
            InitializeComponent();
            listItems.ItemsSource = Items;

            if (!string.IsNullOrEmpty(p1))
            {
                Add(p1, p2, p3);
            }

        }

        public async void Addtodo(object sender, EventArgs e)
        {
            _items.Add(new TempTimelineModel
            {
                _date = "15/2/2018",
                _header = "นัดประชุม",
                _descrips = "เวลา 11.00 เรื่อง เพิ่มเงินเดือนนักศึกษาฝึกงาน + 1000k + bonus 1000k"
            });
        }
       

        public async void Nexttodo(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TodoItem());
        }
        public void Add(string p1, string p2, string p3)
        {
            _items.Add(new TempTimelineModel
            {
                _date = p1,
                _header = p2,
                _descrips = p3
            });
        }
    }
	
}