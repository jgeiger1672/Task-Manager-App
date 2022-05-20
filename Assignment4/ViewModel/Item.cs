using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace Assignment4.ViewModel
{
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class Item : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string name;

        [JsonProperty]
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                NotifyPropertyChanged();
            }
        }

        private string description;

        [JsonProperty]
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                NotifyPropertyChanged();
            }
        }

        public enum PriorityLevel { High, Medium, Low, None }
        private PriorityLevel priority = PriorityLevel.None;

        [JsonProperty]
        public PriorityLevel Priority
        {
            get { return priority; }
            set
            {
                priority = value;
                NotifyPropertyChanged();
            }
        }

        private Guid id;

        [JsonProperty]
        public Guid Id
        {
            get { return id; }
            set
            {
                id = value;
                NotifyPropertyChanged();
            }
        }
    }
}
