using System;
using System.Collections.Generic;
using Assignment4.ViewModel;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Assignment4.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddAppointmentDialogue : ContentPage
    {
        private ICollection<Appointment> AppointmentList;
        public AddAppointmentDialogue(ICollection<Appointment> AppointmentList)
        {
            InitializeComponent();
            this.AppointmentList = AppointmentList;
            BindingContext = new Appointment();
        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private async void Ok_Clicked(object sender, EventArgs e)
        {
            var thisAppointment = BindingContext as Appointment;
            thisAppointment = JsonConvert.DeserializeObject<Appointment>(await new WebRequestHandler().Post("http://localhost:5000/items/addorupdateappointment", thisAppointment));
            AppointmentList.Add(thisAppointment);

            await Navigation.PopModalAsync();
        }

        private void OnPrioritySelected(object sender, EventArgs e)
        {
            if ((sender as Picker).SelectedItem.ToString() == "High")
                (BindingContext as Appointment).Priority = Item.PriorityLevel.High;

            else if ((sender as Picker).SelectedItem.ToString() == "Medium")
                (BindingContext as Appointment).Priority = Item.PriorityLevel.Medium;

            else if ((sender as Picker).SelectedItem.ToString() == "Low")
                (BindingContext as Appointment).Priority = Item.PriorityLevel.Low;

            else if ((sender as Picker).SelectedItem.ToString() == "None")
                (BindingContext as Appointment).Priority = Item.PriorityLevel.None;

            else if ((sender as Picker).SelectedItem == null)
                (BindingContext as Appointment).Priority = Item.PriorityLevel.None;
        }
    }
}
