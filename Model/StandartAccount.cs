using Bogus.DataSets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_13_WPF_Delegat_HomeWork.Model

{
    class StandartAccount
    {
        public uint Id { get; set; }
        public long AccountNumber { get; set; }
        public int TotalSum { get; set; }
        public uint IdClient { get; set; }
       
        public List<StandartAccount> AcStandartList { get; set; }
        public StandartAccount() : this(0, 0, 0, 0)
        {

        }
        public StandartAccount(uint id, long accountNumber, int totalSum, uint idClient)
        {
            this.Id = id;
            this.AccountNumber = accountNumber;
            this.TotalSum = totalSum;
            this.IdClient = idClient;

        }
        public override string ToString()
        {
 ;
            return $"{Id} {AccountNumber} {TotalSum} {IdClient}";
        }

        public void CreateAccount(string idClient, string sum, string typeAccount)
        {
            string path = "";
            try
            {
                if (typeAccount.Equals("standart"))
                {
                    path = BankLib.Bank.GetPathStandart();
                }
                else if (typeAccount.Equals("deposit"))
                {
                    DepositAccount account = new DepositAccount();

                    path = BankLib.Bank.GetPathDeposit() ;
                }
                else
                {
                    return;
                }
            }
            catch (Exception)
            {
                return ;
            }

            uint id = GetCurrentId(typeAccount);
            Random random = new Random();
            long accountNumber = random.Next(1_000_000, 9_000_000);

            if (idClient != null & sum != null & sum != "")
            {
                if (!File.Exists(path))
                {
                    StreamWriter sw = new StreamWriter(path);
                    sw.Close();
                }
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine($"{id}|{accountNumber}|{sum}|{idClient}|");
                }
            }

        }
        public static uint GetCurrentId(string typeAccount)
        {
            StandartAccount account = new StandartAccount();

            List<StandartAccount> db = account.GetAllAccounts(typeAccount);

            HashSet<uint> idHash = new HashSet<uint>();

            uint idCurrent = (uint)db.Count;

            foreach (var ac in db)
            {
                idHash.Add(ac.Id);
            }
            while (true)
            {
                idCurrent++;

                if (!idHash.Contains((uint)idCurrent))
                {
                    return idCurrent;
                }
                continue;
            }
        }
        public List<StandartAccount> GetAllAccounts(string typeAccount)
        {
            string path = "";
            List<StandartAccount> allAccounts = new List<StandartAccount>();

            if (typeAccount.Equals("standart"))
            {
                path = BankLib.Bank.GetPathStandart();
            }
            else if (typeAccount.Equals("deposit"))
            {
                DepositAccount account = new DepositAccount();

                path = BankLib.Bank.GetPathDeposit() ;
            }
            else
            {
                return allAccounts;
            }
            if (!File.Exists(path))
            {
                StreamWriter sw = new StreamWriter(path);
                sw.Close();
            }
            using (StreamReader sr = File.OpenText(path))
            {
                string s = "";

                while ((s = sr.ReadLine()) != null)
                {
                    StandartAccount account = new StandartAccount();

                    string[] array = s.Split('|');

                    account.Id = uint.Parse(array[0]);
                    account.AccountNumber = long.Parse(array[1]);
                    account.TotalSum = int.Parse(array[2]);
                    account.IdClient = uint.Parse(array[3]);

                    allAccounts.Add(account);
                }
            }
            return allAccounts;
        }
        public void TransferActive(string senderAcNum, string benifAcNum, string sum)
        {

            List<StandartAccount> standartAccounts = GetAllAccounts("standart");

            var standartSender = standartAccounts.Find(e => e.AccountNumber == long.Parse(senderAcNum));
            var standartBenif = standartAccounts.Find(e => e.AccountNumber == long.Parse(benifAcNum));

            bool done = false;
            if (standartSender != null & standartBenif != null)
            {
                if (standartSender.TotalSum - int.Parse(sum) > 0)
                {
                    standartSender.TotalSum -= int.Parse(sum);
                    standartBenif.TotalSum += int.Parse(sum);

                    done = true;
                }
            }
            if (done)
            {
                for (int i = 0; i < standartAccounts.Count; i++)
                {
                    int count = 0;
                    if (standartAccounts[i].Id == standartSender.Id)
                    {
                        standartAccounts[i] = standartSender;
                        count++;
                    }
                    if (standartAccounts[i].Id == standartBenif.Id)
                    {
                        standartAccounts[i] = standartBenif;
                        count++;
                    }
                    if (count == 2)
                    {
                        break;
                    }
                }
                RecordAccounts(standartAccounts);
            }
        }
        public void RecordAccounts(List<StandartAccount> allAccounts)
        {
            using (StreamWriter sw = new StreamWriter(BankLib.Bank.GetPathStandart()))
            {
                for (int i = 0; i < allAccounts.Count; i++)
                {
                    if (allAccounts[i] != null)
                    {
                        sw.WriteLine($"{allAccounts[i].Id}|" +
                        $"{allAccounts[i].AccountNumber}|" +
                        $"{allAccounts[i].TotalSum}|" +
                        $"{allAccounts[i].IdClient}|"
                        );
                    }
                }
            }
        }
        public List<StandartAccount> GetAllAccountsClientId(string idClient)
        {
            List<StandartAccount> standartAccounts = GetAllAccounts("standart");

            try
            {
                standartAccounts = standartAccounts.FindAll(e => e.IdClient == uint.Parse(idClient));
            }
            catch (Exception e)
            {
                return null;
            }
            return standartAccounts;
        }
        public void DeleteAccount(string accountId)
        {
            List<StandartAccount> standartAccounts = GetAllAccounts("standart");
            StandartAccount account = new StandartAccount();

            standartAccounts = standartAccounts.FindAll(e => e.Id != uint.Parse(accountId));

            RecordAccounts(standartAccounts);
        }
        public string LogingFunction(string user, string action)
        {
            return $"|User -->{user}\n|Action -->{action}\n|Date -->{DateTime.Now}\n";       
        }
    }
}
