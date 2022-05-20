using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment4.Pages;
using Assignment4.ViewModel;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Assignment4
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new TaskViewModel();
            GetAllItems();
        }

        private async void GetAllItems()
        {
            var handler = new WebRequestHandler();
            (BindingContext as TaskViewModel).TaskList = JsonConvert.DeserializeObject<ObservableCollection<ViewModel.Task>>(await new WebRequestHandler().Get("http://localhost:5000/items/getalltasks"));
            (BindingContext as TaskViewModel).AppointmentList = JsonConvert.DeserializeObject<ObservableCollection<Appointment>>(await new WebRequestHandler().Get("http://localhost:5000/items/getallappointments"));

            taskListView.ItemsSource = (BindingContext as TaskViewModel).TaskList;
            appointmentListView.ItemsSource = (BindingContext as TaskViewModel).AppointmentList;
        }

        private void CreateTask_Clicked(object sender, EventArgs e)
        {
            var diag = new AddTaskDialogue((BindingContext as TaskViewModel).TaskList);
            Navigation.PushModalAsync(diag);
        }

        private void CreateAppointment_Clicked(object sender, EventArgs e)
        {
            var diag = new AddAppointmentDialogue((BindingContext as TaskViewModel).AppointmentList);
            Navigation.PushModalAsync(diag);
        }

        private void OnTaskSelected(object sender, EventArgs e)
        {
            if (taskListView.SelectedItem == null)
                return;

            var diag = new EditTask(taskListView.SelectedItem as ViewModel.Task);
            Navigation.PushModalAsync(diag);

            taskListView.SelectedItem = null;
        }

        private void OnAppointmentSelected(object sender, EventArgs e)
        {
            if (appointmentListView.SelectedItem == null)
                return;

            var diag = new EditAppointment(appointmentListView.SelectedItem as ViewModel.Appointment);
            Navigation.PushModalAsync(diag);

            appointmentListView.SelectedItem = null;
        }

        private void Save_Clicked(object sender, EventArgs e)
        {
            var taskListStr = JsonConvert.SerializeObject((BindingContext as TaskViewModel).TaskList);
            var appointmentListStr = JsonConvert.SerializeObject((BindingContext as TaskViewModel).AppointmentList);

            string tasksFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "tasks.json");
            string appointmentsFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "appointments.json");

            File.WriteAllText(tasksFileName, taskListStr);
            File.WriteAllText(appointmentsFileName, appointmentListStr);
        }

        private void Load_Clicked(object sender, EventArgs e)
        {
            string tasksFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "tasks.json");
            string appointmentsFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "appointments.json");

            var taskListStr = File.ReadAllText(tasksFileName);
            var appointmentListStr = File.ReadAllText(appointmentsFileName);

            (BindingContext as TaskViewModel).TaskList = JsonConvert.DeserializeObject<ObservableCollection<ViewModel.Task>>(taskListStr);
            (BindingContext as TaskViewModel).AppointmentList = JsonConvert.DeserializeObject<ObservableCollection<ViewModel.Appointment>>(appointmentListStr);

            taskListView.ItemsSource = (BindingContext as TaskViewModel).TaskList;
            appointmentListView.ItemsSource = (BindingContext as TaskViewModel).AppointmentList;
        }

        private void Sort_Clicked(object sender, EventArgs e)
        {
            taskListView.ItemsSource = (BindingContext as TaskViewModel).TaskList.OrderBy(t => t.Priority);
        }

        private async void DeleteTaskSwipeItem_Selected(object sender, EventArgs e)
        {
            var handler = new WebRequestHandler();
            SwipeItem item = sender as SwipeItem;
            ViewModel.Task taskToRemove = item.BindingContext as ViewModel.Task;

            ViewModel.Task taskToRemove2 = JsonConvert.DeserializeObject<ViewModel.Task>(await handler.Post("http://localhost:5000/items/deletetask", taskToRemove.Id));

            Debug.Assert(taskToRemove.Id.CompareTo(taskToRemove2.Id) == 0);

            (BindingContext as TaskViewModel).TaskList.Remove(item.BindingContext as ViewModel.Task);

        }

        private async void DeleteAppointmentSwipeItem_Selected(object sender, EventArgs e)
        {
            var handler = new WebRequestHandler();
            SwipeItem item = sender as SwipeItem;
            ViewModel.Appointment appointmentToRemove = item.BindingContext as ViewModel.Appointment;

            ViewModel.Appointment appointmentToRemove2 = JsonConvert.DeserializeObject<ViewModel.Appointment>(await handler.Post("http://localhost:5000/items/deleteappointment", appointmentToRemove.Id));

            (BindingContext as TaskViewModel).AppointmentList.Remove(item.BindingContext as ViewModel.Appointment);
        }

        private async void OnSearchButtonPressed(object sender, EventArgs e)
        {
            var handler = new WebRequestHandler();
            var keyword = MainSearchBar.Text;
            List<ViewModel.Task> taskSearchResults = JsonConvert.DeserializeObject<List<ViewModel.Task>>(await handler.Post("http://localhost:5000/items/searchtasks", keyword));
            List<ViewModel.Appointment> appointmentSearchResults = JsonConvert.DeserializeObject<List<ViewModel.Appointment>>(await handler.Post("http://localhost:5000/items/searchappointments", keyword));

            taskListView.ItemsSource = taskSearchResults;
            appointmentListView.ItemsSource = appointmentSearchResults;
        }

        private void ResetSearch_Clicked(object sender, EventArgs e)
        {
            GetAllItems();
        }
    }
}
