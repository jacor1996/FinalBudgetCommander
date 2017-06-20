using FinalBudgetCommander.Models;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FinalBudgetCommander.Views
{
    public partial class ProgramView : Form
    {
        private TransactionCollection _collection;

        public ProgramView(TransactionCollection collection)
        {
            _collection = collection;
            InitializeComponent();
            InitializeTransactionsView();

            InitializeAddTranasctionView();
            Chart();
        }

        private void UpdateData(TransactionCollection collection)
        {
            _collection = collection;
            UpdateTransacstionsListView();
        }

        private void Chart()
        {
            chart1.Series.Add("Wykres");
            DataAnalyzer analyzer = new DataAnalyzer(_collection);
            Data[] data = analyzer.ComputeDataForEachMonth(-6);

            chart1.Series[0].YValuesPerPoint = 3;
            chart1.Series[0].MarkerStep = 3;
            

            foreach (var d in data)
            {
                string timeSpan = String.Format("{0} - {1}", d.StartDate, d.EndDate);
                chart1.Series[0].Points.AddXY(timeSpan, d.PlannedBalance, d.Balance);
                
                
            }
            
            
        }

        #region TransactionsListView
        private void InitializeTransactionsView()
        {
            transactionsListView.MultiSelect = false;
            transactionsListView.FullRowSelect = true;

            transactionsListView.ColumnClick += new ColumnClickEventHandler(Test);

            UpdateTransacstionsListView();
        }

        private void Test(object sender, ColumnClickEventArgs e)
        {
            int columnIndex = e.Column;
            if (columnIndex == 3) { _collection.SortByCategory(); }
            UpdateTransacstionsListView();
        }

        private void UpdateTransacstionsListView()
        {
            transactionsListView.Items.Clear();
            foreach (Transaction t in _collection)
            {
                ListViewItem item = new ListViewItem(t.Name);

                ListViewItem.ListViewSubItem[] items = new ListViewItem.ListViewSubItem[4];
                items[0] = new ListViewItem.ListViewSubItem(item, t.Value.ToString());
                items[1] = new ListViewItem.ListViewSubItem(item, t.Date);
                items[2] = new ListViewItem.ListViewSubItem(item, t.Category);
                items[3] = new ListViewItem.ListViewSubItem(item, t.IsPlanned.ToString());
                item.SubItems.AddRange(items);

                transactionsListView.Items.Add(item);
            }
        }

        private void deleteTransactionButton_Click(object sender, EventArgs e)
        {
            
            var data = transactionsListView.SelectedItems[0].SubItems;
            string[] args = new string[data.Count];

            int i = 0;
            foreach (ListViewItem.ListViewSubItem arg in data)
            {
                //Console.WriteLine(arg.Text);
                args[i] = arg.Text;
                i++;
            }

            Transaction newTransaction = new Transaction(args);
            //Console.WriteLine(newTransaction);
            _collection.Remove(_collection.Find(newTransaction));
            UpdateData(_collection);
        }

        #endregion

        #region AddTransactionView

        private void InitializeAddTranasctionView()
        {
            nameTextBox.Text = "Default transaction";
            valueTextBox.Text = "10.50";
            comboBoxCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxCategory.Items.AddRange(_collection.GetCategories());
            comboBoxCategory.SelectedIndex = 0;
            
        }


        //Events
        private void checkButton_Click(object sender, EventArgs e)
        {
            string[] userInput = ReadUserInput();

            if (Transaction.IsValid(userInput[0], userInput[1], userInput[2], userInput[3], userInput[4]))
            {
                addButton.Enabled = true;
                addButton.Focus();
            }

            
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            Transaction newTransaction = new Transaction(ReadUserInput());
            _collection.Add(newTransaction);
            MessageBox.Show("Added");
            UpdateData(_collection);
        }

        private void nameTextBox_Enter(object sender, EventArgs e)
        {
            LoseFocus();
        }


        private void LoseFocus()
        {
            addButton.Enabled = false;
        }

        //Events end

        private string[] ReadUserInput()
        {
            string[] userInput = new string[5];

            userInput[0] = nameTextBox.Text;
            userInput[1] = valueTextBox.Text;
            userInput[2] = dateTimePicker.Value.ToShortDateString();
            userInput[3] = comboBoxCategory.SelectedItem.ToString();
            userInput[4] = checkBoxIsPlanned.Checked.ToString();

            Console.WriteLine("Debug user input");
            foreach (var data in userInput)
            {
                Console.WriteLine(data);
            }

            return userInput;
        }


        //Add valid transaction


        #endregion

        private void ProgramView_FormClosing(object sender, FormClosingEventArgs e)
        {
            _collection.Save("test.txt");
        }

        
    }
}
