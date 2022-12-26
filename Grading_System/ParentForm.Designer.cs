namespace Grading_System
{
    partial class ParentForm
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
            this.pnlTaskbar = new System.Windows.Forms.Panel();
            this.Options = new System.Windows.Forms.GroupBox();
            this.rbtnAddRegistrar = new System.Windows.Forms.RadioButton();
            this.rbtnAddSection = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.rbtnAddAsstTeacher = new System.Windows.Forms.RadioButton();
            this.rbtnAddSubject = new System.Windows.Forms.RadioButton();
            this.rbtnAddTeacher = new System.Windows.Forms.RadioButton();
            this.rbtnAddStudent = new System.Windows.Forms.RadioButton();
            this.rbtnGrades = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblPosition = new System.Windows.Forms.Label();
            this.pnlTaskbar.SuspendLayout();
            this.Options.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTaskbar
            // 
            this.pnlTaskbar.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pnlTaskbar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pnlTaskbar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlTaskbar.Controls.Add(this.Options);
            this.pnlTaskbar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlTaskbar.Location = new System.Drawing.Point(0, 0);
            this.pnlTaskbar.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTaskbar.Name = "pnlTaskbar";
            this.pnlTaskbar.Size = new System.Drawing.Size(212, 673);
            this.pnlTaskbar.TabIndex = 0;
            // 
            // Options
            // 
            this.Options.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Options.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Options.Controls.Add(this.rbtnAddRegistrar);
            this.Options.Controls.Add(this.rbtnAddSection);
            this.Options.Controls.Add(this.panel2);
            this.Options.Controls.Add(this.btnLogout);
            this.Options.Controls.Add(this.rbtnAddAsstTeacher);
            this.Options.Controls.Add(this.rbtnAddSubject);
            this.Options.Controls.Add(this.rbtnAddTeacher);
            this.Options.Controls.Add(this.rbtnAddStudent);
            this.Options.Controls.Add(this.rbtnGrades);
            this.Options.Controls.Add(this.panel1);
            this.Options.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Options.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Options.Font = new System.Drawing.Font("Microsoft Sans Serif", 0.01F);
            this.Options.Location = new System.Drawing.Point(0, 0);
            this.Options.Margin = new System.Windows.Forms.Padding(0);
            this.Options.Name = "Options";
            this.Options.Padding = new System.Windows.Forms.Padding(0);
            this.Options.Size = new System.Drawing.Size(208, 669);
            this.Options.TabIndex = 0;
            this.Options.TabStop = false;
            // 
            // rbtnAddRegistrar
            // 
            this.rbtnAddRegistrar.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnAddRegistrar.AutoSize = true;
            this.rbtnAddRegistrar.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.rbtnAddRegistrar.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbtnAddRegistrar.FlatAppearance.BorderSize = 0;
            this.rbtnAddRegistrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnAddRegistrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.rbtnAddRegistrar.Location = new System.Drawing.Point(0, 365);
            this.rbtnAddRegistrar.MinimumSize = new System.Drawing.Size(208, 52);
            this.rbtnAddRegistrar.Name = "rbtnAddRegistrar";
            this.rbtnAddRegistrar.Size = new System.Drawing.Size(208, 52);
            this.rbtnAddRegistrar.TabIndex = 6;
            this.rbtnAddRegistrar.TabStop = true;
            this.rbtnAddRegistrar.Text = "Add Registrar";
            this.rbtnAddRegistrar.UseVisualStyleBackColor = false;
            this.rbtnAddRegistrar.Click += new System.EventHandler(this.btnAddRegistrar_Click);
            // 
            // rbtnAddSection
            // 
            this.rbtnAddSection.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnAddSection.AutoSize = true;
            this.rbtnAddSection.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.rbtnAddSection.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbtnAddSection.FlatAppearance.BorderSize = 0;
            this.rbtnAddSection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnAddSection.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.rbtnAddSection.Location = new System.Drawing.Point(0, 313);
            this.rbtnAddSection.MinimumSize = new System.Drawing.Size(208, 52);
            this.rbtnAddSection.Name = "rbtnAddSection";
            this.rbtnAddSection.Size = new System.Drawing.Size(208, 52);
            this.rbtnAddSection.TabIndex = 5;
            this.rbtnAddSection.TabStop = true;
            this.rbtnAddSection.Text = "Add Section";
            this.rbtnAddSection.UseVisualStyleBackColor = false;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 313);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(208, 304);
            this.panel2.TabIndex = 4;
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnLogout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnLogout.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnLogout.Location = new System.Drawing.Point(0, 617);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(208, 52);
            this.btnLogout.TabIndex = 0;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // rbtnAddAsstTeacher
            // 
            this.rbtnAddAsstTeacher.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnAddAsstTeacher.AutoSize = true;
            this.rbtnAddAsstTeacher.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.rbtnAddAsstTeacher.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbtnAddAsstTeacher.FlatAppearance.BorderSize = 0;
            this.rbtnAddAsstTeacher.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnAddAsstTeacher.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.rbtnAddAsstTeacher.Location = new System.Drawing.Point(0, 261);
            this.rbtnAddAsstTeacher.MinimumSize = new System.Drawing.Size(208, 52);
            this.rbtnAddAsstTeacher.Name = "rbtnAddAsstTeacher";
            this.rbtnAddAsstTeacher.Size = new System.Drawing.Size(208, 52);
            this.rbtnAddAsstTeacher.TabIndex = 3;
            this.rbtnAddAsstTeacher.TabStop = true;
            this.rbtnAddAsstTeacher.Text = "Add Asst. Teacher";
            this.rbtnAddAsstTeacher.UseVisualStyleBackColor = false;
            this.rbtnAddAsstTeacher.CheckedChanged += new System.EventHandler(this.rbtnAddAsstTeacher_CheckedChanged);
            // 
            // rbtnAddSubject
            // 
            this.rbtnAddSubject.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnAddSubject.AutoSize = true;
            this.rbtnAddSubject.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.rbtnAddSubject.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbtnAddSubject.FlatAppearance.BorderSize = 0;
            this.rbtnAddSubject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnAddSubject.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.rbtnAddSubject.Location = new System.Drawing.Point(0, 209);
            this.rbtnAddSubject.MinimumSize = new System.Drawing.Size(208, 52);
            this.rbtnAddSubject.Name = "rbtnAddSubject";
            this.rbtnAddSubject.Size = new System.Drawing.Size(208, 52);
            this.rbtnAddSubject.TabIndex = 2;
            this.rbtnAddSubject.TabStop = true;
            this.rbtnAddSubject.Text = "Add Subject";
            this.rbtnAddSubject.UseVisualStyleBackColor = false;
            this.rbtnAddSubject.Click += new System.EventHandler(this.btnAddSubject_Click);
            // 
            // rbtnAddTeacher
            // 
            this.rbtnAddTeacher.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnAddTeacher.AutoSize = true;
            this.rbtnAddTeacher.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.rbtnAddTeacher.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbtnAddTeacher.FlatAppearance.BorderSize = 0;
            this.rbtnAddTeacher.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnAddTeacher.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.rbtnAddTeacher.Location = new System.Drawing.Point(0, 157);
            this.rbtnAddTeacher.MinimumSize = new System.Drawing.Size(208, 52);
            this.rbtnAddTeacher.Name = "rbtnAddTeacher";
            this.rbtnAddTeacher.Size = new System.Drawing.Size(208, 52);
            this.rbtnAddTeacher.TabIndex = 1;
            this.rbtnAddTeacher.TabStop = true;
            this.rbtnAddTeacher.Text = "Add Teacher";
            this.rbtnAddTeacher.UseVisualStyleBackColor = false;
            this.rbtnAddTeacher.Click += new System.EventHandler(this.btnAddTeacher_Click);
            // 
            // rbtnAddStudent
            // 
            this.rbtnAddStudent.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnAddStudent.AutoSize = true;
            this.rbtnAddStudent.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.rbtnAddStudent.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbtnAddStudent.FlatAppearance.BorderSize = 0;
            this.rbtnAddStudent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnAddStudent.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.rbtnAddStudent.Location = new System.Drawing.Point(0, 105);
            this.rbtnAddStudent.MinimumSize = new System.Drawing.Size(208, 52);
            this.rbtnAddStudent.Name = "rbtnAddStudent";
            this.rbtnAddStudent.Size = new System.Drawing.Size(208, 52);
            this.rbtnAddStudent.TabIndex = 0;
            this.rbtnAddStudent.TabStop = true;
            this.rbtnAddStudent.Text = "Add Student";
            this.rbtnAddStudent.UseVisualStyleBackColor = false;
            this.rbtnAddStudent.Click += new System.EventHandler(this.btnAddStudent_Click);
            // 
            // rbtnGrades
            // 
            this.rbtnGrades.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnGrades.AutoSize = true;
            this.rbtnGrades.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.rbtnGrades.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbtnGrades.FlatAppearance.BorderSize = 0;
            this.rbtnGrades.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnGrades.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.rbtnGrades.Location = new System.Drawing.Point(0, 53);
            this.rbtnGrades.MinimumSize = new System.Drawing.Size(208, 52);
            this.rbtnGrades.Name = "rbtnGrades";
            this.rbtnGrades.Size = new System.Drawing.Size(208, 52);
            this.rbtnGrades.TabIndex = 0;
            this.rbtnGrades.Text = "Grades";
            this.rbtnGrades.UseVisualStyleBackColor = false;
            this.rbtnGrades.Click += new System.EventHandler(this.btnGrades_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblPosition);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(208, 52);
            this.panel1.TabIndex = 2;
            // 
            // lblPosition
            // 
            this.lblPosition.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPosition.AutoSize = true;
            this.lblPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblPosition.Location = new System.Drawing.Point(10, 13);
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new System.Drawing.Size(68, 25);
            this.lblPosition.TabIndex = 1;
            this.lblPosition.Text = "Admin";
            // 
            // ParentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.pnlTaskbar);
            this.IsMdiContainer = true;
            this.Name = "ParentForm";
            this.Text = "Grading System";
            this.pnlTaskbar.ResumeLayout(false);
            this.Options.ResumeLayout(false);
            this.Options.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTaskbar;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Label lblPosition;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox Options;
        private System.Windows.Forms.RadioButton rbtnGrades;
        private System.Windows.Forms.RadioButton rbtnAddStudent;
        private System.Windows.Forms.RadioButton rbtnAddAsstTeacher;
        private System.Windows.Forms.RadioButton rbtnAddSubject;
        private System.Windows.Forms.RadioButton rbtnAddTeacher;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rbtnAddSection;
        private System.Windows.Forms.RadioButton rbtnAddRegistrar;
    }
}

