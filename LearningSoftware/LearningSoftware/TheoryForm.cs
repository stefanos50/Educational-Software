using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LearningSoftware
{
    public partial class TheoryForm : Form
    {
        [DllImport("winmm.dll")]
        private static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);
        private string user_id;
        private HelpForm help_form;
        public TheoryForm(string user_id,string section)
        {
            InitializeComponent();
            load_sound_state();
            this.Icon = Properties.Resources.target_icon;
            this.user_id = user_id;

            if(Int32.Parse(section) == 1)
            {
                radioButton1.Checked = true;
            }else if (Int32.Parse(section) == 2)
            {
                radioButton2.Checked = true;
            }
            else if (Int32.Parse(section) == 3)
            {
                radioButton3.Checked = true;
            }
            else if (Int32.Parse(section) == 4)
            {
                radioButton4.Checked = true;
            }
            else if (Int32.Parse(section) == 5)
            {
                radioButton5.Checked = true;
            }
            else if (Int32.Parse(section) == 6)
            {
                radioButton6.Checked = true;
            }
            else if (Int32.Parse(section) == 7)
            {
                radioButton7.Checked = true;
            }
            else if (Int32.Parse(section) == 8)
            {
                radioButton8.Checked = true;
            }
            else if (Int32.Parse(section) == 9)
            {
                radioButton9.Checked = true;
            }
            else if (Int32.Parse(section) == 10)
            {
                radioButton10.Checked = true;
            }
            else if (Int32.Parse(section) == 11)
            {
                radioButton11.Checked = true;
            }  
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void TheoryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private bool views_exists(int choice)
        {

            var cs = "Host=localhost;Username=postgres;Password=3050;Database=LearningSoftwareDB";
            var con = new NpgsqlConnection(cs);
            con.Open();

            string sql = "Select * from Student_Traffic WHERE student_id=" + user_id + " and table_id=" + choice + ";";
            var cmd = new NpgsqlCommand(sql, con);
            NpgsqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                return true;
            }
            return false;

        }

        private void add_view(int selected_table_id)
        {
            var cs = "Host=localhost;Username=postgres;Password=3050;Database=LearningSoftwareDB";
            var con = new NpgsqlConnection(cs);
            con.Open();

            string sql = "UPDATE Student_Traffic SET table_views = table_views + 1 WHERE student_id = '" + user_id + "' and table_id='" + selected_table_id + "'; ";
            var cmd = new NpgsqlCommand(sql, con);
            var version = cmd.ExecuteScalar();

            if (!views_exists(selected_table_id))
            {
                string sq2 = "INSERT INTO Student_Traffic(student_id,table_id,table_views) VALUES('" + user_id + "','" + selected_table_id + "','1');";
                var cmd2 = new NpgsqlCommand(sq2, con);
                var version2 = cmd2.ExecuteScalar();

            }
            //Console.WriteLine(version.ToString());
        }

        private void close_button_Click(object sender, EventArgs e)
        {
            (new SoundPlayer(Properties.Resources.radio_button_sound)).Play();
            if (!(help_form == null))
            {
                help_form.Close();
            }
            MainForm mform = new MainForm();
            mform.Show();
            this.Hide();
        }

        private void sound_picture_Click(object sender, EventArgs e)
        {
            (new SoundPlayer(Properties.Resources.button_click_2)).Play();
            string current_state = File.ReadAllText("AudioSettings.txt");
            if (current_state.Equals("on"))
            {
                sound_picture.Image = Properties.Resources.no_sound_icon;
                mute();
            }
            else if (current_state.Equals("off"))
            {
                sound_picture.Image = Properties.Resources.sound_icon;
                unmute();
            }
        }
        public void load_sound_state()
        {
            string current_state = File.ReadAllText("AudioSettings.txt");
            if (current_state.Equals("on"))
            {
                sound_picture.Image = Properties.Resources.sound_icon;
                unmute();
            }
            else if (current_state.Equals("off"))
            {
                sound_picture.Image = Properties.Resources.no_sound_icon;
                mute();
            }
        }

        private void mute()
        {
            //string current_state = File.ReadAllText("AudioSettings.txt");
            System.IO.File.WriteAllText("AudioSettings.txt", "off");
            int NewVolume = 0; //set 0 to unmute
            uint NewVolumeAllChannels = (((uint)NewVolume & 0x0000ffff) | ((uint)NewVolume << 16));
            waveOutSetVolume(IntPtr.Zero, NewVolumeAllChannels);
        }
        private void unmute()
        {
            System.IO.File.WriteAllText("AudioSettings.txt", "on");
            int NewVolume = 65535; //set 65535 to unmute
            uint NewVolumeAllChannels = (((uint)NewVolume & 0x0000ffff) | ((uint)NewVolume << 16));
            waveOutSetVolume(IntPtr.Zero, NewVolumeAllChannels);
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton9.Checked)
            {
                (new SoundPlayer(Properties.Resources.radio_button_sound)).Play();
                multi_tables_image.Image = null;
                single_table_image.Visible = true;
                single_table_image.Image = Properties.Resources.table_9;
                table_image_title.Text = "9 Times Table";
                add_view(21);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                (new SoundPlayer(Properties.Resources.radio_button_sound)).Play();
                multi_tables_image.Image = null;
                single_table_image.Visible = true;
                single_table_image.Image = Properties.Resources.table_1;
                table_image_title.Text = "1 Times Table";
                add_view(13);
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                (new SoundPlayer(Properties.Resources.radio_button_sound)).Play();
                multi_tables_image.Image = null;
                single_table_image.Visible = true;
                single_table_image.Image = Properties.Resources.table_2;
                table_image_title.Text = "2 Times Table";
                add_view(14);
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                (new SoundPlayer(Properties.Resources.radio_button_sound)).Play();
                multi_tables_image.Image = null;
                single_table_image.Visible = true;
                single_table_image.Image = Properties.Resources.table_3;
                table_image_title.Text = "3 Times Table";
                add_view(15);
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                (new SoundPlayer(Properties.Resources.radio_button_sound)).Play();
                multi_tables_image.Image = null;
                single_table_image.Visible = true;
                single_table_image.Image = Properties.Resources.table_4;
                table_image_title.Text = "4 Times Table";
                add_view(16);
            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked)
            {
                (new SoundPlayer(Properties.Resources.radio_button_sound)).Play();
                multi_tables_image.Image = null;
                single_table_image.Visible = true;
                single_table_image.Image = Properties.Resources.table_5;
                table_image_title.Text = "5 Times Table";
                add_view(17);
            }
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton6.Checked)
            {
                (new SoundPlayer(Properties.Resources.radio_button_sound)).Play();
                multi_tables_image.Image = null;
                single_table_image.Visible = true;
                single_table_image.Image = Properties.Resources.table_6;
                table_image_title.Text = "6 Times Table";
                add_view(18);
            }
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton7.Checked)
            {
                (new SoundPlayer(Properties.Resources.radio_button_sound)).Play();
                multi_tables_image.Image = null;
                single_table_image.Visible = true;
                single_table_image.Image = Properties.Resources.table_7;
                table_image_title.Text = "7 Times Table";
                add_view(19);
            }
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton8.Checked)
            {
                (new SoundPlayer(Properties.Resources.radio_button_sound)).Play();
                multi_tables_image.Image = null;
                single_table_image.Visible = true;
                single_table_image.Image = Properties.Resources.table_8;
                table_image_title.Text = "8 Times Table";
                add_view(20);
            }
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton10.Checked)
            {
                (new SoundPlayer(Properties.Resources.radio_button_sound)).Play();
                multi_tables_image.Image = null;
                single_table_image.Visible = true;
                single_table_image.Image = Properties.Resources.table_10;
                table_image_title.Text = "10 Times Table";
                add_view(22);
            }
        }

        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton11.Checked)
            {
                (new SoundPlayer(Properties.Resources.radio_button_sound)).Play();
                multi_tables_image.Image = Properties.Resources.times_table;
                single_table_image.Visible = false;
                single_table_image.Image = null;
                table_image_title.Text = "Times Tables";
                if (!(user_id == "-1"))
                {
                    add_view(11);
                }
            }
        }
        private int total_views()
        {
            int total = 0;
            var cs = "Host=localhost;Username=postgres;Password=3050;Database=LearningSoftwareDB";
            var con = new NpgsqlConnection(cs);
            con.Open();

            string sql = "SELECT table_id,table_views FROM Student_Traffic WHERE student_id=" + user_id + " AND table_id>10 ORDER BY table_id ASC";
            var cmd = new NpgsqlCommand(sql, con);

            NpgsqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                if (rdr.GetInt32(0) == 12)
                {
                    continue;
                }
                total += rdr.GetInt32(1);
            }
            con.Close();
            return total;
        }
        private void load_views_table()
        {
            int total_views_count = total_views();
            var cs = "Host=localhost;Username=postgres;Password=3050;Database=LearningSoftwareDB";
            var con = new NpgsqlConnection(cs);
            con.Open();

            string sql = "SELECT table_id,table_views FROM Student_Traffic WHERE student_id=" + user_id + " AND table_id>10 ORDER BY table_id ASC";
            var cmd = new NpgsqlCommand(sql, con);

            NpgsqlDataReader rdr = cmd.ExecuteReader();
            Label[] view_labels = { view_11 , view_1, view_2, view_3, view_4, view_5, view_6, view_7, view_8, view_9, view_10};
            Label[] percent_labels = { percent_11, percent_1, percent_2, percent_3, percent_4, percent_5, percent_6, percent_7, percent_8, percent_9, percent_10 };
            for (int i = 0; i < percent_labels.Length; i++)
            {
                view_labels[i].Text = "0";
                percent_labels[i].Text = "0%";
            }
            int index = 0;
            while (rdr.Read())
            {
                if(rdr.GetInt32(0) == 12)
                {
                    continue;
                }
                view_labels[index].Text = rdr.GetInt32(1).ToString();
                percent_labels[index].Text = ((int)Math.Round((double)(100 * rdr.GetInt32(1)) / total_views_count)).ToString() + "%";
                index++;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            (new SoundPlayer(Properties.Resources.radio_button_sound)).Play();
            if (pictureBox1.Tag.ToString() == "right")
            {
                load_views_table();
                pictureBox1.Tag = "left";
                pictureBox1.Image = Properties.Resources.red_arrow_left;
                groupBox2.Visible = true;
                groupBox1.Visible = false;
            }else if (pictureBox1.Tag.ToString() == "left")
            {
                pictureBox1.Tag = "right";
                pictureBox1.Image = Properties.Resources.red_arrow_right;
                groupBox2.Visible = false;
                groupBox1.Visible = true;
            }
        }

        private void help_button_Click(object sender, EventArgs e)
        {
            (new SoundPlayer(Properties.Resources.radio_button_sound)).Play();
            help_form = new HelpForm("-Press the sound icon to enable or disable the sound based on the current state." + System.Environment.NewLine + "-Press the 'X' button on the right top to close the APP." + "-Press the Close button to go back to the main form." + System.Environment.NewLine + "-Press the red arrow to switch between section selection and traffic table." + System.Environment.NewLine + "-Press one of the radio buttons (on the section selection group) for each section to see the theory of that section." + System.Environment.NewLine + "-The traffic table shows how many times you viewed each section theory." + System.Environment.NewLine + "-SA on the traffic table refers to section all tables.");
            help_form.Show();
        }
    }
}
