using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentFive
{
    public class StandAloneProperty : Customer
    {
        private bool isEnteranceClear;

        public bool IsEnteranceClear { get => isEnteranceClear; set => isEnteranceClear = value; }
        public StandAloneProperty() { }
        public StandAloneProperty(string name, decimal sizeOfLot, string sizeOfWorkingArea, string creditCard, bool isEnteranceClear) : base(name, sizeOfLot, sizeOfWorkingArea, creditCard)
        {
            this.isEnteranceClear = isEnteranceClear;
        }
        public override void Requirement()
        {
            Console.WriteLine("Appointment Fixed");
        }

        public override string ToString()
        {
            return string.Format($"{CustName} for total size of lot {SizeOfLot} acre, area available For Work is {SizeOfWorkArea} acre, " +
                $"{CustName} credit Card number is {CreditCard} and enterance is {(isEnteranceClear ? "cleared" : "Not cleared")}");
        }
    }
}
