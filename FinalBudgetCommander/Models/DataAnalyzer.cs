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


        public object ComputeBalance(string startDate, string endDate)
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

            return new {PlannedBalance = plannedBalance, Balance = balance, Days = (end - start).TotalDays};
        }

    }
}
