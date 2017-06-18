using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FinalBudgetCommander.Models;

namespace FinalBudgetCommander
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
            Test();
        }

        public void Test()
        {
            TransactionCollection collection = new TransactionCollection();
            Transaction t1 = new Transaction("Salary", 3219.56, DateTime.Now.ToShortDateString(), "Work", true);
            Transaction t2 = new Transaction("Salary", 3229.56, @"7/10/2017", "Work", true);
            Transaction t3 = new Transaction("Salary", 3419.76, @"8/10/2017", "Work", true);
            Transaction t4 = new Transaction("Salary", 3219.56, @"9/10/2017", "Work", true);

            Transaction t5 = new Transaction("Food", -320.50, @"9/10/2017", "Food", true);
            Transaction t6 = new Transaction("Electricity bills", -400.39, @"9/10/2017", "Bills", true);
            Transaction t7 = new Transaction("Beer with friends", -20.0, @"9/10/2017", "Entertainment", false);
            Transaction t8 = new Transaction("Books", -19.56, @"9/10/2017", "Education", true);
            Transaction t9 = new Transaction("Phone bills", -219.56, @"9/10/2017", "Bills", true);

            collection.Add(t1);
            collection.Add(t2);
            collection.Add(t3);
            collection.Add(t4);
            collection.Add(t5);
            collection.Add(t6);
            collection.Add(t7);
            collection.Add(t8);
            collection.Add(t9);
            collection.Print();
            collection.Save();

            DataAnalyzer analyzer = new DataAnalyzer(collection);
            Console.WriteLine(analyzer.ComputeBalance("6/10/2017", "9/11/2017"));
        }
    }
}
