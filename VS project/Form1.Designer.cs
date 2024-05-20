
namespace SchoolTimetebale
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

            #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.ColumnDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comboBoxDay = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxGroup = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DeleteTeacher = new System.Windows.Forms.Button();
            this.DeleteSubject = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxClassroom = new System.Windows.Forms.MaskedTextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.comboBoxTeacher = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxSubject = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.DeleteGroup = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnDate});
            this.dataGridView.Location = new System.Drawing.Point(23, 62);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersWidth = 51;
            this.dataGridView.RowTemplate.Height = 24;
            this.dataGridView.Size = new System.Drawing.Size(579, 228);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);
            // 
            // ColumnDate
            // 
            this.ColumnDate.HeaderText = "№Урока";
            this.ColumnDate.MinimumWidth = 6;
            this.ColumnDate.Name = "ColumnDate";
            this.ColumnDate.Width = 125;
            // 
            // comboBoxDay
            // 
            this.comboBoxDay.DisplayMember = "1";
            this.comboBoxDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDay.FormattingEnabled = true;
            this.comboBoxDay.Items.AddRange(new object[] {
            "Понедельник",
            "Вторник",
            "Среда",
            "Четверг",
            "Пятница",
            "Суббота"});
            this.comboBoxDay.Location = new System.Drawing.Point(121, 21);
            this.comboBoxDay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxDay.Name = "comboBoxDay";
            this.comboBoxDay.Size = new System.Drawing.Size(145, 24);
            this.comboBoxDay.TabIndex = 1;
            this.comboBoxDay.SelectedIndexChanged += new System.EventHandler(this.comboBoxDay_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "День тижня";
            // 
            // comboBoxGroup
            // 
            this.comboBoxGroup.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.comboBoxGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGroup.FormattingEnabled = true;
            this.comboBoxGroup.Location = new System.Drawing.Point(413, 21);
            this.comboBoxGroup.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxGroup.Name = "comboBoxGroup";
            this.comboBoxGroup.Size = new System.Drawing.Size(153, 24);
            this.comboBoxGroup.TabIndex = 3;
            this.comboBoxGroup.SelectedIndexChanged += new System.EventHandler(this.comboBoxGroup_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(358, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Група";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.DeleteTeacher);
            this.groupBox1.Controls.Add(this.DeleteSubject);
            this.groupBox1.Controls.Add(this.buttonDelete);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textBoxClassroom);
            this.groupBox1.Controls.Add(this.buttonSave);
            this.groupBox1.Controls.Add(this.comboBoxTeacher);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.comboBoxSubject);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(626, 58);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(258, 258);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // DeleteTeacher
            // 
            this.DeleteTeacher.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.DeleteTeacher.Location = new System.Drawing.Point(223, 90);
            this.DeleteTeacher.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DeleteTeacher.Name = "DeleteTeacher";
            this.DeleteTeacher.Size = new System.Drawing.Size(29, 27);
            this.DeleteTeacher.TabIndex = 11;
            this.DeleteTeacher.Text = "❌";
            this.DeleteTeacher.UseVisualStyleBackColor = true;
            this.DeleteTeacher.Click += new System.EventHandler(this.DeleteTeacher_Click);
            // 
            // DeleteSubject
            // 
            this.DeleteSubject.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.DeleteSubject.Location = new System.Drawing.Point(223, 38);
            this.DeleteSubject.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DeleteSubject.Name = "DeleteSubject";
            this.DeleteSubject.Size = new System.Drawing.Size(29, 27);
            this.DeleteSubject.TabIndex = 10;
            this.DeleteSubject.Text = "❌";
            this.DeleteSubject.UseVisualStyleBackColor = true;
            this.DeleteSubject.Click += new System.EventHandler(this.DeleteSubject_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.buttonDelete.Location = new System.Drawing.Point(67, 186);
            this.buttonDelete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(108, 27);
            this.buttonDelete.TabIndex = 9;
            this.buttonDelete.Text = "Видалити";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(85, 129);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "№ кімнати";
            // 
            // textBoxClassroom
            // 
            this.textBoxClassroom.Location = new System.Drawing.Point(109, 148);
            this.textBoxClassroom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxClassroom.Mask = "099";
            this.textBoxClassroom.Name = "textBoxClassroom";
            this.textBoxClassroom.Size = new System.Drawing.Size(29, 22);
            this.textBoxClassroom.TabIndex = 7;
            this.textBoxClassroom.TextChanged += new System.EventHandler(this.textBoxClassroom_TextChanged);
            // 
            // buttonSave
            // 
            this.buttonSave.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.buttonSave.Location = new System.Drawing.Point(67, 217);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(108, 27);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "Зберегти";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // comboBoxTeacher
            // 
            this.comboBoxTeacher.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTeacher.FormattingEnabled = true;
            this.comboBoxTeacher.Location = new System.Drawing.Point(5, 92);
            this.comboBoxTeacher.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxTeacher.Name = "comboBoxTeacher";
            this.comboBoxTeacher.Size = new System.Drawing.Size(212, 24);
            this.comboBoxTeacher.TabIndex = 3;
            this.comboBoxTeacher.DropDown += new System.EventHandler(this.comboBoxTeacher_DropDown);
            this.comboBoxTeacher.SelectedIndexChanged += new System.EventHandler(this.comboBoxTeacher_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(85, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "Вчитель";
            // 
            // comboBoxSubject
            // 
            this.comboBoxSubject.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxSubject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSubject.FormattingEnabled = true;
            this.comboBoxSubject.Location = new System.Drawing.Point(5, 38);
            this.comboBoxSubject.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxSubject.Name = "comboBoxSubject";
            this.comboBoxSubject.Size = new System.Drawing.Size(212, 24);
            this.comboBoxSubject.TabIndex = 1;
            this.comboBoxSubject.DropDown += new System.EventHandler(this.comboBoxSubject_DropDown);
            this.comboBoxSubject.SelectedIndexChanged += new System.EventHandler(this.comboBoxSubject_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(85, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Предмет";
            // 
            // DeleteGroup
            // 
            this.DeleteGroup.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.DeleteGroup.Location = new System.Drawing.Point(572, 21);
            this.DeleteGroup.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DeleteGroup.Name = "DeleteGroup";
            this.DeleteGroup.Size = new System.Drawing.Size(29, 27);
            this.DeleteGroup.TabIndex = 12;
            this.DeleteGroup.Text = "❌";
            this.DeleteGroup.UseVisualStyleBackColor = true;
            this.DeleteGroup.Click += new System.EventHandler(this.DeleteGroup_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(902, 313);
            this.Controls.Add(this.DeleteGroup);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxGroup);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxDay);
            this.Controls.Add(this.dataGridView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(643, 360);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.ComboBox comboBoxDay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDate;
        private System.Windows.Forms.ComboBox comboBoxGroup;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxTeacher;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxSubject;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.MaskedTextBox textBoxClassroom;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button DeleteSubject;
        private System.Windows.Forms.Button DeleteTeacher;
        private System.Windows.Forms.Button DeleteGroup;
    }
}

