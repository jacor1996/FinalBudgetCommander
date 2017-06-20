using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBudgetCommander.Models
{
    public class DataAnalyzer
    {
        private readonly TransactionCollection transactions;

        public DataAnalyzer(TransactionCollection transactions)
        {
            this.transactions = new TransactionCollection(transactions);
        }

        public Data Data
        {
            get => default(Data);
            set
            {
            }
        }

        public Data ComputeBalance(string startDate, string endDate)
        {
            DateTime start = DateTime.Parse(startDate);
            DateTime end = DateTime.Parse(endDate);
            double plannedBalance = 0;
            double balance = 0;

            foreach (Transaction t in transactions)
            {
                DateTime current = DateTime.Parse(t.Date);

                if (current.CompareTo(start) >= 0 && current.CompareTo(end) < 0)
                {
                    balance += t.Value;
                    if (t.IsPlanned)
                    {
                        plannedBalance += t.Value;
                    }
                }
            }

            return new Data(plannedBalance, balance, startDate, endDate);
        }


        public Data[] ComputeDataForEachMonth(int offset = -3)
        {
            Data[] datas = new Data[12];

            DateTime now = DateTime.Now;
            for (int i = 0; i < 12; i++)
            {
                DateTime start = now.AddMonths(offset + i);
                string _start = start.ToShortDateString();
                string _end = start.AddMonths(1).ToShortDateString();
                datas[i] = ComputeBalance(_start, _end);
            }
            return datas;
        }

        public string Noitify(Data computedData)
        {
            string notification = String.Empty;
            if (computedData.PlannedBalance < 0 || computedData.Balance < 0)
            {
                notification = "Warning! You are in debt";
            }
            else
            {
                notification = "Everthing is ok";
            }
            return notification;
        }

    }

    public class Data
    {
        public double PlannedBalance { get; set; }
        public double Balance { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public Data(double plannedBalance, double balance, string startDate, string endDate)
        {
            PlannedBalance = plannedBalance;
            Balance = balance;
            StartDate = startDate;
            EndDate = endDate;
        }

        public override string ToString()
        {
            return string.Format("{0}|{1}|{2}|{3}", PlannedBalance, Balance, StartDate, EndDate);
        }
    }
}
