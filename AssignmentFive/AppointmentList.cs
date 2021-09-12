using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Collections;

namespace AssignmentFive
{
    [XmlRoot("AppointmentList")]
    public class AppointmentList : IEnumerable
    {
        [XmlArray("AppointmentArray")]
        [XmlArrayItem("Appointment", typeof(Appointment))]
        private List<Appointment> listAppObj = null;
        public List<Appointment> ScheduleListObj { get => listAppObj; set => listAppObj = value; }

        public AppointmentList()
        {
            listAppObj = new List<Appointment>();
        }

        public Appointment this[int i]
        {
            get => this.listAppObj[i]; set => this.listAppObj[i] = value;
        }
        public IEnumerator<Appointment> GetEnumerator()
        {
            return ((IEnumerable<Appointment>)listAppObj).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Appointment>)listAppObj).GetEnumerator();
        }
        public int Count
        {
            get => this.listAppObj.Count;
        }
        public void Add(Appointment appObj)
        {
            listAppObj.Add(appObj);
        }

        public void RemoveFromList(Appointment appObj)
        {
            listAppObj.Remove(appObj);
        }


        public void ClearList()
        {
            listAppObj.Clear();
        }
        public void Sort()
        {
            listAppObj.Sort();
        }
    }
}
