using FinalBudgetCommander.Models;
using System;
using System.Windows.Forms;

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


        #endregion

        #region AddTransactionView

        private void InitializeAddTranasctionView()
        {
            nameTextBox.Text = "Default transaction";
            valueTextBox.Text = "10.50";
            
        }

        private void checkButton_Click(object sender, EventArgs e)
        {
            addButton.Enabled = true;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Added");
        }


        #endregion


    }
}
