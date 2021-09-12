using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentFive
{
    public class RuralProperty : Customer
    {
        private bool isWorkingAreaClean;

        public bool IsWorkingAreaClean { get => isWorkingAreaClean; set => isWorkingAreaClean = value; }

        public RuralProperty() { }
        public RuralProperty(string name, decimal sizeOfLot, string sizeOfWorkingArea, string creditCard, bool isWorkingAreaClean) : base(name, sizeOfLot, sizeOfWorkingArea, creditCard)
        {
            this.isWorkingAreaClean = isWorkingAreaClean;

        }
        public override void Requirement()
        {
            Console.WriteLine("Appointment Fixed");
        }
        public override string ToString()
        {
            return string.Format($"{CustName} for total size of lot {SizeOfLot} acre, area available For Work is {SizeOfWorkArea} acre, " +
                $"{CustName} credit Card number is {CreditCard} and working area is {(isWorkingAreaClean ? "clean" : "not clean")}");
        }
    }
}
