using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;


namespace AssignmentFive
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Boolean[] errFlag = new Boolean[6] { true, true, true, true, true, true };
        AppointmentList appListObj = new AppointmentList();
        AppointmentList xmlListObj = new AppointmentList();
        Appointment[] appFinalArray = new Appointment[10];
        ArrayList arrayListObj = new ArrayList();
        /*        List<Shedule>  sheduleObj = new List<Shedule>();
        */
        string xmlFilePath = "appointments.xml";
        public ObservableCollection<Shedule> SheduleList { get; set; } = null;
        public Shedule Shedule { get; set; } = new Shedule();


        ICustomer cust = new TownHouseProperty();
        string addInfoValue = string.Empty;
        enum Properties
        {
            RuralProperty, StandAloneProperty, TownHouseProperty
        }
        public MainWindow()
        {
            InitializeComponent();
            FetchPropertyType();
            FetchAppointments();
            SheduleList = new ObservableCollection<Shedule>();
            DataContext = this;
            cbAppointment.SelectedIndex = 0;
            cbPropertyType.SelectedIndex = 0;
        }
        //---------------------------------------FETCH APPOINTMENTS----------------------------------------
        public void FetchAppointments()
        {
            string[] appArray = new string[10] { "7:00am", "7:30am", "8:00am", "8:30am", "9:00am", "9:30am", "10:00am", "10:30am", "11:00am", "11:30am"};
            for (int i = 0; i < appArray.Length; i++)
            {
                appFinalArray[i] = new Appointment();
                appFinalArray[i].TimeSlot = appArray[i];
                arrayListObj.Add(appArray[i]);
            }
            RemoveTimeSlot();
        }

        public void RemoveTimeSlot()
        {
            cbAppointment.ItemsSource = null;
            xmlListObj.ClearList();
            GetXmlData();
            foreach(var appointment in xmlListObj)
            {
                appListObj.Add(appointment);
                arrayListObj.Remove(appointment.TimeSlot);
            }
            cbAppointment.ItemsSource = arrayListObj;
            cbAppointment.SelectedIndex = 0;
        }

        //-----------------------------------------FETCH PROPERTY TYPE-------------------------------------
        public void FetchPropertyType()
        {

            var propertiesEnum = Enum.GetValues(typeof(Properties));
            foreach (var property in propertiesEnum)
            {
                cbPropertyType.Items.Add(property);
            }
        }
        //--------------------------------------------NAME VALIDATION--------------------------------------
        public void SetNameTextBox()
        {
            cust.CustName = tbCustName.Text;
            if (cust.ValidateCustomerName())
            {
                tbCustName.Foreground = Brushes.Black;
                tbCustName.BorderBrush = new SolidColorBrush(Colors.Black);
                lNameErr.Content = "";
                errFlag[0] = false;
            }
            else
            {
                tbCustName.BorderBrush = new SolidColorBrush(Colors.Red);
                tbCustName.Foreground = Brushes.Red;
                lNameErr.Content = "enter name in proper format";
                errFlag[0] = true;
            }

        }
        //---------------------------------------------SIZE OF LOT------------------------------------
        public void SetSizeOfLandTextBox()
        {
            string szOfLandString = tbSizeOfLand.Text;
            decimal szOfLlandDecimal = 0;
            decimal.TryParse(szOfLandString, out szOfLlandDecimal);
            cust.SizeOfLot = szOfLlandDecimal;
            if (cust.ValidateSizeOfLot())
            {
                tbSizeOfLand.Foreground = Brushes.Black;
                tbSizeOfLand.BorderBrush = new SolidColorBrush(Colors.Black);
                errFlag[1] = false;
                lLandSize.Content = "";
            }
            else
            {
                lLandSize.Content = "number less than 100";
                tbSizeOfLand.BorderBrush = new SolidColorBrush(Colors.Red);
                tbSizeOfLand.Foreground = Brushes.Red;
                errFlag[1] = true;
            }

        }
        //------------------------------------------SIZE OF WORK---------------------------------------
        public void SetSizeOfWorkTextBox()
        {
            cust.SizeOfWorkArea = tbSizeOfWork.Text;
            if (cust.ValidateSizeOfWork())
            {
                tbSizeOfWork.Foreground = Brushes.Black;
                tbSizeOfWork.BorderBrush = new SolidColorBrush(Colors.Black);
                errFlag[2] = false;
                lWorkSize.Content = "";
            }
            else
            {
                lWorkSize.Content = "number less than 100";
                tbSizeOfWork.BorderBrush = new SolidColorBrush(Colors.Red);
                tbSizeOfWork.Foreground = Brushes.Red;
                errFlag[2] = true;
            }
        }
        //---------------------------------------------CREDIT CARD-------------------------------------
        public void SetCreditCardTextBox()
        {
            cust.CreditCard = tbCreditCard.Text;
            if (cust.ValidCreditCardNumber())
            {
                tbCreditCard.Foreground = Brushes.Black;
                tbCreditCard.BorderBrush = new SolidColorBrush(Colors.Black);
                errFlag[3] = false;
                lCreditCard.Content = "";
            }
            else
            {
                lCreditCard.Content = "please enter valid credit card number";
                tbCreditCard.BorderBrush = new SolidColorBrush(Colors.Red);
                tbCreditCard.Foreground = Brushes.Red;
                errFlag[3] = true;
            }
        }
        //---------------------------------------ADDITIONAL INFO---------------------------------------
        public void SetAdditionalInfoRadio(RadioButton rb)
        {
            if ((rb.Name != "yes") && (rb.Name != "no"))
            {
                errFlag[4] = true;
            }
            else
            {
                errFlag[4] = false;
                addInfoValue = rb.Name;
            }
        }
        //-----------------------------------------BOOK APPOINTMENT------------------------------------
        public void BookAppointment()
        {
            SetCreditCardTextBox();
            SetNameTextBox();
            SetSizeOfLandTextBox();
            SetSizeOfWorkTextBox();
            if(errFlag[0] == true || errFlag[1] == true || errFlag[2] == true || errFlag[3] == true )
            {
                lBookApp.Foreground = Brushes.Red;
                lBookApp.Content = "Appointment not booked. Please fill all fields correctly.";
            }
            else
            {
                int index = cbAppointment.SelectedIndex;
                if (index == -1)
                {
                    MessageBox.Show("All Appointments are booked");
                }
                else
                {
                    appFinalArray[index].Customer = CreateNewAppointment();
                    appFinalArray[index].TimeSlot = cbAppointment.Text;
                    appListObj.Add(appFinalArray[index]);
                    WriteXmlData(appListObj);
                    appListObj.ClearList();
                    RemoveTimeSlot();
                    lBookApp.Foreground = Brushes.Black;
                    lBookApp.Content = "Appointment Booked.";
                }
            }
        }
        public Customer CreateNewAppointment()
        {
            Customer customer = null;
            string ccNumMaskString = cust.MaskCreditCardNum;

            switch (cbPropertyType.SelectedValue)
            {
                case Properties.RuralProperty:
                    customer = new RuralProperty(cust.CustName, cust.SizeOfLot, cust.SizeOfWorkArea, ccNumMaskString, (addInfoValue.ToUpper() == "YES") ? true : false);
                    break;
                case Properties.StandAloneProperty:
                    customer = new StandAloneProperty(cust.CustName, cust.SizeOfLot, cust.SizeOfWorkArea, ccNumMaskString, (addInfoValue.ToUpper() == "YES") ? true : false);
                    break;
                case Properties.TownHouseProperty:
                    customer = new TownHouseProperty(cust.CustName, cust.SizeOfLot, cust.SizeOfWorkArea, ccNumMaskString, (addInfoValue.ToUpper() == "YES") ? true : false);
                    break;
                default:
                    Console.WriteLine("Please select the service which is mentioned in the list.");
                    break;
            }
            return customer;
        }
        //-----------------------------------------DISPLAY LIST----------------------------------------
        public void DisplayList()
        {
            xmlListObj.ClearList();
            SheduleList.Clear();
            GetXmlData();
            //appointmentObj = xmlListObj;
            foreach (var xmlData in xmlListObj)
            {
                Shedule sheObj = new Shedule();
                sheObj.Time = xmlData.TimeSlot;
                sheObj.Name = xmlData.Customer.CustName;
                sheObj.Land = xmlData.Customer.SizeOfLot;
                sheObj.WorkArea = xmlData.Customer.SizeOfWorkArea;
                sheObj.CreditCard = xmlData.Customer.CreditCard;
                sheObj.AdditionalInfo = "Additional info";
                SheduleList.Add(sheObj);
            }
            Shedule sheObject = new Shedule();

            dgAppointmentList.ItemsSource = null;
            dgAppointmentList.ItemsSource = SheduleList;
        }

        public void SaveAppointment()
        {

        }
        //=============================================================================================
        private void NameTextBox(object sender, TextChangedEventArgs e)
        {
            SetNameTextBox();
        }
        private void SizeOfLandTextBox(object sender, TextChangedEventArgs e)
        {
            SetSizeOfLandTextBox();
        }
        private void SizeOfWorkTextBox(object sender, TextChangedEventArgs e)
        {
            SetSizeOfWorkTextBox();
        }
        private void CreditCardTextBox(object sender, TextChangedEventArgs e)
        {
            SetCreditCardTextBox();
        }      
        private void AdditionalInfoRadio(object sender, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            SetAdditionalInfoRadio(rb);
        }

        private void OnClickBookAppointment(object sender, RoutedEventArgs e)
        {
            BookAppointment();
        }
        private void OnClickDisplayList(object sender, RoutedEventArgs e)
        {
            dgAppointmentList.ItemsSource = null;
            DisplayList();

        }
        private void OnClickSearch(object sender, RoutedEventArgs e)
        {
            if (tbSearch.Text == null)
            {
                dgAppointmentList.ItemsSource = SheduleList;
            }
            else
            {
                var qryResult = from Shedule in SheduleList
                                where Shedule.Name == tbSearch.Text
                                select Shedule;
                dgAppointmentList.ItemsSource = qryResult;
            }
        }
        //---------------------------------SAVE BUTTON------------------------------
        private void OnClickSaveAppointment(object sender, RoutedEventArgs e)
        {
            SaveAppointment();
        }
        //===========================FILE METHODS===================================
        private void WriteXmlData(AppointmentList appList)
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(AppointmentList));
                TextWriter textWriter = new StreamWriter(xmlFilePath);
                xmlSerializer.Serialize(textWriter, appList);
                textWriter.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }

        private void GetXmlData()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(AppointmentList));
            if (File.Exists(xmlFilePath))
            {
                TextReader tr = new StreamReader(xmlFilePath);
                xmlListObj = (AppointmentList)xmlSerializer.Deserialize(tr);
                tr.Close();
            }
        }

        
    }
}
