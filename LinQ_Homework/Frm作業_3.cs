using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinQ_Homework
{
    public partial class Frm作業_3 : Form
    {
        NorthwindEntities nwEntities = new NorthwindEntities();
        public Frm作業_3()
        {
            InitializeComponent();
            this.ordersTableAdapter1.Fill(this.nwDataSet1.Orders);
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int[] scores = { 90, 100, 45 };

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[] scores = { 20, 40, 60, 80, 100, 90, 70, 50, 30, 10 };
            List<int> listH = new List<int>();
            List<int> listM = new List<int>();
            List<int> listL = new List<int>();

            for (int i = 0; i < scores.Length - 1; i++)
            {
                if (scores[i] >= 80)
                    listH.Add(scores[i]);
                else if (scores[i] >= 60)
                    listM.Add(scores[i]);
                else
                    listL.Add(scores[i]);
            }
            string s1 = string.Empty, s2 = string.Empty, s3 = string.Empty;
            foreach (int i in listH)
                s1 += i + " ";
            foreach (int i in listM)
                s2 += i + " ";
            foreach (int i in listL)
                s3 += i + " ";
            MessageBox.Show("高分群(80以上)：" + s1 + "\n" +
                "中分群(60以上)：" + s2 + "\n" +
                "低分群(59以下)：" + s3);
        }

        private void button38_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            FileInfo[] files = dir.GetFiles();

            var q = from f in files
                    orderby f.Length descending
                    group f by MyFileSize((int)f.Length) into g                    
                    select new
                    {
                        MyKey = g.Key,
                        MyCount = g.Count(),
                        MyGroup = g
                    };

            this.dataGridView1.DataSource = q.ToList();

            foreach (var group in q)
            {
                string s = $"{group.MyKey} ({group.MyCount})";
                TreeNode x = this.treeView1.Nodes.Add(s);
                foreach (var item in group.MyGroup)
                    x.Nodes.Add(item.Name.ToString() + $" {item.Length} 位元組");
            }
        }
        string MyFileSize(int n)
        {
            if (n > 50000)
                return "超過50000";
            else if (n > 20000)
                return "超過20000";
            else
                return "低於20000";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            FileInfo[] files = dir.GetFiles();

            var q = from f in files
                    orderby f.CreationTime.Year descending
                    group f by MyFileYear(f.CreationTime.Year) into g
                    select new
                    {
                        MyKey = g.Key,
                        MyCount = g.Count(),
                        MyGroup = g
                    };

            this.dataGridView1.DataSource = q.ToList();

            foreach (var group in q)
            {
                string s = $"{group.MyKey} ({group.MyCount})";
                TreeNode x = this.treeView1.Nodes.Add(s);
                foreach (var item in group.MyGroup)
                    x.Nodes.Add(item.Name.ToString() + $" {item.CreationTime.Year} 年");
            }
        }
        string MyFileYear(int year)
        {
            if (year > 2019)
                return "2020";
            else if (year > 2018)
                return "2019";
            else
                return "2018前";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);

            var q = from p in this.nwDataSet1.Products
                    orderby p.UnitPrice
                    group p by MyProductPrice((int)p.UnitPrice) into g                    
                    select new
                    {
                        MyKey = g.Key,
                        MyCount = g.Count(),
                        MyGroup = g
                    };
            this.dataGridView1.DataSource = q.ToList();

            foreach (var group in q)
            {
                string s = $"{group.MyKey} ({group.MyCount})";
                TreeNode x = this.treeView1.Nodes.Add(s);
                foreach (var item in group.MyGroup)
                    x.Nodes.Add(item.ProductName + $" {item.UnitPrice:c2}");
            }
        }
        string MyProductPrice(int price)
        {
            if (price > 100)
                return "Over 100";
            else if (price > 10)
                return "Over 10";
            else
                return "Less thsn 10";
        }

        private void button15_Click(object sender, EventArgs e)
        {
            this.ordersTableAdapter1.Fill(this.nwDataSet1.Orders);
            var q = from o in this.nwDataSet1.Orders
                    orderby o.OrderDate descending
                    group o by o.OrderDate.Year into g
                    select new
                    {
                        Year = g.Key,
                        MyCount = g.Count(),
                        MyGroup = g
                    };
            this.dataGridView1.DataSource = q.ToList();

            foreach (var group in q)
            {
                string s = $"{group.Year} ({group.MyCount})";
                TreeNode x = this.treeView1.Nodes.Add(s);
                foreach (var item in group.MyGroup)
                    x.Nodes.Add(item.OrderID + $"-{item.OrderDate}");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            
            var q = from o in this.nwDataSet1.Orders
                    orderby o.OrderDate descending
                    group o by (o.OrderDate.Year, o.OrderDate.Month) into g
                    select new
                    {
                        YearMonth = g.Key,
                        MyCount = g.Count(),
                        MyGroup = g
                    };

            var qq = nwDataSet1.Orders.GroupBy(a => new { a.OrderDate.Year, a.OrderDate.Month });

            this.dataGridView1.DataSource = qq.ToList();
            foreach (var group in q)
            {
                string s = $"{group.YearMonth} ({group.MyCount})";
                TreeNode x = this.treeView1.Nodes.Add(s);
                foreach (var item in group.MyGroup)
                    x.Nodes.Add(item.OrderID + $"-{item.OrderDate}");
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            this.ordersTableAdapter1.Fill(this.nwDataSet1.Orders);

            var q = from o in this.nwDataSet1.Orders
                     orderby o.OrderDate ascending
                     group o by o.OrderDate.Year into g1
                     select new
                     {
                         year = g1.Key,
                         children = from i in g1
                                    group i by i.OrderDate.Month into g2
                                    select new
                                    {
                                        month = g2.Key,
                                        sidechildren = from i in g2
                                                       select i
                                    }
                     };

            foreach (var groupY in q)
            {
                string year = $"{groupY.year}年-共{groupY.children.Count():00}筆月份";
                TreeNode nodeYear = treeView1.Nodes.Add(year);
                foreach (var groupM in groupY.children)
                {
                    string month = $"{groupM.month:00}月-共{groupM.sidechildren.Count()}筆資料";
                    TreeNode NodesMonth = nodeYear.Nodes.Add(month);
                    foreach (var item in groupM.sidechildren)
                        NodesMonth.Nodes.Add(item.OrderID + " " + item.OrderDate);
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //this.order_DetailsTableAdapter1.Fill(this.nwDataSet1.Order_Details);
            //var q = from od in nwDataSet1.Order_Details
            //        select new { od.ProductID,od.UnitPrice,od.Quantity, TotalPrice = od.UnitPrice * od.Quantity };
            //this.dataGridView1.DataSource = q.ToList();
            //MessageBox.Show(this.nwDataSet1.Order_Details.Sum(n => n.UnitPrice * n.Quantity).ToString());
            var q = from o in nwEntities.Orders
                    from od in o.Order_Details
                    select (float)(od.UnitPrice * od.Quantity) * (1.0 - od.Discount);
            MessageBox.Show($"總額：{q.Sum():c2}");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var q = from od in nwEntities.Order_Details.AsEnumerable()
                    group (float)(od.UnitPrice * od.Quantity) * (1.0 - od.Discount) by od.Order.Employee.FirstName + " " + od.Order.Employee.LastName into g
                    orderby g.Sum() descending
                    select new
                    {
                        Name = g.Key,
                        Performance = $"{g.Sum():c2}"
                    };
            dataGridView1.DataSource = q.Take(5).ToList();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            bool flag = nwEntities.Products.Any(n => n.UnitPrice > 300);
            MessageBox.Show("是否有單價大於300的產品：" + flag);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var q = from p in nwEntities.Products
                    orderby p.UnitPrice descending
                    select new
                    {
                        Product = p.ProductName,
                        Category = p.Category.CategoryName,
                        Price = p.UnitPrice
                    };
            dataGridView1.DataSource = q.Take(5).ToList();
        }
    }
}
