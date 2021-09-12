using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentFive
{
    public class Appointment
    {
        private string timeSlot;
        private Customer customer;

        public string TimeSlot { get => timeSlot; set => timeSlot = value; }
        public Customer Customer { get => customer; set => customer = value; }
    }
}
