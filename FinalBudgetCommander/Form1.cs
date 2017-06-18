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
            collection.Load();
            collection.Print();
            

            DataAnalyzer analyzer = new DataAnalyzer(collection);
            Console.WriteLine(analyzer.ComputeBalance("6/1/2017", "6/30/2017").PlannedBalance);
            Console.WriteLine(analyzer.Noitify(analyzer.ComputeBalance("6/1/2017", "6/30/2017")));

            Console.WriteLine("Categories:");
            foreach (string category in collection.GetCategories())
            {
                Console.WriteLine(category);
            }

            var data = analyzer.ComputeDataForEachMonth();
            foreach (Data d in data)
            {
                Console.WriteLine(d);
            }

        }
    }
}
