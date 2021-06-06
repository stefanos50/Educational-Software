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

    public partial class Form1 : Form
    {
        [DllImport("winmm.dll")]
        private static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);
        private bool clicked = false;
        private string answear;
        private string question;
        private int score = 0;
        private int total_questions = 5;
        private int current_question = 1;
        private string table_id, user_id;
        private int correct_answears = 0;
        private List<int> probabilities = new List<int>();
        Random r = new Random();
        private int upper_skill = 600, bottom_skill = 400;
        private HelpForm help_form;
        public Form1(string table_id,string user_id,int total_questions)
        {
            InitializeComponent();
            load_sound_state();
            this.Icon = Properties.Resources.target_icon;
            this.table_id = table_id;
            this.user_id = user_id;
            if (table_id.Equals("practice")){
                get_prob_array();
            }
            init();
            score = 0;
            correct_answears = 0;
            score_label.Text = "Score: " + score.ToString();
            this.total_questions = total_questions;
            current_question = 1;
            question_label.Text = "Question: " + current_question.ToString() + "/" + total_questions.ToString();
        }

        private void get_prob_array()
        {
            var cs = "Host=localhost;Username=postgres;Password=3050;Database=LearningSoftwareDB";
            var con = new NpgsqlConnection(cs);
            con.Open();

            string sql = "SELECT table_id,avg(score) as average_performance FROM Student_Performance WHERE student_id=" + user_id + " group by table_id ORDER BY table_id ASC";
            var cmd = new NpgsqlCommand(sql, con);

            NpgsqlDataReader rdr = cmd.ExecuteReader();
            int[] probs = { 15,15,15,15,15,15,15,15,15,15 };
            while (rdr.Read())
            {
                if(rdr.GetInt32(0) == 11)
                {
                    continue;
                }
                int student_db_average_score = rdr.GetInt32(1);
                if (student_db_average_score <= upper_skill && student_db_average_score >= bottom_skill)
                {
                    probs[rdr.GetInt32(0) - 1] = 10;
                }
                else if (student_db_average_score > upper_skill)
                {
                    probs[rdr.GetInt32(0) - 1] = 5;
                }
                else if (student_db_average_score < bottom_skill)
                {
                    probs[rdr.GetInt32(0) - 1] = 15;
                }
               
            }
            for(int i = 1; i <= probs.Length; i++)
            {
                for(int j=0; j<probs[i-1]; j++)
                {
                    probabilities.Add(i);
                }
            }

        }

        private void generate_question()
        {

                Label[] guesses = { guess_1, guess_2, guess_3, guess_4 };
                for (int i = 0; i < guesses.Length; i++)
                {
                    guesses[i].Text = "-1";
                }

                int number_one = -1;
                if (!table_id.Equals("practice"))
                {
                    number_one = Int32.Parse(table_id);
                }
                else
                {
                    int randomprobIndex = r.Next(0, probabilities.Count);
                    number_one = probabilities[randomprobIndex];
                }
                int number_two = r.Next(1, 13); // creates a number between 1 and 12

                question = number_one + "x" + number_two;
                answear = (number_one * number_two).ToString();

                mult_prep_1.Text = question+"=";
                mult_prep_2.Text = question+"=";

                int randomIndex = r.Next(0, guesses.Length);
                guesses[randomIndex].Text = answear;

                for(int i=0; i<guesses.Length; i++)
                {
                    int random_wrong_guess = -1;
                    do
                    {                 
                        if (Int32.Parse(answear) - 10 <= 0)
                        {
                            random_wrong_guess = r.Next(1, Int32.Parse(answear) + 11);
                        }
                        else
                        {
                            random_wrong_guess = r.Next(Int32.Parse(answear) - 10, Int32.Parse(answear) + 11);
                        }
                    } while(random_wrong_guess == Int32.Parse(answear) || random_wrong_guess == Int32.Parse(guesses[0].Text) || random_wrong_guess == Int32.Parse(guesses[1].Text) || random_wrong_guess == Int32.Parse(guesses[2].Text) || random_wrong_guess == Int32.Parse(guesses[3].Text));

                    if (!guesses[i].Text.Equals(answear))
                    {
                        guesses[i].Text = random_wrong_guess.ToString();
                    }
                }          
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void change_cursor()
        {
            Bitmap b = new Bitmap(Properties.Resources.aim);
            b.MakeTransparent(b.GetPixel(0, 0));
            Graphics g = Graphics.FromImage(b);
            IntPtr ptr = b.GetHicon();
            Cursor = new System.Windows.Forms.Cursor(ptr);
        }
        public void enable_disable_aims(bool aim_y , bool aim_r , bool aim_g, bool arrow,bool skip_arrows)
        {
            PictureBox[] red_stoixoi = { stoxos_red_1, stoxos_red_2, stoxos_red_3, stoxos_red_4 };
            PictureBox[] green_stoixoi = { stoxos_green_1, stoxos_green_2, stoxos_green_3, stoxos_green_4 };
            PictureBox[] yellow_stoixoi = { stoxos_yellow_1, stoxos_yellow_2, stoxos_yellow_3, stoxos_yellow_4 };
            PictureBox[] arrows = { arrow_1,arrow_2,arrow_3,arrow_4 };
            for (int i = 0; i < red_stoixoi.Length; i++)
            {
                red_stoixoi[i].Visible = aim_r;
                green_stoixoi[i].Visible = aim_g;
                yellow_stoixoi[i].Visible = aim_y;
                if (!skip_arrows)
                {
                    arrows[i].Visible = arrow;
                }
            }
        }
        private void play_bow(int aim_index)
        {
            bow_picture_box.Image = Properties.Resources.bow_gif;
            WaitSomeTime(aim_index);
        }
        public async void WaitSomeTime(int aim_index)
        {
            await Task.Delay(800);
            (new SoundPlayer(Properties.Resources.bow_sound)).Play();
            await Task.Delay(300);
            bow_picture_box.Image = null;
            PictureBox[] arrows = { arrow_1, arrow_2, arrow_3, arrow_4 };
            arrows[aim_index].Visible = true;
            (new SoundPlayer(Properties.Resources.arrow_hit_sound)).Play();
            validate_result(aim_index);
        }
        private void init()
        {
            clicked = false;
            panel1.Visible = false;
            change_cursor();
            enable_disable_aims(true, false, false,false,false);
            hide_fireworks();
            generate_question();

        }
        private void validate_result(int index)
        {
            if(index == 0)
            {
                if(guess_1.Text == answear)
                {
                    (new SoundPlayer(Properties.Resources.correct_choice_sound)).Play();
                    correct_answears++;
                    enable_disable_aims(false, false, true, false, true);
                    score += 100;
                    score_label.Text = "Score: " + score.ToString();
                    play_fireworks();
                }
                else
                {
                    (new SoundPlayer(Properties.Resources.wrong_choice_sound)).Play();
                    enable_disable_aims(false, true, false, false, true);
                    show_mistake_panel();
                }
            }else if (index == 1)
            {
                if (guess_2.Text == answear)
                {
                    (new SoundPlayer(Properties.Resources.correct_choice_sound)).Play();
                    correct_answears++;
                    enable_disable_aims(false, false, true, false, true);
                    score += 100;
                    score_label.Text = "Score: " + score.ToString();
                    play_fireworks();
                }
                else
                {
                    (new SoundPlayer(Properties.Resources.wrong_choice_sound)).Play();
                    enable_disable_aims(false, true, false, false, true);
                    show_mistake_panel();
                }
            }
            else if (index == 2)
            {
                if (guess_3.Text == answear)
                {
                    (new SoundPlayer(Properties.Resources.correct_choice_sound)).Play();
                    correct_answears++;
                    enable_disable_aims(false, false, true, false, true);
                    score += 100;
                    score_label.Text = "Score: " + score.ToString();
                    play_fireworks();
                }
                else
                {
                    (new SoundPlayer(Properties.Resources.wrong_choice_sound)).Play();
                    enable_disable_aims(false, true, false, false, true);
                    show_mistake_panel();
                }
            }
            else if (index == 3)
            {
                if (guess_4.Text == answear)
                {
                    (new SoundPlayer(Properties.Resources.correct_choice_sound)).Play();
                    correct_answears++;
                    enable_disable_aims(false, false, true, false, true);
                    score += 100;
                    score_label.Text = "Score: " + score.ToString();
                    play_fireworks();
                }
                else
                {
                    (new SoundPlayer(Properties.Resources.wrong_choice_sound)).Play();
                    enable_disable_aims(false, true, false, false, true);
                    show_mistake_panel();
                }
            }
        }
        private void play_fireworks()
        {
            PictureBox[] fireworks = { firework_1, firework_2, firework_3,firework_4 };
            for (int i = 0; i < fireworks.Length; i++)
            {
                fireworks[i].Image = Properties.Resources.firework_orange;
            }
            WaitSomeTimeFireworks();
        }
        private void increment_question()
        {
                current_question++;
                question_label.Text = "Question: " + current_question.ToString() + "/" + total_questions.ToString();
        }
        private void show_mistake_panel()
        {
            panel1.Visible = true;
            panel_message_label.Text ="Too Bad "+ question + "=" + answear;
            WaitSomeTimePanel();
        }
        public async void WaitSomeTimePanel()
        {
            await Task.Delay(4000);
            panel1.Visible = false;
            next_question();
        }
        public async void WaitSomeTimeFireworks()
        {
            await Task.Delay(2000);
            (new SoundPlayer(Properties.Resources.firework_sound)).Play();
            await Task.Delay(2000);
            bow_picture_box.Image = null;
            hide_fireworks();
            next_question();
        }
        private void hide_fireworks()
        {
            PictureBox[] fireworks = { firework_1, firework_2, firework_3,firework_4 };
            for (int i = 0; i < fireworks.Length; i++)
            {
                fireworks[i].Image = null;
            }
        }

        private void next_question()
        {
            increment_question();
            if (current_question < total_questions + 1)
            {
                init();
            }
            else
            {
                if (!(help_form == null))
                {
                    help_form.Close();
                }
                EndForm end_form = new EndForm(user_id,table_id,correct_answears.ToString(),score.ToString(),total_questions.ToString());
                end_form.Show();
                this.Hide();
            }
        }

        private void stoxos_yellow_1_Click(object sender, EventArgs e)
        {
            if(clicked == false)
            {
                clicked = true;
                play_bow(0);
            }
        }

        private void stoxos_yellow_2_Click(object sender, EventArgs e)
        {
            if (clicked == false)
            {
                clicked = true;
                play_bow(1);
            }
        }

        private void stoxos_yellow_3_Click(object sender, EventArgs e)
        {
            if (clicked == false)
            {
                clicked = true;
                play_bow(2);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
                Application.Exit();
        }

        private void stoxos_yellow_4_Click(object sender, EventArgs e)
        {
            if (clicked == false)
            {
                clicked = true;
                play_bow(3);
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
        private void pictureBox1_Click(object sender, EventArgs e)
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

        private void stoxos_green_1_Click(object sender, EventArgs e)
        {

        }

        private void guess_1_Click(object sender, EventArgs e)
        {
            if (clicked == false)
            {
                clicked = true;
                play_bow(0);
            }
        }

        private void guess_2_Click(object sender, EventArgs e)
        {
            if (clicked == false)
            {
                clicked = true;
                play_bow(1);
            }
        }

        private void guess_3_Click(object sender, EventArgs e)
        {
            if (clicked == false)
            {
                clicked = true;
                play_bow(2);
            }
        }

        private void guess_4_Click(object sender, EventArgs e)
        {
            if (clicked == false)
            {
                clicked = true;
                play_bow(3);
            }
        }

        private void help_button_Click(object sender, EventArgs e)
        {
            (new SoundPlayer(Properties.Resources.radio_button_sound)).Play();
            help_form = new HelpForm("-Look at the question that there is on the signs." + System.Environment.NewLine + "-Click on the target that you believe that contains the correct answear for the question" + System.Environment.NewLine +"-Press the 'X' button on the right top to close the APP." + System.Environment.NewLine +"-There is a small delay between the time you answear and the time that the next question will appear." + System.Environment.NewLine + "-Press the sound icon to enable or disable the sound based on the current state." + System.Environment.NewLine + "-When practising it is more likely to get questions from the sections that your skill is lower.");
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
