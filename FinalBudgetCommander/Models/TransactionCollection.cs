using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Remoting.Messaging;

namespace FinalBudgetCommander.Models
{
    public class TransactionCollection : IEnumerable
    {
        private List<Transaction> transactions;

        public TransactionCollection()
        {
            this.transactions = new List<Transaction>();
        }

        public TransactionCollection(TransactionCollection tCollection)
        {
            this.transactions = new List<Transaction>();
            foreach (Transaction t in tCollection)
            {
                this.transactions.Add(t);
            }
        }

        public Transaction Transaction
        {
            get => default(Transaction);
            set
            {
            }
        }

        internal CollectionEnumerator CollectionEnumerator
        {
            get => default(CollectionEnumerator);
            set
            {
            }
        }

        public void Add(Transaction transaction)
        {
            this.transactions.Add(transaction);
        }

        public bool Remove(Transaction transaction)
        {
            foreach (Transaction t in transactions)
            {
                if (t.CompareTo(transaction) == 0)
                {
                    transactions.Remove(transaction);
                    return true;
                }
            }
            return false;
        }

        public Transaction Find(Transaction transaction)
        {
            return transactions.Find(t => transaction.CompareTo(t) == 0);
        }

        public void Clear()
        {
            this.transactions = new List<Transaction>();
        }

        public void SortByCategory()
        {
            var sorted = from transaction in transactions
                orderby transaction.Category
                select transaction;

            Clear();

            foreach (Transaction t in sorted)
            {
                transactions.Add(t);
            }
        }

        public void Print()
        {
            foreach (Transaction t in transactions)
            {
                Console.WriteLine(t.ToString());
            }
            Console.WriteLine();
        }

        public string[] GetStrings()
        {
            int length = transactions.Count;
            string[] data = new string[length];
            int i = 0;

            foreach (Transaction t in transactions)
            {
                data[i] = t.ToString();
                i++;
            }

            return data;
        }

        public void Save()
        {
            const string fileName = "transactions.txt";

            Save(fileName);
        }

        public void Save(string fileName)
        {
            const string path = @"D:\Git\FinalBudgetCommander\FinalBudgetCommander\Files\";

            try
            {
                File.WriteAllLines(path + fileName, GetStrings());
                Console.WriteLine("File has been saved");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Load(string path = @"D:\Git\FinalBudgetCommander\FinalBudgetCommander\Files\transactions.txt")
        {
            try
            {
                string[] loadedData = File.ReadAllLines(path);
                Transaction t;

                foreach (string data in loadedData)
                {
                    string[] args = data.Split('|');
                    if (args.Length == 5)
                    {
                        string name = args[0];
                        double value = Double.Parse(args[1]);
                        string date = args[2];
                        string category = args[3];
                        bool isPlanned = bool.Parse(args[4]);
                        t = new Transaction(name, value, date, category, isPlanned);
                    }
                    else
                    {
                        t = new Transaction();
                    }
                    
                    transactions.Add(t);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("File does not exist!");
                throw;
            }
        }

        public IEnumerator GetEnumerator()
        {
            return new CollectionEnumerator(transactions.ToArray());
        }

        public string[] GetCategories()
        {
            HashSet<string> set = new HashSet<string>();
            
            foreach (Transaction t in transactions)
            {
                set.Add(t.Category);
            }
            return set.ToArray();
        }

    }

    class CollectionEnumerator : IEnumerator
    {
        private readonly Transaction[] _transactions;
        private int index;

        public CollectionEnumerator(Transaction[] transactions)
        {
            _transactions = transactions;
            index = -1;
        }

        public object Current => _transactions[index];

        public bool MoveNext()
        {
            index++;
            return _transactions.Length > index;
        }

        public void Reset()
        {
            index = -1;
        }
    }

}
