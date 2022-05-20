using System;
using System.ComponentModel;
using Newtonsoft.Json;

namespace Assignment4.ViewModel
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Task : Item
    {
        private DateTime deadline;

        [JsonProperty]
        public DateTime Deadline
        {
            get { return deadline; }
            set
            {
                deadline = value;
                NotifyPropertyChanged();
            }
        }

        private bool isCompleted = false;

        [JsonProperty]
        public bool IsCompleted
        {
            get { return isCompleted; }
            set
            {
                isCompleted = value;
                NotifyPropertyChanged();
            }
        }

        public override string ToString() {
            return this.Name + " - " + this.Description + " - " + this.Priority;
        }
    }
}
