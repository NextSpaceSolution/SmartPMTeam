using System;
using System.Collections.Generic;
using System.Text;
using SmartPM.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace SmartPM.ViewModels
{
    public class GTimelineViewModel : INotifyPropertyChanged
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
                _descrips = "คุยกับขี้"
            }
        };

        public ObservableCollection<TempTimelineModel> items
        {
            get { return _items; }
            set {
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
                        _header = "ยสตน",
                        _descrips = "ยิ้มสด"
                    }
                   );
                });
            }
        }

        public event ProgressChangedEventHandler PropertyChanged;

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add
            {
                ((INotifyPropertyChanged)_items).PropertyChanged += value;
            }

            remove
            {
                ((INotifyPropertyChanged)_items).PropertyChanged -= value;
            }
        }
        /*
protected void OnPropertyChanged(string propertyName)
{
   PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}*/
    }
}
