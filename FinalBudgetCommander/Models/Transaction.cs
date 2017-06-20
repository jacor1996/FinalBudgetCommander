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

        public Transaction()
        {
            Name = String.Empty;
            Value = 0;
            Date = DateTime.Now.ToShortDateString();
            Category = String.Empty;
            IsPlanned = false;
        }

        public Transaction(string name, string value, string date, string category, string isPlanned)
        {
            Name = name;
            Value = Double.Parse(value);
            Date = date;
            Category = category;
            IsPlanned = bool.Parse(isPlanned);
        }

        public Transaction(string[] argStrings)
        {
            if (argStrings.Length != 5) return;
            Name = argStrings[0];
            Value = Double.Parse(argStrings[1]);
            Date = argStrings[2];
            Category = argStrings[3];
            IsPlanned = bool.Parse(argStrings[4]);
        }

        public static bool IsValid(string name, string value, string date, string category, string isPlanned)
        {
            bool _name;
            bool _category;
            double parsedValue;
            DateTime parsedDate;
            bool parsedIsPlanned;

            if (name != null)
            {
                _name = true;
            }
            else
            {
                _name = false;
            }

            var _value = Double.TryParse(value, out parsedValue);

            var _date = DateTime.TryParse(date, out parsedDate);

            if (category != null)
            {
                _category = true;
            }
            else
            {
                _category = false;
            }

            var _isPlanned = bool.TryParse(isPlanned, out parsedIsPlanned);

            return (_name && _value && _date && _category && _isPlanned);
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
                return this.ToString().CompareTo(cmpTransaction.ToString());
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
