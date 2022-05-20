using System;
using System.Collections.Generic;
using Assignment4.ViewModel;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Assignment4.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditTask : ContentPage
    {
        private Task task;
        private Task edittedTask;
        public EditTask(Task selectedTask)
        {
            InitializeComponent();
            this.task = new Task();

            this.task.Name = selectedTask.Name;
            this.task.Description = selectedTask.Description;
            this.task.Deadline = selectedTask.Deadline;
            this.task.Id = selectedTask.Id;
            this.task.IsCompleted = selectedTask.IsCompleted;
            this.task.Priority = selectedTask.Priority;

            this.edittedTask = selectedTask;
            BindingContext = edittedTask;
        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            // revert properties back to original values
            edittedTask.Name = task.Name;
            edittedTask.Description = task.Description;
            edittedTask.Deadline = task.Deadline;
            edittedTask.IsCompleted = task.IsCompleted;
            edittedTask.Priority = task.Priority;

            Navigation.PopModalAsync();
        }

        private async void Ok_Clicked(object sender, EventArgs e)
        {
            edittedTask = JsonConvert.DeserializeObject<Task>(await new WebRequestHandler().Post("http://localhost:5000/items/addorupdatetask", edittedTask));
            await Navigation.PopModalAsync();
        }

        private void OnDateSelected(object sender, EventArgs e)
        {
            edittedTask.Deadline = (sender as DatePicker).Date;
        }

        private void OnPrioritySelected(object sender, EventArgs e)
        {
            if ((sender as Picker).SelectedItem.ToString() == "High")
                edittedTask.Priority = Item.PriorityLevel.High;

            else if ((sender as Picker).SelectedItem.ToString() == "Medium")
                edittedTask.Priority = Item.PriorityLevel.Medium;

            else if ((sender as Picker).SelectedItem.ToString() == "Low")
                edittedTask.Priority = Item.PriorityLevel.Low;

            else if ((sender as Picker).SelectedItem.ToString() == "None")
                edittedTask.Priority = Item.PriorityLevel.None;

            else if ((sender as Picker).SelectedItem == null)
                edittedTask.Priority = Item.PriorityLevel.None;
        }

        private void Delete_Clicked(object sender, EventArgs e)
        {

        }
    }
}
