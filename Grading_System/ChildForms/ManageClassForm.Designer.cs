namespace Grading_System.ChildForms
{
    partial class ManageClassForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabManage = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbTeacher = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tblSubjectList = new System.Windows.Forms.DataGridView();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.tabAssign = new System.Windows.Forms.TabPage();
            this.btnClassCancel = new System.Windows.Forms.Button();
            this.btnClassUpdate = new System.Windows.Forms.Button();
            this.tblClass = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbSection = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabManage.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblSubjectList)).BeginInit();
            this.tabAssign.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblClass)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabManage);
            this.tabControl1.Controls.Add(this.tabAssign);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1050, 673);
            this.tabControl1.TabIndex = 0;
            // 
            // tabManage
            // 
            this.tabManage.Controls.Add(this.panel1);
            this.tabManage.Controls.Add(this.btnCancel);
            this.tabManage.Controls.Add(this.tblSubjectList);
            this.tabManage.Controls.Add(this.btnUpdate);
            this.tabManage.Location = new System.Drawing.Point(4, 25);
            this.tabManage.Name = "tabManage";
            this.tabManage.Padding = new System.Windows.Forms.Padding(3);
            this.tabManage.Size = new System.Drawing.Size(1042, 644);
            this.tabManage.TabIndex = 0;
            this.tabManage.Text = "Manage";
            this.tabManage.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbTeacher);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1036, 70);
            this.panel1.TabIndex = 9;
            // 
            // cbTeacher
            // 
            this.cbTeacher.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTeacher.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cbTeacher.FormattingEnabled = true;
            this.cbTeacher.Location = new System.Drawing.Point(219, 20);
            this.cbTeacher.Name = "cbTeacher";
            this.cbTeacher.Size = new System.Drawing.Size(292, 33);
            this.cbTeacher.TabIndex = 2;
            this.cbTeacher.DropDown += new System.EventHandler(this.cbTeacher_DropDown);
            this.cbTeacher.SelectedValueChanged += new System.EventHandler(this.cbTeacher_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(13, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Choose Teacher:";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnCancel.Location = new System.Drawing.Point(937, 592);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 44);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tblSubjectList
            // 
            this.tblSubjectList.AllowUserToAddRows = false;
            this.tblSubjectList.AllowUserToResizeColumns = false;
            this.tblSubjectList.AllowUserToResizeRows = false;
            this.tblSubjectList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tblSubjectList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tblSubjectList.Location = new System.Drawing.Point(12, 79);
            this.tblSubjectList.Name = "tblSubjectList";
            this.tblSubjectList.RowHeadersWidth = 51;
            this.tblSubjectList.RowTemplate.Height = 24;
            this.tblSubjectList.Size = new System.Drawing.Size(1025, 507);
            this.tblSubjectList.TabIndex = 11;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnUpdate.Location = new System.Drawing.Point(831, 592);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(100, 44);
            this.btnUpdate.TabIndex = 10;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // tabAssign
            // 
            this.tabAssign.Controls.Add(this.btnClassCancel);
            this.tabAssign.Controls.Add(this.btnClassUpdate);
            this.tabAssign.Controls.Add(this.tblClass);
            this.tabAssign.Controls.Add(this.panel2);
            this.tabAssign.Location = new System.Drawing.Point(4, 25);
            this.tabAssign.Name = "tabAssign";
            this.tabAssign.Padding = new System.Windows.Forms.Padding(3);
            this.tabAssign.Size = new System.Drawing.Size(1042, 644);
            this.tabAssign.TabIndex = 1;
            this.tabAssign.Text = "Assign";
            this.tabAssign.UseVisualStyleBackColor = true;
            // 
            // btnClassCancel
            // 
            this.btnClassCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClassCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnClassCancel.Location = new System.Drawing.Point(934, 592);
            this.btnClassCancel.Name = "btnClassCancel";
            this.btnClassCancel.Size = new System.Drawing.Size(100, 44);
            this.btnClassCancel.TabIndex = 13;
            this.btnClassCancel.Text = "Cancel";
            this.btnClassCancel.UseVisualStyleBackColor = true;
            // 
            // btnClassUpdate
            // 
            this.btnClassUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClassUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnClassUpdate.Location = new System.Drawing.Point(828, 592);
            this.btnClassUpdate.Name = "btnClassUpdate";
            this.btnClassUpdate.Size = new System.Drawing.Size(100, 44);
            this.btnClassUpdate.TabIndex = 14;
            this.btnClassUpdate.Text = "Update";
            this.btnClassUpdate.UseVisualStyleBackColor = true;
            this.btnClassUpdate.Click += new System.EventHandler(this.btnClassUpdate_Click);
            // 
            // tblClass
            // 
            this.tblClass.AllowUserToAddRows = false;
            this.tblClass.AllowUserToResizeColumns = false;
            this.tblClass.AllowUserToResizeRows = false;
            this.tblClass.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tblClass.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tblClass.Location = new System.Drawing.Point(9, 79);
            this.tblClass.Name = "tblClass";
            this.tblClass.RowHeadersWidth = 51;
            this.tblClass.RowTemplate.Height = 24;
            this.tblClass.Size = new System.Drawing.Size(1025, 507);
            this.tblClass.TabIndex = 12;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cbSection);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1036, 70);
            this.panel2.TabIndex = 10;
            // 
            // cbSection
            // 
            this.cbSection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSection.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cbSection.FormattingEnabled = true;
            this.cbSection.Location = new System.Drawing.Point(219, 20);
            this.cbSection.Name = "cbSection";
            this.cbSection.Size = new System.Drawing.Size(292, 33);
            this.cbSection.TabIndex = 4;
            this.cbSection.DropDown += new System.EventHandler(this.cbSection_DropDown);
            this.cbSection.SelectedValueChanged += new System.EventHandler(this.cbSection_SelectedValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label3.Location = new System.Drawing.Point(13, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(158, 25);
            this.label3.TabIndex = 3;
            this.label3.Text = "Choose Section:";
            // 
            // ManageClassForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1050, 673);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ManageClassForm";
            this.Text = "ManageClass";
            this.tabControl1.ResumeLayout(false);
            this.tabManage.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblSubjectList)).EndInit();
            this.tabAssign.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tblClass)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabManage;
        private System.Windows.Forms.TabPage tabAssign;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridView tblSubjectList;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.ComboBox cbTeacher;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cbSection;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView tblClass;
        private System.Windows.Forms.Button btnClassCancel;
        private System.Windows.Forms.Button btnClassUpdate;
    }
}