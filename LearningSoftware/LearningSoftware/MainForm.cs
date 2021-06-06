using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace LearningSoftware
{
    public partial class MainForm : Form
    {
        [DllImport("winmm.dll")]
        private static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);
        private string choice = "null";
        private string user_id = "null";
        private int upper_skill = 600, bottom_skill = 400,questions=0;
        private HelpForm help_form;
        public MainForm()
        {
            InitializeComponent();
            load_sound_state();
            label_no_choose.Visible = false;
            this.Icon = Properties.Resources.target_icon;
            var cs = "Host=localhost;Username=postgres;Password=3050;Database=LearningSoftwareDB";

            var con = new NpgsqlConnection(cs);
            con.Open();

            string path = "UserID.txt";
            if (!File.Exists(path))
            {
                var sql = "INSERT INTO Users(name) VALUES('" + Environment.MachineName + "') RETURNING id; ";
                var cmd = new NpgsqlCommand(sql, con);
                var version = cmd.ExecuteScalar().ToString();
                File.Create(path).Dispose();
                File.WriteAllText(path, version);
                user_id = version;
            }
            else
            {
                user_id = File.ReadAllText(path);
            }
            load_performance_table();
            load_views_table();
        }

        private void load_performance_table()
        {
            var cs = "Host=localhost;Username=postgres;Password=3050;Database=LearningSoftwareDB";
            var con = new NpgsqlConnection(cs);
            con.Open();

            string sql = "SELECT table_id,avg(score) as average_performance FROM Student_Performance WHERE student_id="+user_id+" group by table_id ORDER BY table_id ASC";
            var cmd = new NpgsqlCommand(sql, con);

            NpgsqlDataReader rdr = cmd.ExecuteReader();
            Label[] score_labels = { score_1, score_2, score_3, score_4, score_5, score_6, score_7, score_8, score_9, score_10,score_11 };
            Label[] performance_labels = { skill_1, skill_2, skill_3, skill_4, skill_5, skill_6, skill_7, skill_8, skill_9, skill_10,skill_11 };
            for(int i = 0; i < score_labels.Length; i++)
            {
                score_labels[i].Text = "0";
                performance_labels[i].Text = "Low";
                performance_labels[i].ForeColor = Color.DarkRed;
            }
            while (rdr.Read())
            {
                if(rdr.GetInt32(0) - 1 == 10)
                {
                    upper_skill = 1100;
                    bottom_skill = 900;
                }
                else
                {
                    upper_skill = 600;
                    bottom_skill = 400;
                }
                score_labels[rdr.GetInt32(0) - 1].Text = rdr.GetInt32(1).ToString();
                int student_db_average_score = rdr.GetInt32(1);
                if(student_db_average_score<=upper_skill && student_db_average_score >= bottom_skill)
                {
                    performance_labels[rdr.GetInt32(0) - 1].Text = "Average";
                    performance_labels[rdr.GetInt32(0) - 1].ForeColor = Color.Gold;
                }
                else if (student_db_average_score > upper_skill)
                {
                    performance_labels[rdr.GetInt32(0) - 1].Text = "High";
                    performance_labels[rdr.GetInt32(0) - 1].ForeColor = Color.DarkGreen;
                }
                else if(student_db_average_score < bottom_skill)
                {
                    performance_labels[rdr.GetInt32(0) - 1].Text = "Low";
                    performance_labels[rdr.GetInt32(0) - 1].ForeColor = Color.DarkRed;
                }
                Console.WriteLine("{0} {1}", rdr.GetInt32(0), rdr.GetInt32(1));
            }
        }
        private int total_views()
        {
            int total = 0;
            var cs = "Host=localhost;Username=postgres;Password=3050;Database=LearningSoftwareDB";
            var con = new NpgsqlConnection(cs);
            con.Open();

            string sql = "SELECT table_id,table_views FROM Student_Traffic WHERE student_id=" + user_id + " AND table_id<13 ORDER BY table_id ASC";
            var cmd = new NpgsqlCommand(sql, con);

            NpgsqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
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

            string sql = "SELECT table_id,table_views FROM Student_Traffic WHERE student_id="+user_id+" ORDER BY table_id ASC";
            var cmd = new NpgsqlCommand(sql, con);

            NpgsqlDataReader rdr = cmd.ExecuteReader();
            Label[] view_labels = { view_1,view_2,view_3,view_4,view_5,view_6,view_7,view_8,view_9,view_10,view_11,view_12 };
            Label[] percent_labels = { percent_1,percent_2,percent_3,percent_4,percent_5,percent_6,percent_7,percent_8,percent_9,percent_10,percent_11,percent_12};
            for (int i = 0; i <percent_labels.Length; i++)
            {
                view_labels[i].Text = "0";
                percent_labels[i].Text = "0%";
            }
            Console.WriteLine(total_views_count.ToString());
            while (rdr.Read())
            {          
                view_labels[rdr.GetInt32(0) - 1].Text = rdr.GetInt32(1).ToString();
                percent_labels[rdr.GetInt32(0) - 1].Text = ((int)Math.Round((double)(100 * rdr.GetInt32(1)) / total_views_count)).ToString()+"%" ;
                if (rdr.GetInt32(0) == 12)
                {
                    break;
                }
            }
        }

        private bool views_exists(int stableid)
        {

            var cs = "Host=localhost;Username=postgres;Password=3050;Database=LearningSoftwareDB";
            var con = new NpgsqlConnection(cs);
            con.Open();

            string sql = "Select * from Student_Traffic WHERE student_id="+user_id+" and table_id="+stableid+ ";";
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

            string sql = "UPDATE Student_Traffic SET table_views = table_views + 1 WHERE student_id = '" + user_id+"' and table_id='"+selected_table_id+"'; ";
            var cmd = new NpgsqlCommand(sql, con);
            var version = cmd.ExecuteScalar();

            if(!views_exists(selected_table_id))
            {
                string sq2 = "INSERT INTO Student_Traffic(student_id,table_id,table_views) VALUES('" +user_id+ "','"+selected_table_id+"','1');";
                var cmd2 = new NpgsqlCommand(sq2, con);
                var version2 = cmd2.ExecuteScalar();

            }
            //Console.WriteLine(version.ToString());
        }


        private void button1_Click(object sender, EventArgs e)
        {
            (new SoundPlayer(Properties.Resources.button_click_1)).Play();
            label_no_choose.Visible = false;
            if (choice.Equals("1"))
            {
                choice = "null";
                button1.BackColor = Color.Tan;
                button1.FlatAppearance.BorderColor = Color.Tan;
                return;
            }
            Button[] buttons = { button1, button2, button3, button4, button5, button6, button7, button8, button9, button10, button11};
            for(int i = 0; i < buttons.Length; i++)
            {
                buttons[i].BackColor = Color.Tan;
                buttons[i].FlatAppearance.BorderColor = Color.Tan;
            }
            button1.BackColor = Color.SaddleBrown;
            button1.FlatAppearance.BorderColor = Color.SaddleBrown;
            choice = "1";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            (new SoundPlayer(Properties.Resources.button_click_1)).Play();
            label_no_choose.Visible = false;
            if (choice.Equals("2"))
            {
                choice = "null";
                button2.BackColor = Color.Tan;
                button2.FlatAppearance.BorderColor = Color.Tan;
                return;
            }
            Button[] buttons = { button1, button2, button3, button4, button5, button6, button7, button8, button9, button10, button11 };
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].BackColor = Color.Tan;
                buttons[i].FlatAppearance.BorderColor = Color.Tan;
            }
            button2.BackColor = Color.SaddleBrown;
            button2.FlatAppearance.BorderColor = Color.SaddleBrown;
            choice = "2";
        }
        private void button4_Click(object sender, EventArgs e)
        {
            (new SoundPlayer(Properties.Resources.button_click_1)).Play();
            label_no_choose.Visible = false;
            if (choice.Equals("4"))
            {
                choice = "null";
                button4.BackColor = Color.Tan;
                button4.FlatAppearance.BorderColor = Color.Tan;
                return;
            }
            Button[] buttons = { button1, button2, button3, button4, button5, button6, button7, button8, button9, button10, button11 };
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].BackColor = Color.Tan;
                buttons[i].FlatAppearance.BorderColor = Color.Tan;
            }
            button4.BackColor = Color.SaddleBrown;
            button4.FlatAppearance.BorderColor = Color.SaddleBrown;
            choice = "4";
        }
        private void button3_Click(object sender, EventArgs e)
        {
            (new SoundPlayer(Properties.Resources.button_click_1)).Play();
            label_no_choose.Visible = false;
            if (choice.Equals("3"))
            {
                choice = "null";
                button3.BackColor = Color.Tan;
                button3.FlatAppearance.BorderColor = Color.Tan;
                return;
            }
            Button[] buttons = { button1, button2, button3, button4, button5, button6, button7, button8, button9, button10, button11 };
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].BackColor = Color.Tan;
                buttons[i].FlatAppearance.BorderColor = Color.Tan;
            }
            button3.BackColor = Color.SaddleBrown;
            button3.FlatAppearance.BorderColor = Color.SaddleBrown;
            choice = "3";

        }
        private void button5_Click(object sender, EventArgs e)
        {
            (new SoundPlayer(Properties.Resources.button_click_1)).Play();
            label_no_choose.Visible = false;
            if (choice.Equals("5"))
            {
                choice = "null";
                button5.BackColor = Color.Tan;
                button5.FlatAppearance.BorderColor = Color.Tan;
                return;
            }
            Button[] buttons = { button1, button2, button3, button4, button5, button6, button7, button8, button9, button10, button11 };
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].BackColor = Color.Tan;
                buttons[i].FlatAppearance.BorderColor = Color.Tan;
            }
            button5.BackColor = Color.SaddleBrown;
            button5.FlatAppearance.BorderColor = Color.SaddleBrown;
            choice = "5";
        }
        private void button6_Click(object sender, EventArgs e)
        {
            (new SoundPlayer(Properties.Resources.button_click_1)).Play();
            label_no_choose.Visible = false;
            if (choice.Equals("6"))
            {
                choice = "null";
                button6.BackColor = Color.Tan;
                button6.FlatAppearance.BorderColor = Color.Tan;
                return;
            }
            Button[] buttons = { button1, button2, button3, button4, button5, button6, button7, button8, button9, button10, button11 };
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].BackColor = Color.Tan;
                buttons[i].FlatAppearance.BorderColor = Color.Tan;
            }
            button6.BackColor = Color.SaddleBrown;
            button6.FlatAppearance.BorderColor = Color.SaddleBrown;
            choice = "6";
        }
        private void button7_Click(object sender, EventArgs e)
        {
            (new SoundPlayer(Properties.Resources.button_click_1)).Play();
            label_no_choose.Visible = false;
            if (choice.Equals("7"))
            {
                choice = "null";
                button7.BackColor = Color.Tan;
                button7.FlatAppearance.BorderColor = Color.Tan;
                return;
            }
            Button[] buttons = { button1, button2, button3, button4, button5, button6, button7, button8, button9, button10, button11 };
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].BackColor = Color.Tan;
                buttons[i].FlatAppearance.BorderColor = Color.Tan;
            }
            button7.BackColor = Color.SaddleBrown;
            button7.FlatAppearance.BorderColor = Color.SaddleBrown;
            choice = "7";
        }
        private void button8_Click(object sender, EventArgs e)
        {
            (new SoundPlayer(Properties.Resources.button_click_1)).Play();
            label_no_choose.Visible = false;
            if (choice.Equals("8"))
            {
                choice = "null";
                button8.BackColor = Color.Tan;
                button8.FlatAppearance.BorderColor = Color.Tan;
                return;
            }
            Button[] buttons = { button1, button2, button3, button4, button5, button6, button7, button8, button9, button10, button11 };
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].BackColor = Color.Tan;
                buttons[i].FlatAppearance.BorderColor = Color.Tan;
            }
            button8.BackColor = Color.SaddleBrown;
            button8.FlatAppearance.BorderColor = Color.SaddleBrown;
            choice = "8";
        }
        private void button9_Click(object sender, EventArgs e)
        {
            (new SoundPlayer(Properties.Resources.button_click_1)).Play();
            label_no_choose.Visible = false;
            if (choice.Equals("9"))
            {
                choice = "null";
                button9.BackColor = Color.Tan;
                button9.FlatAppearance.BorderColor = Color.Tan;
                return;
            }
            Button[] buttons = { button1, button2, button3, button4, button5, button6, button7, button8, button9, button10, button11 };
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].BackColor = Color.Tan;
                buttons[i].FlatAppearance.BorderColor = Color.Tan;
            }
            button9.BackColor = Color.SaddleBrown;
            button9.FlatAppearance.BorderColor = Color.SaddleBrown;
            choice = "9";

        }
        private void button10_Click(object sender, EventArgs e)
        {
            (new SoundPlayer(Properties.Resources.button_click_1)).Play();
            label_no_choose.Visible = false;
            if (choice.Equals("10"))
            {
                choice = "null";
                button10.BackColor = Color.Tan;
                button10.FlatAppearance.BorderColor = Color.Tan;
                return;
            }
            Button[] buttons = { button1, button2, button3, button4, button5, button6, button7, button8, button9, button10, button11 };
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].BackColor = Color.Tan;
                buttons[i].FlatAppearance.BorderColor = Color.Tan;
            }
            button10.BackColor = Color.SaddleBrown;
            button10.FlatAppearance.BorderColor = Color.SaddleBrown;
            choice = "10";
        }
        private void button11_Click(object sender, EventArgs e)
        {
            (new SoundPlayer(Properties.Resources.button_click_1)).Play();
            label_no_choose.Visible = false;
            if (choice.Equals("practice"))
            {
                choice = "null";
                button11.BackColor = Color.Tan;
                button11.FlatAppearance.BorderColor = Color.Tan;
                return;
            }
            Button[] buttons = { button1, button2, button3, button4, button5, button6, button7, button8, button9, button10, button11 };
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].BackColor = Color.Tan;
                buttons[i].FlatAppearance.BorderColor = Color.Tan;
            }
            button11.BackColor = Color.SaddleBrown;
            button11.FlatAppearance.BorderColor = Color.SaddleBrown;
            choice = "practice";
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
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

        private void button13_Click(object sender, EventArgs e)
        {
            if(!(help_form == null))
            {
                help_form.Close();
            }
            TheoryForm tform = new TheoryForm(user_id,"11");
            tform.Show();
            this.Hide();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            (new SoundPlayer(Properties.Resources.radio_button_sound)).Play();
            if (choice.Equals("null"))
            {
                label_no_choose.Visible = true;
            }
            else
            {
                if (!(choice.Equals("practice")))
                {
                    add_view(Int32.Parse(choice));
                    questions = 10;
                }
                else
                {
                    add_view(11);
                    questions = 20;
                }
                if (!(help_form == null))
                {
                    help_form.Close();
                }
                Form1 test_form = new Form1(choice,user_id,questions);
                test_form.Show();
                this.Hide();
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

        private void help_button_Click(object sender, EventArgs e)
        {
            (new SoundPlayer(Properties.Resources.radio_button_sound)).Play();
            help_form = new HelpForm("-Press one out of the 10 section buttons or the practice section button and then press the Start button to play."+ System.Environment.NewLine+"-Press the sound icon to enable or disable the sound based on the current state." + System.Environment.NewLine +"-Press the syllabus button to go to the syllabus form and read the theory." + System.Environment.NewLine +"-Performance table shows you current level of skill (low,average or high) for every available section." + System.Environment.NewLine +"-Traffic table shows the times you press to play each of the available sections of the game." + System.Environment.NewLine +"-P on the tables refers to Practice and S for Syllabus." + System.Environment.NewLine + "-Press the 'X' button on the right top to close the APP.");
            help_form.Show();
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
    }
}
