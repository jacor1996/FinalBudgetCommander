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
            Transaction t1 = new Transaction("d", 3219.56, DateTime.Now.ToShortDateString(), "aWork", true);
            Transaction t2 = new Transaction("a", 3229.56, DateTime.Now.ToShortDateString(), "zWork", true);
            Transaction t3 = new Transaction("z", 3419.76, DateTime.Now.ToShortDateString(), "gWork", true);
            Transaction t4 = new Transaction("b", 3219.56, @"11/19/2017", "Work", true);

            collection.Add(t1);
            collection.Add(t2);
            collection.Add(t3);
            collection.Add(t4);
            collection.Print();
            collection.Remove(t2);
            collection.Print();
            collection.SortByCategory();
            collection.Print();
            collection.Save();
        }
    }
}
