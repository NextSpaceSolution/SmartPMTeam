using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartPM.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Collections.Specialized;

namespace SmartPM.Views.Team
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GlobalTimelineScreen : ContentPage
    {
        public GlobalTimelineScreen()
        {
            InitializeComponent();
            ObservableCollection<TempTimelineModel> item = new ObservableCollection<TempTimelineModel>() { 
            new TempTimelineModel
            {
                _date = "5/2/2018",
                _header = "Project Start",
                _descrips = "This Project has been started"

            },
                    new TempTimelineModel
                    {
                        _date = "6/2/2018",
                        _header = "Project phase 1",
                        _descrips = "phase 1 has been finished"
                    },
                    new TempTimelineModel
                    {
                        _date = "7/2/2018",
                        _header = "Project phase 2",
                        _descrips = "phase 2 has been finished"
                    }
                     };
            listItems.ItemsSource = item;
        }

        /*
        public event EventHandler Refreshing;

        public bool IsPullToRefreshEnabled { get; set; }
        public bool IsRefreshing { get; set; }
        public ICommand RefreshCommand { get; set; }


        public class ListItem : List<TempTimelineModel>, INotifyCollectionChanged
        {
            public event NotifyCollectionChangedEventHandler CollectionChanged;

            public new void Reverse()
            {
                base.Reverse();

                if (CollectionChanged != null)
                {
                    CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                }
            }
        }

        protected override void OnAppearing()
        {
            ListItem listItems = new ListItem()
                    {
                    new TempTimelineModel
                     {
                         _date = "5/2/2018",
                        _header = "Project Start",
                        _descrips = "This Project has been started"

                     },
                    new TempTimelineModel
                    {
                        _date = "6/2/2018",
                        _header = "Project phase 1",
                        _descrips = "phase 1 has been finished"
                     },
                    new TempTimelineModel
                    {
                        _date = "7/2/2018",
                        _header = "Project phase 2",
                        _descrips = "phase 2 has been finished"
                    }
                        };
            ListView listView = new ListView
            {
                ItemsSource = listItems,
                ItemTemplate = new DataTemplate(() =>
                {
                    TextCell textCell = new TextCell();
                    textCell.SetBinding(TextCell.TextProperty, ".");
                    return textCell;
                }),

                IsPullToRefreshEnabled = true
            };

            listView.RefreshCommand = new Command(() =>
            {
                listItems.Reverse();

                listView.IsRefreshing = false;
            });

            Content = listView;
            BindingContext = listView;
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            Content = null;
            base.OnDisappearing();
        }*/

    }  
   
}