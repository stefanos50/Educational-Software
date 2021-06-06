
namespace LearningSoftware
{
    partial class EndForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.main_panel = new System.Windows.Forms.Panel();
            this.low_score_lb_2 = new System.Windows.Forms.Label();
            this.low_score_lb_1 = new System.Windows.Forms.Label();
            this.score_label = new System.Windows.Forms.Label();
            this.Title_too_bad = new System.Windows.Forms.Label();
            this.Title_not_bad = new System.Windows.Forms.Label();
            this.play_again_button = new System.Windows.Forms.Button();
            this.questions_label = new System.Windows.Forms.Label();
            this.Title_well_done = new System.Windows.Forms.Label();
            this.sound_picture = new System.Windows.Forms.PictureBox();
            this.help_button = new System.Windows.Forms.Button();
            this.main_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sound_picture)).BeginInit();
            this.SuspendLayout();
            // 
            // main_panel
            // 
            this.main_panel.BackColor = System.Drawing.Color.Transparent;
            this.main_panel.BackgroundImage = global::LearningSoftware.Properties.Resources.wooden_sign_1000;
            this.main_panel.Controls.Add(this.low_score_lb_2);
            this.main_panel.Controls.Add(this.low_score_lb_1);
            this.main_panel.Controls.Add(this.score_label);
            this.main_panel.Controls.Add(this.Title_too_bad);
            this.main_panel.Controls.Add(this.Title_not_bad);
            this.main_panel.Controls.Add(this.play_again_button);
            this.main_panel.Controls.Add(this.questions_label);
            this.main_panel.Controls.Add(this.Title_well_done);
            this.main_panel.Location = new System.Drawing.Point(437, 169);
            this.main_panel.Name = "main_panel";
            this.main_panel.Size = new System.Drawing.Size(1028, 649);
            this.main_panel.TabIndex = 31;
            // 
            // low_score_lb_2
            // 
            this.low_score_lb_2.AutoSize = true;
            this.low_score_lb_2.BackColor = System.Drawing.Color.Transparent;
            this.low_score_lb_2.Font = new System.Drawing.Font("Segoe Script", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.low_score_lb_2.ForeColor = System.Drawing.Color.DarkRed;
            this.low_score_lb_2.Location = new System.Drawing.Point(308, 470);
            this.low_score_lb_2.Name = "low_score_lb_2";
            this.low_score_lb_2.Size = new System.Drawing.Size(433, 31);
            this.low_score_lb_2.TabIndex = 44;
            this.low_score_lb_2.Text = "It is recommended to revisit the syllabus.";
            // 
            // low_score_lb_1
            // 
            this.low_score_lb_1.AutoSize = true;
            this.low_score_lb_1.BackColor = System.Drawing.Color.Transparent;
            this.low_score_lb_1.Font = new System.Drawing.Font("Segoe Script", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.low_score_lb_1.ForeColor = System.Drawing.Color.DarkRed;
            this.low_score_lb_1.Location = new System.Drawing.Point(224, 439);
            this.low_score_lb_1.Name = "low_score_lb_1";
            this.low_score_lb_1.Size = new System.Drawing.Size(614, 31);
            this.low_score_lb_1.TabIndex = 43;
            this.low_score_lb_1.Text = "It seems your skill in this chapter after X tries is still low.";
            // 
            // score_label
            // 
            this.score_label.AutoSize = true;
            this.score_label.BackColor = System.Drawing.Color.Transparent;
            this.score_label.Font = new System.Drawing.Font("Segoe Script", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.score_label.Location = new System.Drawing.Point(458, 274);
            this.score_label.Name = "score_label";
            this.score_label.Size = new System.Drawing.Size(173, 44);
            this.score_label.TabIndex = 42;
            this.score_label.Text = "Score: 100";
            // 
            // Title_too_bad
            // 
            this.Title_too_bad.AutoSize = true;
            this.Title_too_bad.BackColor = System.Drawing.Color.Transparent;
            this.Title_too_bad.Font = new System.Drawing.Font("Segoe Script", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.Title_too_bad.Location = new System.Drawing.Point(364, 127);
            this.Title_too_bad.Name = "Title_too_bad";
            this.Title_too_bad.Size = new System.Drawing.Size(377, 106);
            this.Title_too_bad.TabIndex = 41;
            this.Title_too_bad.Text = "Too Bad...";
            // 
            // Title_not_bad
            // 
            this.Title_not_bad.AutoSize = true;
            this.Title_not_bad.BackColor = System.Drawing.Color.Transparent;
            this.Title_not_bad.Font = new System.Drawing.Font("Segoe Script", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.Title_not_bad.Location = new System.Drawing.Point(374, 127);
            this.Title_not_bad.Name = "Title_not_bad";
            this.Title_not_bad.Size = new System.Drawing.Size(343, 106);
            this.Title_not_bad.TabIndex = 40;
            this.Title_not_bad.Text = "Not Bad!";
            // 
            // play_again_button
            // 
            this.play_again_button.BackColor = System.Drawing.Color.Tan;
            this.play_again_button.FlatAppearance.BorderColor = System.Drawing.Color.Tan;
            this.play_again_button.FlatAppearance.BorderSize = 2;
            this.play_again_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.play_again_button.Font = new System.Drawing.Font("Segoe Script", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.play_again_button.Location = new System.Drawing.Point(686, 512);
            this.play_again_button.Name = "play_again_button";
            this.play_again_button.Size = new System.Drawing.Size(195, 60);
            this.play_again_button.TabIndex = 39;
            this.play_again_button.Text = "Play Again";
            this.play_again_button.UseVisualStyleBackColor = false;
            this.play_again_button.Click += new System.EventHandler(this.play_again_button_Click);
            // 
            // questions_label
            // 
            this.questions_label.AutoSize = true;
            this.questions_label.BackColor = System.Drawing.Color.Transparent;
            this.questions_label.Font = new System.Drawing.Font("Segoe Script", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.questions_label.Location = new System.Drawing.Point(294, 351);
            this.questions_label.Name = "questions_label";
            this.questions_label.Size = new System.Drawing.Size(530, 44);
            this.questions_label.TabIndex = 31;
            this.questions_label.Text = "You got 2 out of 20 questions right";
            // 
            // Title_well_done
            // 
            this.Title_well_done.AutoSize = true;
            this.Title_well_done.BackColor = System.Drawing.Color.Transparent;
            this.Title_well_done.Font = new System.Drawing.Font("Segoe Script", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.Title_well_done.Location = new System.Drawing.Point(345, 127);
            this.Title_well_done.Name = "Title_well_done";
            this.Title_well_done.Size = new System.Drawing.Size(412, 106);
            this.Title_well_done.TabIndex = 30;
            this.Title_well_done.Text = "Well Done!";
            // 
            // sound_picture
            // 
            this.sound_picture.AccessibleName = "stoxos_green_1";
            this.sound_picture.BackColor = System.Drawing.Color.Transparent;
            this.sound_picture.Image = global::LearningSoftware.Properties.Resources.sound_icon;
            this.sound_picture.Location = new System.Drawing.Point(1816, 12);
            this.sound_picture.Name = "sound_picture";
            this.sound_picture.Size = new System.Drawing.Size(75, 51);
            this.sound_picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.sound_picture.TabIndex = 32;
            this.sound_picture.TabStop = false;
            this.sound_picture.Tag = "unmute";
            this.sound_picture.Click += new System.EventHandler(this.sound_picture_Click);
            // 
            // help_button
            // 
            this.help_button.BackColor = System.Drawing.Color.LimeGreen;
            this.help_button.FlatAppearance.BorderColor = System.Drawing.Color.DarkOliveGreen;
            this.help_button.FlatAppearance.BorderSize = 2;
            this.help_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.help_button.Font = new System.Drawing.Font("Segoe Script", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.help_button.Location = new System.Drawing.Point(1652, 12);
            this.help_button.Name = "help_button";
            this.help_button.Size = new System.Drawing.Size(146, 40);
            this.help_button.TabIndex = 48;
            this.help_button.Text = "Help";
            this.help_button.UseVisualStyleBackColor = false;
            this.help_button.Click += new System.EventHandler(this.help_button_Click);
            // 
            // EndForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::LearningSoftware.Properties.Resources.Forest1;
            this.ClientSize = new System.Drawing.Size(1903, 987);
            this.Controls.Add(this.help_button);
            this.Controls.Add(this.sound_picture);
            this.Controls.Add(this.main_panel);
            this.Name = "EndForm";
            this.Text = "Times Tables Shooting";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EndForm_FormClosing);
            this.Load += new System.EventHandler(this.EndForm_Load);
            this.main_panel.ResumeLayout(false);
            this.main_panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sound_picture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel main_panel;
        private System.Windows.Forms.Label score_label;
        private System.Windows.Forms.Label Title_too_bad;
        private System.Windows.Forms.Label Title_not_bad;
        private System.Windows.Forms.Button play_again_button;
        private System.Windows.Forms.Label questions_label;
        private System.Windows.Forms.Label Title_well_done;
        private System.Windows.Forms.Label low_score_lb_2;
        private System.Windows.Forms.Label low_score_lb_1;
        private System.Windows.Forms.PictureBox sound_picture;
        private System.Windows.Forms.Button help_button;
    }
}