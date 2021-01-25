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
    public partial class Frm考試 : Form
    {
        NorthwindEntities nwEntities = new NorthwindEntities();
        public Frm考試()
        {
            InitializeComponent();
        }

        private void button34_Click(object sender, EventArgs e)
        {
            var q = from od in nwEntities.Order_Details
                    group (float)(od.UnitPrice * od.Quantity) * (1.0 - od.Discount)
                    by od.Order.OrderDate.Value.Year into g
                    orderby g.Key
                    select new
                    {
                        Year = g.Key,
                        Total = g.Sum()
                    };
            
            dataGridView1.DataSource = q.ToList();
            chart1.DataSource = q.ToList();
            chart1.Series[0].XValueMember = "Year";
            chart1.Series[0].YValueMembers = "Total";
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var q = (from od in nwEntities.Order_Details
                    group (float)(od.UnitPrice * od.Quantity) * (1.0 - od.Discount)
                    by od.Order.OrderDate.Value.Year into g
                    orderby g.Key
                    select new
                    {
                        g.Key,
                        Sum = g.Sum()
                    }).ToList();
            
            var q2 = (from i in Enumerable.Range(0, q.Count)
                     select new
                     {
                         Year = q[i].Key,
                         營收 = q[i].Sum,
                         成長率 = (i - 1 >= 0) ? ((q[i].Sum - q[i - 1].Sum) / q[i - 1].Sum)*100 : 0
                     }).ToList();
                        
            dataGridView1.DataSource = q2;
            chart1.DataSource = q2;
            chart1.Series[0].XValueMember = "Year";
            chart1.Series[0].YValueMembers = "成長率";
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
        }

        private void button35_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            int[] nums = new int[33];
            for (int i = 0; i <= nums.Length - 1; i++)
                nums[i] = r.Next(1, 11);
            var q = from n in nums.AsEnumerable()
                    group n by n into g
                    orderby g.Key ascending
                    select new
                    {
                        Mykey = g.Key,
                        次數 = g.Count(),
                        出現率百分比 = $"{((float)g.Count() / (float)nums.Length *100):f2}"
                    };
            this.dataGridView1.DataSource = q.ToList();

            this.chart1.DataSource = q.ToList();
            this.chart1.Series[0].XValueMember = "MyKey";
            this.chart1.Series[0].YValueMembers = "出現率百分比";
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            this.chart1.Series[0].Name = "數字出現率";
        }        
    }
}
