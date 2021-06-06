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
    public partial class EndForm : Form
    {
        [DllImport("winmm.dll")]
        private static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);
        string user_id, table_id, right_questions, score, total_questions;
        private int times, avg_score;
        private int upper_skill = 600, bottom_skill = 400;
        private int upper_edge = 12, bottom_edge = 8;
        private HelpForm help_form;
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

        private void play_again_button_Click(object sender, EventArgs e)
        {
            if (!(help_form == null))
            {
                help_form.Close();
            }
            if (play_again_button.Text.Equals("Play Again"))
            {
                (new SoundPlayer(Properties.Resources.radio_button_sound)).Play();
                MainForm main_form = new MainForm();
                main_form.Show();
                this.Hide();
            }
            else
            {
                if (table_id.Equals("practice"))
                {
                    TheoryForm tform = new TheoryForm(user_id,"11");
                    tform.Show();
                    this.Hide();
                }
                else
                {
                    TheoryForm tform = new TheoryForm(user_id, table_id);
                    tform.Show();
                    this.Hide();
                }
            }
        }

        private void EndForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        public EndForm(string user_id,string table_id,string right_questions,string score,string total_questions)
        {
            InitializeComponent();
            load_sound_state();
            this.Icon = Properties.Resources.target_icon;
            if (Int32.Parse(total_questions) == 20)
            {
                upper_edge = 12;
                bottom_edge = 8;
            }else if(Int32.Parse(total_questions) == 10)
            {
                upper_edge = 6;
                bottom_edge = 4;
            }
            this.user_id = user_id;
            this.table_id = table_id;
            this.right_questions = right_questions;
            this.score = score;
            this.total_questions = total_questions;
            low_score_lb_1.Visible = false;
            low_score_lb_2.Visible = false;
            play_again_button.Text = "Play Again";

            if (Int32.Parse(right_questions) <= upper_edge && Int32.Parse(right_questions)>= bottom_edge)
            {
                Title_not_bad.Visible = true;
                Title_too_bad.Visible = false;
                Title_well_done.Visible = false;  
            }else if(Int32.Parse(right_questions) > upper_edge)
            {
                Title_not_bad.Visible = false;
                Title_too_bad.Visible = false;
                Title_well_done.Visible = true;
            }else if(Int32.Parse(right_questions) < bottom_edge)
            {
                Title_not_bad.Visible = false;
                Title_too_bad.Visible = true;
                Title_well_done.Visible = false;
            }
            score_label.Text = "Score: " + score;
            questions_label.Text = "You got " + right_questions + " out of "+total_questions+" questions right";
            SaveScoreToDB();
            get_times_finished_test();
            get_avg_score();
            if (!(table_id.Equals("practice")))
            {
                upper_skill = 600;
                bottom_skill = 400;
            }
            else
            {
                upper_skill = 1100;
                bottom_skill = 900;
            }
            if (Int32.Parse(score) < bottom_skill && avg_score < bottom_skill && times >= 3)
            {
                  low_score_lb_1.Text = "It seems your skill in this chapter after " + times + " tries is still low.";
                  low_score_lb_1.Visible = true;
                  low_score_lb_2.Visible = true;
                  play_again_button.Text = "Syllabus";
            }
            add_low_perf_student();

        }
        private void get_times_finished_test()
        {
            var cs = "Host=localhost;Username=postgres;Password=3050;Database=LearningSoftwareDB";
            var con = new NpgsqlConnection(cs);
            con.Open();

            string sql;
            if (!(table_id.Equals("practice")))
            {
                sql = "SELECT table_id,count(table_id) as cnt FROM Student_Performance WHERE student_id=" + user_id + " and table_id=" + table_id + " group by table_id;";
            }
            else
            {
                sql = "SELECT table_id,count(table_id) as cnt FROM Student_Performance WHERE student_id=" + user_id + " and table_id=" + "11" + " group by table_id;";
            }
                var cmd = new NpgsqlCommand(sql, con);

            NpgsqlDataReader rdr = cmd.ExecuteReader();
            
            while (rdr.Read())
            {
                times = rdr.GetInt32(1);
                //Console.WriteLine("{0} {1}", rdr.GetInt32(0), rdr.GetInt32(1));
            }
        }

        private void get_avg_score()
        {
            var cs = "Host=localhost;Username=postgres;Password=3050;Database=LearningSoftwareDB";
            var con = new NpgsqlConnection(cs);
            con.Open();

            string sql;
            if (!(table_id.Equals("practice")))
            {
                sql = "SELECT table_id,avg(score) as avg FROM Student_Performance WHERE student_id=" + user_id + " and table_id=" + table_id + " group by table_id;";
            }
            else
            {
                sql = "SELECT table_id,avg(score) as avg FROM Student_Performance WHERE student_id=" + user_id + " and table_id=" + "11" + " group by table_id;";
            }
            var cmd = new NpgsqlCommand(sql, con);

            NpgsqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                avg_score = rdr.GetInt32(1);
            }
        }

        private void help_button_Click(object sender, EventArgs e)
        {
            (new SoundPlayer(Properties.Resources.radio_button_sound)).Play();
            help_form = new HelpForm("-Press the sound icon to enable or disable the sound based on the current state." + System.Environment.NewLine + "-Press the 'X' button on the right top to close the APP." + "-Press the Play Again button to go back to the main form." + System.Environment.NewLine + "-If you keep getting low score after 2 times of playing the specific section then the Play Again button will become Syllabus and will redirect you to the theory of the specific section that you have problem learning.");
            help_form.Show();
        }

        private bool low_performance_exists()
        {

            var cs = "Host=localhost;Username=postgres;Password=3050;Database=LearningSoftwareDB";
            var con = new NpgsqlConnection(cs);
            con.Open();

            string sql;
            if (!(table_id.Equals("practice")))
            {
                sql = "SELECT * FROM Low_Performing_Students WHERE student_id=" + user_id + " and table_id=" + table_id + ";";
            }
            else
            {
                sql = "SELECT * FROM Low_Performing_Students WHERE student_id=" + user_id + " and table_id=" + "11" + ";";
            }
            var cmd = new NpgsqlCommand(sql, con);
            NpgsqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                return true;
            }
            return false;

        }

        private void add_low_perf_student()
        {
            var cs = "Host=localhost;Username=postgres;Password=3050;Database=LearningSoftwareDB";
            var con = new NpgsqlConnection(cs);
            con.Open();

            if (!low_performance_exists())
            {
                if (avg_score < bottom_skill)
                {
                    string sql2;
                    if (!(table_id.Equals("practice")))
                    {
                        sql2 = "INSERT INTO Low_Performing_Students(student_id,table_id) VALUES('" + user_id + "','" + table_id + "');";
                    }
                    else
                    {
                        sql2 = "INSERT INTO Low_Performing_Students(student_id,table_id) VALUES('" + user_id + "','" + "11" + "');";

                    }
                    var cmd2 = new NpgsqlCommand(sql2, con);
                    var version2 = cmd2.ExecuteScalar();
                }
            }
            else
            {
                if (avg_score > upper_skill)
                {
                    string sql;
                    if (!(table_id.Equals("practice")))
                    {
                        sql = "DELETE FROM Low_Performing_Students WHERE student_id=" + user_id + " and table_id=" + table_id + " ;";
                    }
                    else
                    {
                        sql = "DELETE FROM Low_Performing_Students WHERE student_id=" + user_id + " and table_id=" + "11" + " ;";
                    }
                    var cmd = new NpgsqlCommand(sql, con);
                    var version = cmd.ExecuteScalar();
                }
            }
        }
        private void EndForm_Load(object sender, EventArgs e)
        {

        }

        private void SaveScoreToDB()
        {
            var cs = "Host=localhost;Username=postgres;Password=3050;Database=LearningSoftwareDB";
            var con = new NpgsqlConnection(cs);
            con.Open();
            string sql;
            if (!(table_id.Equals("practice")))
            {
                sql = "INSERT INTO Student_Performance(student_id,table_id,score) VALUES('" + user_id + "','" + table_id + "','" + score + "');";
            }
            else
            {
                sql = "INSERT INTO Student_Performance(student_id,table_id,score) VALUES('" + user_id + "','" + "11" + "','" + score + "');";
            }
            var cmd = new NpgsqlCommand(sql, con);
            var version = cmd.ExecuteScalar();
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
    }
}
