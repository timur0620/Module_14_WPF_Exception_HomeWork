using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankLib;

namespace Module_13_WPF_Delegat_HomeWork.Model
{
    class Manager: Client
    {
        public Manager() : this(0, "", "", "", "", "")
        {

        }
        public Manager(uint id, string lastName, string name, string surname,
                          string phoneNumber, string seriesPassportNumber)
        {
            this.Id = id;
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
        public List<Manager> GetAllClient()
        {
            List<Manager> templist = new List<Manager>();
            try
            {
                using (StreamReader sr = File.OpenText(Bank.GetPathClient()))
                {
                    string s = "";

                    while ((s = sr.ReadLine()) != null)
                    {
                        if (s != "")
                        {
                            Manager manager = new Manager();

                            string[] array = s.Split('|');
                            manager.Id = uint.Parse(array[0]);
                            manager.LastName = array[1];
                            manager.Name = array[2];
                            manager.Surname = array[3];
                            manager.PhoneNumber = array[4];
                            manager.SeriesPassportNumber = array[5];

                            templist.Add(manager);
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                return templist;   
            }
            return templist;
        }
        public void RecordClients(List<Manager> allClients)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(Bank.GetPathClient()))
                {
                    for (int i = 0; i < allClients.Count; i++)
                    {
                        sw.WriteLine($"{allClients[i].Id}|" +
                                    $"{allClients[i].LastName}|" +
                                    $"{allClients[i].Name}|" +
                                    $"{allClients[i].Surname}|" +
                                    $"{allClients[i].PhoneNumber}|" +
                                    $"{allClients[i].SeriesPassportNumber}|");
                    }
                }
            }
            catch(FileNotFoundException) 
            {
                return;
            }

        }
        public void UpdateAllData(string id, string lastName, string name, string surname,
                                  string phoneNumber, string seriesPassportNumber)
        {
            Manager manager = new Manager();
            List<Manager> allClient = manager.GetAllClient();
            var result = allClient.Find(e => e.Id == uint.Parse(id));

            result.Id = uint.Parse(id);
            result.LastName = lastName;
            result.Name = name;
            result.Surname = surname;
            result.PhoneNumber = phoneNumber;
            result.SeriesPassportNumber = seriesPassportNumber;

            int index = Convert.ToInt16(result.Id);

            allClient.RemoveAt(index - 1);

            allClient.Insert(index - 1, result);

            manager.RecordClients(allClient);
        }
        public void CreateNewClient(string lastName, string name, string surname,
                                    string phoneNumber, string seriesPassportNumber)
        {
            uint newId = GetCurrentId();

            using (StreamWriter sw = File.AppendText(Bank.GetPathClient()))
            {
                sw.Write($"\n{newId}|{lastName}|{name}|{surname}|{phoneNumber}|{seriesPassportNumber}|");
            }
        }
        public List<Manager> SearchClients(string lastName)
        {
            List<Manager> managers = GetAllClient();

            managers = managers.FindAll(e => e.LastName == lastName);

            return managers;
        }
    }
}

