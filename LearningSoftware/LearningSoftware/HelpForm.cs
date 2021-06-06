using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LearningSoftware
{
    public partial class HelpForm : Form
    {
        private string text_to_show;
        public HelpForm(string text_to_show)
        {
            InitializeComponent();
            this.Icon = Properties.Resources.target_icon;
            this.text_to_show = text_to_show;
            label1.Text = text_to_show;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void close_button_Click(object sender, EventArgs e)
        {
            (new SoundPlayer(Properties.Resources.radio_button_sound)).Play();
            this.Close();           
        }
    }
}
