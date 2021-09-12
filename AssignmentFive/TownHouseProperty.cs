using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentFive
{
    public class TownHouseProperty : Customer
    {
        private bool isNeighborConsent;
        public bool IsNeighborConsent { get => isNeighborConsent; set => isNeighborConsent = value; }
        public TownHouseProperty() { }
        public TownHouseProperty(string name, decimal sizeOfLot, string sizeOfWorkingArea, string creditCard, bool isNeighborConsent) : base(name, sizeOfLot, sizeOfWorkingArea, creditCard)
        {
            this.isNeighborConsent = isNeighborConsent;
        }
        public override void Requirement()
        {
            Console.WriteLine("Appointment Fixed ");
        }
        public override string ToString()
        {
            return string.Format($"{CustName} for total size of lot {SizeOfLot} acre, area available For Work is {SizeOfWorkArea} acre, " +
                $"{CustName} credit Card number is {CreditCard} and Neighbor's concent {(isNeighborConsent ? "is taken" : "is not taken")}");
        }
    }
}
