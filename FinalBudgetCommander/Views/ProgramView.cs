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
        }

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
            if (columnIndex == 3) { _collection.SortByCategory();}
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
    }
}
