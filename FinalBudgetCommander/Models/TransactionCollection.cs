using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Remoting.Messaging;

namespace FinalBudgetCommander.Models
{
    public class TransactionCollection
    {
        private List<Transaction> transactions;

        public TransactionCollection()
        {
            this.transactions = new List<Transaction>();
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
    }
}
