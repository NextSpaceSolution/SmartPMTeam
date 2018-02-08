
using System;
using System.Collections.Generic;
using System.Text;
using SmartPM.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Prism.Mvvm;
using Prism.Commands;

namespace SmartPM.ViewModels
{
    class TodoTimelineViewModel : BindableBase
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
            set
            {
                _items = value;
                // OnPropertyChanged("_items");
            }
        }

        public ICommand AddCommand
        {
            get
            {
                return new Command(() =>
                {
                    _items.Add(new TempTimelineModel
                    {
                        _date = "7/2/2018",
                        _header = "นัดประชุม",
                        _descrips = "เวลา 11.00 เรื่อง เพิ่มเงินเดือน + 1000k"
                    }
                   );
                });
            }
        }

        public TodoTimelineViewModel()
        {
        }

    }
}
