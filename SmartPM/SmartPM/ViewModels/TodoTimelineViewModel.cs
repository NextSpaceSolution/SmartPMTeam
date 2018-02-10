
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
        private ObservableCollection<TempTimelineModel> _items = new ObservableCollection<TempTimelineModel>();
       

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
                });
            }
        }

        public TodoTimelineViewModel()
        {
        }

    }
}
