namespace Grading_System.ChildForms
{
    partial class ManageGrades
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbSection = new System.Windows.Forms.ComboBox();
            this.cbStudent = new System.Windows.Forms.ComboBox();
            this.cbSubjects = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tblList = new System.Windows.Forms.DataGridView();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnExportBook = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btnExportCard = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblList)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbSection);
            this.panel1.Controls.Add(this.cbStudent);
            this.panel1.Controls.Add(this.cbSubjects);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1050, 184);
            this.panel1.TabIndex = 5;
            // 
            // cbSection
            // 
            this.cbSection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSection.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cbSection.FormattingEnabled = true;
            this.cbSection.Location = new System.Drawing.Point(169, 17);
            this.cbSection.Name = "cbSection";
            this.cbSection.Size = new System.Drawing.Size(292, 33);
            this.cbSection.TabIndex = 0;
            this.cbSection.DropDown += new System.EventHandler(this.cbSection_DropDown);
            this.cbSection.SelectedValueChanged += new System.EventHandler(this.cbSection_SelectedValueChanged);
            // 
            // cbStudent
            // 
            this.cbStudent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStudent.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cbStudent.FormattingEnabled = true;
            this.cbStudent.Location = new System.Drawing.Point(169, 130);
            this.cbStudent.Name = "cbStudent";
            this.cbStudent.Size = new System.Drawing.Size(292, 33);
            this.cbStudent.TabIndex = 1;
            this.cbStudent.SelectedValueChanged += new System.EventHandler(this.cbStudent_SelectedValueChanged);
            // 
            // cbSubjects
            // 
            this.cbSubjects.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSubjects.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cbSubjects.FormattingEnabled = true;
            this.cbSubjects.Location = new System.Drawing.Point(169, 75);
            this.cbSubjects.Name = "cbSubjects";
            this.cbSubjects.Size = new System.Drawing.Size(292, 33);
            this.cbSubjects.TabIndex = 2;
            this.cbSubjects.SelectedValueChanged += new System.EventHandler(this.cbSubjects_SelectedValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label3.Location = new System.Drawing.Point(13, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 25);
            this.label3.TabIndex = 0;
            this.label3.Text = "Subject:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.Location = new System.Drawing.Point(13, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "Student:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(13, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Section:";
            // 
            // tblList
            // 
            this.tblList.AllowUserToAddRows = false;
            this.tblList.AllowUserToDeleteRows = false;
            this.tblList.AllowUserToResizeColumns = false;
            this.tblList.AllowUserToResizeRows = false;
            this.tblList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tblList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tblList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tblList.Location = new System.Drawing.Point(13, 190);
            this.tblList.Name = "tblList";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = null;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tblList.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.tblList.RowHeadersVisible = false;
            this.tblList.RowHeadersWidth = 51;
            this.tblList.RowTemplate.Height = 24;
            this.tblList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tblList.Size = new System.Drawing.Size(1025, 430);
            this.tblList.TabIndex = 19;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.Enabled = false;
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnUpdate.Location = new System.Drawing.Point(832, 626);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(100, 44);
            this.btnUpdate.TabIndex = 10;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnCancel.Location = new System.Drawing.Point(938, 626);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 44);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnImport.Enabled = false;
            this.btnImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnImport.Location = new System.Drawing.Point(12, 626);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(100, 44);
            this.btnImport.TabIndex = 12;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnExportBook
            // 
            this.btnExportBook.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportBook.Enabled = false;
            this.btnExportBook.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnExportBook.Location = new System.Drawing.Point(259, 626);
            this.btnExportBook.Name = "btnExportBook";
            this.btnExportBook.Size = new System.Drawing.Size(173, 44);
            this.btnExportBook.TabIndex = 11;
            this.btnExportBook.Text = "Grade Book";
            this.btnExportBook.UseVisualStyleBackColor = true;
            this.btnExportBook.Click += new System.EventHandler(this.btnExportBook_Click);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label6.Location = new System.Drawing.Point(164, 636);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 25);
            this.label6.TabIndex = 9;
            this.label6.Text = "Export:";
            // 
            // btnExportCard
            // 
            this.btnExportCard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportCard.Enabled = false;
            this.btnExportCard.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnExportCard.Location = new System.Drawing.Point(438, 626);
            this.btnExportCard.Name = "btnExportCard";
            this.btnExportCard.Size = new System.Drawing.Size(173, 44);
            this.btnExportCard.TabIndex = 20;
            this.btnExportCard.Text = "Grade Card";
            this.btnExportCard.UseVisualStyleBackColor = true;
            this.btnExportCard.Click += new System.EventHandler(this.btnExportCard_Click);
            // 
            // ManageGrades
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1050, 673);
            this.Controls.Add(this.btnExportCard);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnExportBook);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.tblList);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ManageGrades";
            this.Text = "ManageGrades";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbStudent;
        private System.Windows.Forms.ComboBox cbSubjects;
        private System.Windows.Forms.ComboBox cbSection;
        private System.Windows.Forms.DataGridView tblList;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnExportBook;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnExportCard;
    }
}