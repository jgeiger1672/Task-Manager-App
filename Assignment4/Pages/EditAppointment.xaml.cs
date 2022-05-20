using System;
using System.Collections.Generic;
using Assignment4.ViewModel;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Assignment4.Pages
{
    public partial class EditAppointment : ContentPage
    {
        private Appointment appointment;
        private Appointment edittedAppointment;
        public EditAppointment(Appointment selectedAppointment)
        {
            InitializeComponent();
            this.appointment = new Appointment();

            this.appointment.Name = selectedAppointment.Name;
            this.appointment.Description = selectedAppointment.Description;
            this.appointment.Id = selectedAppointment.Id;
            this.appointment.Start = selectedAppointment.Start;
            this.appointment.Stop = selectedAppointment.Stop;
            this.appointment.Priority = selectedAppointment.Priority;
            this.appointment.Attendees = selectedAppointment.Attendees;

            this.edittedAppointment = selectedAppointment;
            BindingContext = edittedAppointment;
        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            // revert properties back to original values
            edittedAppointment.Name = appointment.Name;
            edittedAppointment.Description = appointment.Description;
            edittedAppointment.Start = appointment.Start;
            edittedAppointment.Stop = appointment.Stop;
            edittedAppointment.Priority = appointment.Priority;
            edittedAppointment.Attendees = appointment.Attendees;

            Navigation.PopModalAsync();
        }

        private async void Ok_Clicked(object sender, EventArgs e)
        {
            edittedAppointment = JsonConvert.DeserializeObject<Appointment>(await new WebRequestHandler().Post("http://localhost:5000/items/addorupdateappointment", edittedAppointment));
            await Navigation.PopModalAsync();
        }

        private void OnStartSelected(object sender, EventArgs e)
        {
            edittedAppointment.Start = (sender as DatePicker).Date;
        }

        private void OnStopSelected(object sender, EventArgs e)
        {
            edittedAppointment.Stop = (sender as DatePicker).Date;
        }

        private void OnPrioritySelected(object sender, EventArgs e)
        {
            if ((sender as Picker).SelectedItem.ToString() == "High")
                edittedAppointment.Priority = Item.PriorityLevel.High;

            else if ((sender as Picker).SelectedItem.ToString() == "Medium")
                edittedAppointment.Priority = Item.PriorityLevel.Medium;

            else if ((sender as Picker).SelectedItem.ToString() == "Low")
                edittedAppointment.Priority = Item.PriorityLevel.Low;

            else if ((sender as Picker).SelectedItem.ToString() == "None")
                edittedAppointment.Priority = Item.PriorityLevel.None;

            else if ((sender as Picker).SelectedItem == null)
                edittedAppointment.Priority = Item.PriorityLevel.None;
        }

        private void Delete_Clicked(object sender, EventArgs e)
        {

        }
    }
}
