using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBudgetCommander.Models
{
    public class Transaction : IComparable
    {
        public string Name { get; set; }
        public double Value { get; set; }
        public string Date { get; set; }
        public string Category { get; set; }
        public bool IsPlanned { get; set; }

        public Transaction(string name, double value, string date, string category, bool isPlanned)
        {
            this.Name = name;
            this.Value = value;
            this.Date = date; //    6/18/2017
            this.Category = category;
            this.IsPlanned = isPlanned;
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            Transaction cmpTransaction = obj as Transaction;
            if (cmpTransaction != null)
            {
                return this.Name.CompareTo(cmpTransaction.Name);
            }
            else
            {
                throw new ArgumentException("Object is not a Transaction");
            }
        }

        public override string ToString()
        {
            return String.Format("{0}|{1}|{2}|{3}|{4}", Name, Value, Date, Category, IsPlanned);
        }
    }
}
