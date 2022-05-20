using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Assignment4.ViewModel
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Appointment : Item
    {
        private DateTime start;

        [JsonProperty]
        public DateTime Start
        {
            get { return start; }
            set
            {
                start = value;
                NotifyPropertyChanged();
            }
        }

        private DateTime stop;

        [JsonProperty]
        public DateTime Stop
        {
            get { return stop; }
            set
            {
                stop = value;
                NotifyPropertyChanged();
            }
        }

        private List<String> attendees;

        [JsonProperty]
        public List<String> Attendees
        {
            get { return attendees; }
            set
            {
                attendees = value;
                NotifyPropertyChanged();
            }
        }

        public override string ToString()
        {
            return this.Name + " - " + this.Description + " - " + this.Priority;
        }
    }
}
