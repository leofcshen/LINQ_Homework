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
    public partial class Frm作業_2_AdventureWorksDataSet : Form
    {
        public Frm作業_2_AdventureWorksDataSet()
        {
            InitializeComponent();
            productPhotoTableAdapter1.Fill(awDataSet1.ProductPhoto);
            dataGridView1.DataSource = bindingSource1;
            FillComboBox();
            this.comboBox2.SelectedIndex = 0;
        }

        private void FillComboBox()
        {
            var q = (from p in this.awDataSet1.ProductPhoto
                     orderby p.ModifiedDate
                     select p.ModifiedDate.Year).Distinct();
            foreach (var a in q)
                this.comboBox3.Items.Add(a);
            this.comboBox3.SelectedIndex = 0;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            pictureBox1.DataBindings.Clear();
            var q = from row in awDataSet1.ProductPhoto
                    select row;
            bindingSource1.DataSource = q.ToList();
            pictureBox1.DataBindings.Add("Image", bindingSource1, "LargePhoto", true);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.DataBindings.Clear();
            var q = from row in awDataSet1.ProductPhoto
                    where row.ModifiedDate > dateTimePicker1.Value && row.ModifiedDate < dateTimePicker2.Value
                    select row;
            bindingSource1.DataSource = q.ToList();
            pictureBox1.DataBindings.Add("Image", bindingSource1, "LargePhoto", true);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            pictureBox1.DataBindings.Clear();
            int year = int.Parse(comboBox3.Text);
            var q = from row in awDataSet1.ProductPhoto
                    where row.ModifiedDate.Year == year
                    select row;
            bindingSource1.DataSource = q.ToList();
            pictureBox1.DataBindings.Add("Image", bindingSource1, "LargePhoto", true);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            pictureBox1.DataBindings.Clear();
            int season = comboBox2.SelectedIndex + 1;
            var q = from row in awDataSet1.ProductPhoto
                    where row.ModifiedDate.Month > season * 3 - 3 && row.ModifiedDate.Month <= season * 3
                    select row;
            List<AWDataSet.ProductPhotoRow> list = q.ToList();
            bindingSource1.DataSource = list;
            label1.Text = $"共{list.Count}件商品";
            pictureBox1.DataBindings.Add("Image", bindingSource1, "LargePhoto", true);
        }
    }
}
