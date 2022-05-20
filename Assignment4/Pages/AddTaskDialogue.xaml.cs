using System;
using System.Collections.Generic;
using Assignment4.ViewModel;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Assignment4.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTaskDialogue : ContentPage
    {
        private ICollection<Task> taskList;
        public AddTaskDialogue(ICollection<Task> taskList)
        {
            InitializeComponent();
            this.taskList = taskList;
            BindingContext = new Task();
        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private async void Ok_Clicked(object sender, EventArgs e)
        {
            var thisTask = BindingContext as Task;
            thisTask = JsonConvert.DeserializeObject<Task>(await new WebRequestHandler().Post("http://localhost:5000/items/addorupdatetask", thisTask));
            taskList.Add(thisTask);

            await Navigation.PopModalAsync();
        }

        
        private void OnDateSelected(object sender, EventArgs e)
        {
            (BindingContext as Task).Deadline = (sender as DatePicker).Date;
        }

        private void OnPrioritySelected(object sender, EventArgs e)
        {
            if ((sender as Picker).SelectedItem.ToString() == "High")
                (BindingContext as Task).Priority = Item.PriorityLevel.High;

            else if ((sender as Picker).SelectedItem.ToString() == "Medium")
                (BindingContext as Task).Priority = Item.PriorityLevel.Medium;

            else if ((sender as Picker).SelectedItem.ToString() == "Low")
                (BindingContext as Task).Priority = Item.PriorityLevel.Low;

            else if ((sender as Picker).SelectedItem.ToString() == "None")
                (BindingContext as Task).Priority = Item.PriorityLevel.None;

            else if ((sender as Picker).SelectedItem == null)
                (BindingContext as Task).Priority = Item.PriorityLevel.None;
        }
    }
}
