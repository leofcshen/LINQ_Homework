using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinQ_Homework
{
    public partial class Frm作業_1 : Form
    {
        public Frm作業_1()
        {
            InitializeComponent();
            this.ordersTableAdapter1.Fill(this.nwDataSet1.Orders);
            this.order_DetailsTableAdapter1.Fill(this.nwDataSet1.Order_Details);
            this.productsTableAdapter1.Fill(nwDataSet1.Products);
            this.bindingSource1.DataSource = this.nwDataSet1.Orders;
            FillComboBox();
            dataGridView1.CellClick += DataGridView1_SelectionChanged;
        }

        private void FillComboBox()
        {
            var q = (from o in this.nwDataSet1.Orders
                     select o.OrderDate.Year).Distinct();
            foreach (var a in q)
                this.comboBox1.Items.Add(a);
            this.comboBox1.SelectedIndex = 0;
        }
        private void button14_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dirs = new System.IO.DirectoryInfo(@"c:\windows");
            //System.IO.FileInfo[] files = dirs.GetFiles("*.log");
            //this.dataGridView1.DataSource = files;
            System.IO.FileInfo[] files = dirs.GetFiles();
            var q = from file in files
                    where file.Extension == ".log"
                    select file;
            dataGridView1.DataSource = q.ToList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 33, 100, 44, 89 , 90, 99};
            int maxNumber = 0;
            int minNumber = 999999;
            Max1(nums, ref maxNumber, ref minNumber);
            MessageBox.Show("For loop Max = " + maxNumber + ", Min = " + minNumber);
        }

        void Max1(int[] nums, ref int maxNumber, ref int minNumber)
        {
            for (int i = 0; i <= nums.Length - 1; i++)
            {
                maxNumber = (nums[i] > maxNumber) ? nums[i] : maxNumber;
                minNumber = (nums[i] < minNumber) ? nums[i] : minNumber;
            }
        }
        int pageCount = 1;

        private void button13_Click(object sender, EventArgs e)
        {
            pageCount += 1;
            int showCount = int.Parse(textBox1.Text);
            var pro = from row in nwDataSet1.Products
                      select row;
            dataGridView2.DataSource = pro.Skip(showCount * pageCount).Take(showCount).ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dirs = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dirs.GetFiles();
            var q = from s in files
                    where s.CreationTime.Year == 2020
                    select s;
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dirs = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dirs.GetFiles();
            var q = from s in files
                    where s.Length > 7000
                    select s;
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
            this.dataGridView1.DataSource = this.nwDataSet1.Orders;
            this.dataGridView1.DataSource = this.bindingSource1;            
            //this.dataGridView2.DataSource = this.nwDataSet1.Order_Details;
        }

        bool flag = true;
        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            try
            {                
                if (flag)
                {
                    var qq = from ss in nwDataSet1.Order_Details
                             where ss.OrderID == 10248
                             select ss;
                    flag = !flag;
                    dataGridView2.DataSource = qq.ToList();
                    return;
                }
                int a = (int)dataGridView1.Rows[bindingSource1.Position].Cells["OrderID"].Value;
                var q = from ss in nwDataSet1.Order_Details
                        where ss.OrderID == a
                        select ss;
                dataGridView2.DataSource = q.ToList();
            }
            catch (Exception ex)
            {
                //裝沒事
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var q = from s in nwDataSet1.Orders
                    where s.OrderDate.Year.ToString() == comboBox1.Text && !s.IsShipRegionNull()
                    select s;
            var qq = from ss in nwDataSet1.Order_Details
                     join s in nwDataSet1.Orders
                     on ss.OrderID equals s.OrderID
                     where s.OrderDate.Year.ToString() == comboBox1.Text
                     select ss;
            dataGridView1.DataSource = q.ToList();
            dataGridView2.DataSource = qq.ToList();
        }
        private void DataGridView1_SelectionChanged(object sender, DataGridViewCellEventArgs e)
        {
            int a = (int)dataGridView1.Rows[bindingSource1.Position].Cells["OrderID"].Value;
            var q = from ss in nwDataSet1.Order_Details
                    where ss.OrderID == a
                    select ss;
            dataGridView2.DataSource = q.ToList();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int a = (int)dataGridView1.CurrentCell.Value;
            MessageBox.Show(dataGridView1.CurrentRow.ToString());
            var q = from ss in nwDataSet1.Order_Details
                    where ss.OrderID == a
                    select ss;
            dataGridView2.DataSource = q.ToList();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            pageCount -= 1;
            if (pageCount < 0) pageCount = 0;
            int showCount = int.Parse(textBox1.Text);
            var pro = from row in nwDataSet1.Products
                      select row;
            dataGridView2.DataSource = pro.Skip(showCount * pageCount).Take(showCount).ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Frm作業_2_AdventureWorksDataSet form = new Frm作業_2_AdventureWorksDataSet();
            form.Show();
        }
        //int Max(int[] nums)
    }
}
