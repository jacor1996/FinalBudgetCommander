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
            string path = @"D:\Git\FinalBudgetCommander\FinalBudgetCommander\Files\";
            string fileName = "transactions.txt";

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

        public void Load()
        {
            
        }

        public IEnumerator GetEnumerator()
        {
            return new CollectionEnumerator(transactions.ToArray());
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
