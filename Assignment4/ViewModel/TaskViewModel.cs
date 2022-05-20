using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Transactions;

namespace Assignment4.ViewModel
{
    public class TaskViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<Task> TaskList { get; set; }
        public ObservableCollection<Appointment> AppointmentList { get; set; }

        public TaskViewModel()
        {
            TaskList = new ObservableCollection<Task>();
            AppointmentList = new ObservableCollection<Appointment>();
        }

        private Item selectedItem;

        public Item SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                NotifyPropertyChanged();
            }
        }

        public void Remove()
        {
            if (SelectedItem is Task)
                TaskList.Remove(SelectedItem as Task);
            else if (SelectedItem is Appointment)
                AppointmentList.Remove(SelectedItem as Appointment);
        }

        public static IEnumerable<Item> Search(ObservableCollection<Item> list, string searchterm)
        {
            // searches based on name or description
            var searchResults1 = list.Where(i => i.Name.Contains(searchterm)
                                              || i.Description.Contains(searchterm));

            // searches based on list of attendees
            var searchResults2 = list.OfType<Appointment>().Where(i => i.Attendees.Contains(searchterm));

            // results of name, description, and attendees
            var allResults = searchResults1.Concat(searchResults2);

            return allResults;
        }
    }
}
