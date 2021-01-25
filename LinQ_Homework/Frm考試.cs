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
            var q = from od in nwEntities.Order_Details
                    group (float)(od.UnitPrice * od.Quantity) * (1.0 - od.Discount)
                    by od.Order.OrderDate.Value.Year into g
                    orderby g.Key
                    select new
                    {
                        g.Key,
                        Sum = g.Sum()
                    };
            var list = q.ToList();
            var q2 = from i in Enumerable.Range(0, list.Count)
                     select new
                     {
                         Year = list[i].Key,
                         營收 = list[i].Sum,
                         成長率 = (i - 1 >= 0) ? ((list[i].Sum - list[i - 1].Sum) / list[i - 1].Sum)*100 : 0
                     };

            var list2 = q2.ToList();
            dataGridView1.DataSource = list2;
            chart1.DataSource = list2;
            chart1.Series[0].XValueMember = "Year";
            chart1.Series[0].YValueMembers = "成長率";
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
        }
    }
}
