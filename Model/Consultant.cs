using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using BankLib;


namespace Module_13_WPF_Delegat_HomeWork.Model
{
    class Consultant: Client
    {
        public  new string SeriesPassportNumber { get { return "......"; } set { } }
        public  List<Consultant> consultantList { get; private set; }
        public Consultant() : this(0, "", "", "", "", "")
        {

        }
        public Consultant(uint idCon, string lastName, string name, string surname,
                          string phoneNumber, string seriesPassportNumber)
        {
            this.Id = idCon;
            this.LastName = lastName;
            this.Name = name;
            this.Surname = surname;
            this.PhoneNumber = phoneNumber;
            this.SeriesPassportNumber = seriesPassportNumber;
        }
        public override string ToString()
        {
            return $"{Id} {LastName} {Name} {Surname} {PhoneNumber} {SeriesPassportNumber}";
        }
        public  List<Consultant> GetAllClient()
        {
            List<Consultant> templist = new List<Consultant>();

            using (StreamReader sr = File.OpenText(Bank.GetPathClient()))
            {
                string s = "";

                while ((s = sr.ReadLine()) != null)
                {
                    Consultant consultant = new Consultant();

                    string[] array = s.Split('|');
                    consultant.Id = uint.Parse(array[0]);
                    consultant.LastName = array[1];
                    consultant.Name = array[2];
                    consultant.Surname = array[3];
                    consultant.PhoneNumber = array[4];
                    consultant.SeriesPassportNumber = array[5];

                    templist.Add(consultant);
                }
            }
            return templist;
        }
        public void UpdatePhone(string id, string newPhone)
        {
            Manager manager = new Manager();
            List<Manager> allClient = manager.GetAllClient();
            var result = allClient.Find(e => e.Id == uint.Parse(id));

            int myInt;
            bool isNumerical = int.TryParse(newPhone, out myInt);

            if (newPhone.Length > 8 & isNumerical)
            {
                result.PhoneNumber = newPhone;

                int index = Convert.ToInt16(result.Id);

                allClient.RemoveAt(index - 1);

                allClient.Insert(index - 1, result);

                manager.RecordClients(allClient);
            }
        }
        public List<Consultant> SearchClients(string lastName)
        {   
            List<Consultant> consultants = GetAllClient();

            consultants = consultants.FindAll(e => e.LastName == lastName);

            return consultants;
        }

    }


}
