using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBudgetCommander.Models
{
    public class DataAnalyzer
    {
        private TransactionCollection transactions;

        public DataAnalyzer(TransactionCollection transactions)
        {
            this.transactions = new TransactionCollection(transactions);
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
    }
}
