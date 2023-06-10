using System;
using System.Windows.Forms;

namespace User_Controllers
{
    public partial class DateComboBoxForm : Form
    {
        public DateComboBoxForm()
        {
            InitializeComponent();

            dateComboBoxLabel.Text = dateComboBox1.Value.ToString("dd MMMM yyyy dddd");
        }

        private void DateComboBox1_ValueChanged(object sender, EventArgs e)
        {
            dateComboBoxLabel.Text = dateComboBox1.Value.ToString("dd MMMM yyyy dddd");
        }
    }
}