using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Assignment4.ViewModel;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Assignment4
{
    public partial class App : Application
    {
        public static TaskViewModel viewModel = new TaskViewModel();

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();

        }

        protected async override void OnStart()
        {
            var handler = new WebRequestHandler();
            viewModel.TaskList = JsonConvert.DeserializeObject<ObservableCollection<Task>>(await new WebRequestHandler().Get("http://localhost:5000/items/getalltasks"));
            viewModel.AppointmentList = JsonConvert.DeserializeObject<ObservableCollection<Appointment>>(await new WebRequestHandler().Get("http://localhost:5000/items/getallappointments"));

            
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
