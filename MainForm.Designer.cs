namespace UniSchedule
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label2 = new System.Windows.Forms.Label();
            this.uploadDir = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.generateButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.generatingProgressBar = new System.Windows.Forms.ProgressBar();
            this.generating = new System.Windows.Forms.Label();
            this.successfullyGen = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.roomsRadioButton = new System.Windows.Forms.RadioButton();
            this.roomLabel = new System.Windows.Forms.Label();
            this.comboBoxRooms = new System.Windows.Forms.ComboBox();
            this.saveRoomSchedulesButton = new System.Windows.Forms.Button();
            this.saveProfSchedulesButton = new System.Windows.Forms.Button();
            this.saveStudSchedulesButton = new System.Windows.Forms.Button();
            this.Showbutton = new System.Windows.Forms.Button();
            this.comboBoxProfessor = new System.Windows.Forms.ComboBox();
            this.comboBoxDepart = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxGroup = new System.Windows.Forms.ComboBox();
            this.comboBoxCourse = new System.Windows.Forms.ComboBox();
            this.comboBoxSpec = new System.Windows.Forms.ComboBox();
            this.radioButtonProfessors = new System.Windows.Forms.RadioButton();
            this.radioButtonStudents = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Turquoise;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1499, 113);
            this.label2.TabIndex = 0;
            this.label2.Text = "Генератор на учебни разписи";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // uploadDir
            // 
            this.uploadDir.AllowDrop = true;
            this.uploadDir.Location = new System.Drawing.Point(23, 166);
            this.uploadDir.Name = "uploadDir";
            this.uploadDir.Size = new System.Drawing.Size(189, 29);
            this.uploadDir.TabIndex = 1;
            this.uploadDir.Text = "Прикачи директория";
            this.uploadDir.UseVisualStyleBackColor = true;
            this.uploadDir.Click += new System.EventHandler(this.uploadDir_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(23, 265);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(141, 29);
            this.button2.TabIndex = 3;
            this.button2.Text = "Изчисти";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.cleanButton_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.button3.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button3.Location = new System.Drawing.Point(791, 512);
            this.button3.Name = "button3";
            this.button3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.button3.Size = new System.Drawing.Size(106, 33);
            this.button3.TabIndex = 4;
            this.button3.Text = "Относно";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.aboutButton_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(791, 551);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(106, 29);
            this.button4.TabIndex = 5;
            this.button4.Text = "Изход";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // generateButton
            // 
            this.generateButton.Location = new System.Drawing.Point(464, 265);
            this.generateButton.Name = "generateButton";
            this.generateButton.Size = new System.Drawing.Size(120, 29);
            this.generateButton.TabIndex = 6;
            this.generateButton.Text = "Генерирай";
            this.generateButton.UseVisualStyleBackColor = true;
            this.generateButton.Click += new System.EventHandler(this.generateButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(311, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(328, 32);
            this.label1.TabIndex = 7;
            this.label1.Text = "Изберете учебен разпис";
            // 
            // generatingProgressBar
            // 
            this.generatingProgressBar.Location = new System.Drawing.Point(127, 426);
            this.generatingProgressBar.Name = "generatingProgressBar";
            this.generatingProgressBar.Size = new System.Drawing.Size(365, 20);
            this.generatingProgressBar.TabIndex = 8;
            this.generatingProgressBar.Visible = false;
            // 
            // generating
            // 
            this.generating.AutoSize = true;
            this.generating.Location = new System.Drawing.Point(185, 404);
            this.generating.Name = "generating";
            this.generating.Size = new System.Drawing.Size(267, 19);
            this.generating.TabIndex = 9;
            this.generating.Text = "Генериране на учебни разписи ...";
            this.generating.Visible = false;
            // 
            // successfullyGen
            // 
            this.successfullyGen.AutoSize = true;
            this.successfullyGen.Location = new System.Drawing.Point(227, 404);
            this.successfullyGen.Name = "successfullyGen";
            this.successfullyGen.Size = new System.Drawing.Size(0, 19);
            this.successfullyGen.TabIndex = 10;
            this.successfullyGen.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(23, 214);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(561, 27);
            this.textBox1.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 166);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 19);
            this.label3.TabIndex = 13;
            this.label3.Text = "Специалност";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 208);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 19);
            this.label4.TabIndex = 14;
            this.label4.Text = "Курс";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 250);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 19);
            this.label5.TabIndex = 15;
            this.label5.Text = "Група";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.roomsRadioButton);
            this.groupBox1.Controls.Add(this.roomLabel);
            this.groupBox1.Controls.Add(this.comboBoxRooms);
            this.groupBox1.Controls.Add(this.saveRoomSchedulesButton);
            this.groupBox1.Controls.Add(this.saveProfSchedulesButton);
            this.groupBox1.Controls.Add(this.saveStudSchedulesButton);
            this.groupBox1.Controls.Add(this.Showbutton);
            this.groupBox1.Controls.Add(this.comboBoxProfessor);
            this.groupBox1.Controls.Add(this.comboBoxDepart);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.comboBoxGroup);
            this.groupBox1.Controls.Add(this.comboBoxCourse);
            this.groupBox1.Controls.Add(this.comboBoxSpec);
            this.groupBox1.Controls.Add(this.radioButtonProfessors);
            this.groupBox1.Controls.Add(this.radioButtonStudents);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Location = new System.Drawing.Point(590, 116);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(909, 590);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            // 
            // roomsRadioButton
            // 
            this.roomsRadioButton.AutoSize = true;
            this.roomsRadioButton.Location = new System.Drawing.Point(721, 102);
            this.roomsRadioButton.Name = "roomsRadioButton";
            this.roomsRadioButton.Size = new System.Drawing.Size(68, 23);
            this.roomsRadioButton.TabIndex = 29;
            this.roomsRadioButton.TabStop = true;
            this.roomsRadioButton.Text = "Стаи";
            this.roomsRadioButton.UseVisualStyleBackColor = true;
            this.roomsRadioButton.CheckedChanged += new System.EventHandler(this.roomsRadioButton_CheckedChanged);
            // 
            // roomLabel
            // 
            this.roomLabel.AutoSize = true;
            this.roomLabel.Location = new System.Drawing.Point(649, 166);
            this.roomLabel.Name = "roomLabel";
            this.roomLabel.Size = new System.Drawing.Size(47, 19);
            this.roomLabel.TabIndex = 28;
            this.roomLabel.Text = "Стая";
            // 
            // comboBoxRooms
            // 
            this.comboBoxRooms.Enabled = false;
            this.comboBoxRooms.FormattingEnabled = true;
            this.comboBoxRooms.Location = new System.Drawing.Point(721, 163);
            this.comboBoxRooms.Name = "comboBoxRooms";
            this.comboBoxRooms.Size = new System.Drawing.Size(151, 27);
            this.comboBoxRooms.TabIndex = 27;
            this.comboBoxRooms.MouseClick += new System.Windows.Forms.MouseEventHandler(this.comboBoxRooms_MouseClick);
            // 
            // saveRoomSchedulesButton
            // 
            this.saveRoomSchedulesButton.Location = new System.Drawing.Point(18, 524);
            this.saveRoomSchedulesButton.Name = "saveRoomSchedulesButton";
            this.saveRoomSchedulesButton.Size = new System.Drawing.Size(191, 56);
            this.saveRoomSchedulesButton.TabIndex = 26;
            this.saveRoomSchedulesButton.Text = "Запиши разписите на всички стаи";
            this.saveRoomSchedulesButton.UseVisualStyleBackColor = true;
            this.saveRoomSchedulesButton.Click += new System.EventHandler(this.saveRoomSchedulesButton_Click);
            // 
            // saveProfSchedulesButton
            // 
            this.saveProfSchedulesButton.Location = new System.Drawing.Point(18, 434);
            this.saveProfSchedulesButton.Name = "saveProfSchedulesButton";
            this.saveProfSchedulesButton.Size = new System.Drawing.Size(191, 56);
            this.saveProfSchedulesButton.TabIndex = 26;
            this.saveProfSchedulesButton.Text = "Запиши разписите на всички преподаватели";
            this.saveProfSchedulesButton.UseVisualStyleBackColor = true;
            this.saveProfSchedulesButton.Click += new System.EventHandler(this.saveProfSchedulesButton_Click);
            // 
            // saveStudSchedulesButton
            // 
            this.saveStudSchedulesButton.Location = new System.Drawing.Point(18, 343);
            this.saveStudSchedulesButton.Name = "saveStudSchedulesButton";
            this.saveStudSchedulesButton.Size = new System.Drawing.Size(191, 56);
            this.saveStudSchedulesButton.TabIndex = 25;
            this.saveStudSchedulesButton.Text = "Запиши разписите на всички студенти";
            this.saveStudSchedulesButton.UseVisualStyleBackColor = true;
            this.saveStudSchedulesButton.Click += new System.EventHandler(this.saveStudSchedulesButton_Click);
            // 
            // Showbutton
            // 
            this.Showbutton.Enabled = false;
            this.Showbutton.Location = new System.Drawing.Point(434, 310);
            this.Showbutton.Name = "Showbutton";
            this.Showbutton.Size = new System.Drawing.Size(114, 29);
            this.Showbutton.TabIndex = 24;
            this.Showbutton.Text = "Покажи";
            this.Showbutton.UseVisualStyleBackColor = true;
            this.Showbutton.Click += new System.EventHandler(this.Show_Click);
            // 
            // comboBoxProfessor
            // 
            this.comboBoxProfessor.Enabled = false;
            this.comboBoxProfessor.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBoxProfessor.FormattingEnabled = true;
            this.comboBoxProfessor.Location = new System.Drawing.Point(367, 205);
            this.comboBoxProfessor.Name = "comboBoxProfessor";
            this.comboBoxProfessor.Size = new System.Drawing.Size(222, 24);
            this.comboBoxProfessor.TabIndex = 23;
            // 
            // comboBoxDepart
            // 
            this.comboBoxDepart.Enabled = false;
            this.comboBoxDepart.FormattingEnabled = true;
            this.comboBoxDepart.Location = new System.Drawing.Point(392, 163);
            this.comboBoxDepart.Name = "comboBoxDepart";
            this.comboBoxDepart.Size = new System.Drawing.Size(103, 27);
            this.comboBoxDepart.TabIndex = 22;
            this.comboBoxDepart.TextChanged += new System.EventHandler(this.comboBoxDepart_TextChanged);
            this.comboBoxDepart.MouseClick += new System.Windows.Forms.MouseEventHandler(this.comboBoxDepart_MouseClick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(242, 208);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(119, 19);
            this.label7.TabIndex = 21;
            this.label7.Text = "Преподавател";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(259, 166);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 19);
            this.label6.TabIndex = 20;
            this.label6.Text = "Катедра";
            // 
            // comboBoxGroup
            // 
            this.comboBoxGroup.FormattingEnabled = true;
            this.comboBoxGroup.Location = new System.Drawing.Point(125, 247);
            this.comboBoxGroup.Name = "comboBoxGroup";
            this.comboBoxGroup.Size = new System.Drawing.Size(102, 27);
            this.comboBoxGroup.TabIndex = 19;
            // 
            // comboBoxCourse
            // 
            this.comboBoxCourse.FormattingEnabled = true;
            this.comboBoxCourse.Location = new System.Drawing.Point(124, 205);
            this.comboBoxCourse.Name = "comboBoxCourse";
            this.comboBoxCourse.Size = new System.Drawing.Size(103, 27);
            this.comboBoxCourse.TabIndex = 18;
            this.comboBoxCourse.TextChanged += new System.EventHandler(this.comboBoxCourse_TextChanged);
            // 
            // comboBoxSpec
            // 
            this.comboBoxSpec.FormattingEnabled = true;
            this.comboBoxSpec.Location = new System.Drawing.Point(124, 163);
            this.comboBoxSpec.Name = "comboBoxSpec";
            this.comboBoxSpec.Size = new System.Drawing.Size(103, 27);
            this.comboBoxSpec.TabIndex = 18;
            this.comboBoxSpec.TextChanged += new System.EventHandler(this.comboBoxSpec_TextChanged);
            this.comboBoxSpec.MouseClick += new System.Windows.Forms.MouseEventHandler(this.comboBoxSpec_MouseClick);
            // 
            // radioButtonProfessors
            // 
            this.radioButtonProfessors.AutoSize = true;
            this.radioButtonProfessors.Location = new System.Drawing.Point(367, 102);
            this.radioButtonProfessors.Name = "radioButtonProfessors";
            this.radioButtonProfessors.Size = new System.Drawing.Size(149, 23);
            this.radioButtonProfessors.TabIndex = 17;
            this.radioButtonProfessors.Text = "Преподаватели";
            this.radioButtonProfessors.UseVisualStyleBackColor = true;
            this.radioButtonProfessors.CheckedChanged += new System.EventHandler(this.radioButtonProfessors_CheckedChanged);
            // 
            // radioButtonStudents
            // 
            this.radioButtonStudents.AutoSize = true;
            this.radioButtonStudents.Checked = true;
            this.radioButtonStudents.Location = new System.Drawing.Point(81, 102);
            this.radioButtonStudents.Name = "radioButtonStudents";
            this.radioButtonStudents.Size = new System.Drawing.Size(103, 23);
            this.radioButtonStudents.TabIndex = 16;
            this.radioButtonStudents.TabStop = true;
            this.radioButtonStudents.Text = "Студенти";
            this.radioButtonStudents.UseVisualStyleBackColor = true;
            this.radioButtonStudents.CheckedChanged += new System.EventHandler(this.radioButtonStudents_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(0, 116);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(590, 590);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(1499, 708);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.successfullyGen);
            this.Controls.Add(this.generating);
            this.Controls.Add(this.generatingProgressBar);
            this.Controls.Add(this.generateButton);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.uploadDir);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Генератор на учебни разписи";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button uploadDir;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button generateButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar generatingProgressBar;
        private System.Windows.Forms.Label successfullyGen;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button Showbutton;
        public System.Windows.Forms.RadioButton radioButtonProfessors;
        public System.Windows.Forms.RadioButton radioButtonStudents;
        public System.Windows.Forms.ComboBox comboBoxCourse;
        public System.Windows.Forms.ComboBox comboBoxSpec;
        public System.Windows.Forms.ComboBox comboBoxGroup;
        public System.Windows.Forms.ComboBox comboBoxProfessor;
        public System.Windows.Forms.ComboBox comboBoxDepart;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button saveProfSchedulesButton;
        private System.Windows.Forms.Button saveStudSchedulesButton;
        private System.Windows.Forms.Button saveRoomSchedulesButton;
        private System.Windows.Forms.RadioButton roomsRadioButton;
        private System.Windows.Forms.Label roomLabel;
        private System.Windows.Forms.ComboBox comboBoxRooms;
        public System.Windows.Forms.Label generating;
    }
}

