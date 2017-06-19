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
using FinalBudgetCommander.Views;

namespace FinalBudgetCommander
{

    public partial class MainMenu : Form
    {
        private TransactionCollection collection;
        private DataAnalyzer analyzer;


        public MainMenu()
        {
            InitializeComponent();
            Test();
        }

        public void Test()
        {
            collection = new TransactionCollection();
            collection.Load();
            //collection.Print();
            

            analyzer = new DataAnalyzer(collection);
            //Console.WriteLine(analyzer.ComputeBalance("6/1/2017", "6/30/2017").PlannedBalance);
            //Console.WriteLine(analyzer.Noitify(analyzer.ComputeBalance("6/1/2017", "6/30/2017")));

            //Console.WriteLine("Categories:");
            //foreach (string category in collection.GetCategories())
            //{
            //    Console.WriteLine(category);
            //}

            //var data = analyzer.ComputeDataForEachMonth();
            //foreach (Data d in data)
            //{
            //    Console.WriteLine(d);
            //}

        }

        private void startButton_Click(object sender, EventArgs e)
        {
            Form programView = new ProgramView(collection);
            programView.ShowDialog();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void optionsButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Dodaj opcje", "Options");
        }
    }
}
