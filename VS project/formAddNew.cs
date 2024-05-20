using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolTimetebale
{
    public partial class formAddNew : Form
    {
        Form1 form1;
        ConnectDB db;
        string param;
        delegate void func();
        func addFunc;
        public formAddNew(Form1 form1, ConnectDB db, int mode, string param = "")
        {
            InitializeComponent();
            this.form1 = form1;
            this.db = db;
            this.param = param;
            switch (mode)
            {
                case 0:
                    addFunc = AddTeacher;
                    label1.Text += "ім'я вчителя:";
                    break;
                case 1:
                    addFunc = AddSubject;
                    label1.Text += "назву предмета:";
                    break;
                case 2:
                    addFunc = AddGroup;
                    label1.Text += "назву групи (число та буква):";
                    maskedTextBox1.Mask = "90-L";
                    break;
            }
        }
        public void AddTeacher()
        {
            if (db.GetInt($"SELECT id_teacher From Teachers WHERE full_name = N'{maskedTextBox1.Text}'") == -999) {
                if (maskedTextBox1.Text.Length < 5)
                    MessageBox.Show("Дуже коротке ім'я");
                else if (maskedTextBox1.Text.Length > 40)
                    MessageBox.Show("Дуже довге ім'я");
                else
                {
                    db.SaveData($"INSERT INTO Teachers(full_name,id_subject) VALUES (N'{maskedTextBox1.Text}',{param})");
                    close_Form();
                }
            }
            else
            {
                MessageBox.Show("Вчитель з таким ім'ям вже існує");
            }
        }
        public void AddSubject()
        {
            if (db.GetInt($"SELECT id From Subjects WHERE subject_name = N'{maskedTextBox1.Text}'") == -999)
            {
                if (maskedTextBox1.Text.Length < 3)
                    MessageBox.Show("Дуже коротка назва");
                else if (maskedTextBox1.Text.Length > 20)
                    MessageBox.Show("Дуже довга назва");
                else
                {
                    db.SaveData($"INSERT INTO Subjects(subject_name) VALUES (N'{maskedTextBox1.Text}')");
                    close_Form();
                }
            }
            else
            {
                MessageBox.Show("Такий предмет вже існує");
            }
        }
        public void AddGroup()
        {
            if (db.GetInt($"SELECT id_group From Groups WHERE group_name = N'{maskedTextBox1.Text}'") == -999)
            {
                if (maskedTextBox1.Text.Length < 2)
                    MessageBox.Show("Дуже коротка назва");
                else
                {
                    db.SaveData($"INSERT INTO Groups(group_name) VALUES (N'{maskedTextBox1.Text.Replace(" ","")}')");
                    form1.UpdateGroups();
                    close_Form();
                }
            }
            else
            {
                MessageBox.Show("Така група вже існує");
            }
        }
        private void cansel_Click(object sender, EventArgs e)
        {
            close_Form();
        }
        private void close_Form()
        {
            form1.Enabled = true;
            Close();
        }
        private void add_Click(object sender, EventArgs e)
        {
            addFunc();
        }
        
        private void maskedTextBox1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Escape)
            {
                close_Form();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                addFunc();
            }

        }
    }
}
