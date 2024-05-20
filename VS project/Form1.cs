using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolTimetebale
{
    public partial class Form1 : Form
    {
        //строка підключення
        static string connString = @"Data Source=DESKTOP-0RP4IKA\MSSQLLOCALDB;Initial Catalog=obdz_cursova;Integrated Security=True";
        //підключення до бд
        static ConnectDB db = new ConnectDB(connString);
        //сховища данних
        static Dictionary<int, string> groups = new Dictionary<int, string>();
        static Dictionary<int, Dictionary<int, string>> lessons = new Dictionary<int, Dictionary<int, string>>();
        static string cur_lesson = "", cur_day = "1", cur_group = "", cur_subject = "";
        static int cur_classroom;
        static bool is_update = false;
        bool flag_start = false;
        static Form1 form1;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)

        {

            form1 = this;
            //вимкнути кнопки видаленя та збереження
            buttonSave.Enabled = false;
            buttonDelete.Enabled = false;
            DeleteGroup.Enabled = false;
            DeleteSubject.Enabled = false;
            DeleteTeacher.Enabled = false;
            textBoxClassroom.Enabled = false;
            //отримати основні дані
            comboBoxDay.SelectedIndex = 0;
            GetGroupsFromBD();
            SetComboBoxGroupe(comboBoxGroup);
            GetLessonsFromBD(comboBoxDay.SelectedIndex+1);
            comboBoxGroup.SelectedIndex = 1;
            //заповнити datagrid
            SetDataGrid("Всі групи");
            flag_start = true;
        }

        //заповнення таблиці даними
        private static void SetDataGrid(string selectGroup)
        {
            //очистити таблицю
            form1.dataGridView.Rows.Clear();
            form1.dataGridView.Columns.Clear();
            //запонити перший стовбець номерами уроку
            form1.dataGridView.Columns.Add("Lesson_number", "№Урока");
            form1.dataGridView.Rows.Add(5);
            for(int i = 0; i < 6; i++)
                form1.dataGridView.Rows[i].Cells[0].Value = i+1;
            //якщо вибрани усі групи
            if (selectGroup.CompareTo("Всі групи") == 0)
            {
                int j = 0;
                //перебрати усі групи
                foreach (var group in groups)
                {
                    form1.dataGridView.Columns.Add(group.Key.ToString(), group.Value);
                    int i = 0;
                    foreach (var lesson in lessons[group.Key])
                    {
                        form1.dataGridView.Rows[i].Cells[j+1].Value = lesson.Value;
                        i++;
                    }
                    j++;
                }
            }
            else //інакше
            {
                int i = 0;
                //вивести значення обраної групи
                form1.dataGridView.Columns.Add(GetKeyFromValue(groups,selectGroup).ToString(), selectGroup);
                int key = GetKeyFromValue(groups, selectGroup);
                if (key != 0)
                foreach (var lesson in lessons[key])
                {
                    form1.dataGridView.Rows[i].Cells[1].Value = lesson.Value;
                    i++;
                }
            }
            form1.dataGridView.AutoResizeColumn(0);
            for (int i = 0; i < form1.dataGridView.Columns.Count; i++)
            {
                form1.dataGridView.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                form1.dataGridView.Columns[i].ReadOnly = true;
            }
        }
        //заповнити ComboBox предметами
        private static void SetComboBoxSubject(ComboBox box)
        {
            box.Items.Clear();
            box.Items.Add("+Додати предмет");
            var data = db.GetData($"SELECT * FROM Subjects");
            while (data != null && data.Read())
            {
                box.Items.Add(data["subject_name"]);
                if (data["subject_name"].ToString().CompareTo(cur_subject) == 0)
                {
                    box.SelectedIndex = box.Items.Count - 1;
                }
            }
        }
        //заповнити ComboBox вчителями
        private static void SetComboBoxTeacher(ComboBox box, MaskedTextBox boxClassroom)
        {
            box.SelectedIndex = -1;
            box.Items.Clear();
            if (form1.comboBoxSubject.SelectedIndex <= 0)
            {
                box.Enabled = false;
            }
            else
            {
                box.Items.Add("+Додати вчителя");
                //Знайти викладача обраного урока
                var cur_teacher = db.GetString($"SELECT full_name FROM Lessons INNER JOIN Teachers ON Teachers.id_teacher = Lessons.id_teacher WHERE Lessons.day_of_week = {cur_day} AND Lessons.id_group = {cur_group} AND Lessons.lesson_number = {cur_lesson}");
                if (cur_teacher != null)
                {
                    is_update = true;
                    int.TryParse(boxClassroom.Text, out cur_classroom);
                    box.Items.Add(cur_teacher);
                    box.SelectedIndex = box.Items.Count - 1;
                }
                else
                {
                    is_update = false;
                }
                //Вибрати усіх викладачів, які не проводять уроки у цей момент, та відповідають за цей предмет 
                var data = db.GetData($"SELECT full_name FROM Teachers INNER JOIN Subjects ON Teachers.id_subject = Subjects.id WHERE subject_name = N'{cur_subject}' EXCEPT SELECT full_name FROM Lessons FULL OUTER JOIN(Teachers INNER JOIN Subjects ON Teachers.id_subject = Subjects.id) ON Teachers.id_teacher = Lessons.id_teacher WHERE day_of_week = {cur_day} AND lesson_number = {cur_lesson} ORDER BY full_name");


                while (data != null && data.Read())
                {
                    box.Items.Add(data["full_name"]);
                }
            }
        }
        //заповнення ComboBox груп
        private static void SetComboBoxGroupe (ComboBox box)
        {
            box.Items.Clear();
            box.Items.Add("+Додати групу");
            box.Items.Add("Всі групи");
            foreach(var group in groups)
            {
                box.Items.Add(group.Value);
            }
           // box.Items.Add("+Додати нову");
        }
        //отримати список груп з бд
        private static void GetGroupsFromBD()
        {
            groups.Clear();
            var data = db.GetData($"SELECT * FROM Groups ORDER BY group_name");
            while (data != null && data.Read())
            {
                groups.Add(int.Parse(data["id_group"].ToString()), data["group_name"].ToString());
            }
        }
        //оновлення списків данних о уроках
        private static void GetLessonsFromBD(int day)
        {
            lessons.Clear();
            foreach (var group in groups)
            {
                Dictionary<int, string> lessonOfGroup = new Dictionary<int, string>();
                //отримати дані урока за групою та днем тижня
                var data = db.GetData($"SELECT lesson_number, id_lesson, subject_name FROM (Lessons INNER JOIN Subjects ON Lessons.id_subject = Subjects.id INNER JOIN Groups ON Groups.id_group = Lessons.id_group) WHERE Groups.group_name = '{group.Value}' and Lessons.day_of_week = {day} ORDER BY lesson_number");
                if (data != null)
                {
                    bool flag = false;
                    if(data.Read())
                    for (int i = 1; i < 7; i++)
                    {
                        if (flag) break;
                            //якщо урок існує записуємо його в массив
                            if (Convert.ToInt32(data["lesson_number"].ToString()) == i)
                            {
                                lessonOfGroup.Add(int.Parse(data["id_lesson"].ToString()), data["subject_name"].ToString());
                                if (!data.Read())
                                {
                                    flag = true;
                                }
                            }
                            //інакше зповнюємо массив пустим значенням
                            else
                            {
                                lessonOfGroup.Add(-999 - i, "");
                            }
                    }
                }
                lessons.Add(group.Key, lessonOfGroup);
            }
        }
        private static void UpdateDataGrid(DataGridView dw, ComboBox box)
        {
            int cur_col = dw.CurrentCell.ColumnIndex;
            int cur_row = dw.CurrentCell.RowIndex;
            GetLessonsFromBD(Convert.ToInt32(cur_day));
            SetDataGrid(box.Text);
            dw.CurrentCell = dw[cur_col, cur_row];
        }
        //заповнити TextBox кімнати
        private static void SetClassroom(MaskedTextBox box)
        {
            if (cur_day.Length > 0 && cur_group.Length > 0 && cur_lesson.Length > 0)
            {
                var classroom = db.GetData($"SELECT classroom FROM Lessons INNER JOIN Teachers ON Teachers.id_teacher = Lessons.id_teacher WHERE Lessons.day_of_week = {cur_day} AND Lessons.id_group = {cur_group} AND Lessons.lesson_number = {cur_lesson}");
                if (classroom.Read())
                    box.Text = classroom["classroom"].ToString();
                else
                    box.Clear();
            }
        }
        //при ззміні групи
        private void comboBoxGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetLessonsFromBD(comboBoxDay.SelectedIndex + 1);
            SetDataGrid(comboBoxGroup.Text);
            if(comboBoxGroup.SelectedIndex == 0)
            {
                Add_Group();
            }
            if(comboBoxGroup.SelectedIndex > 1)
            {
                DeleteGroup.Enabled = true;
            }
            else
            {
                DeleteGroup.Enabled = false;
            }
        }
        //при відкриванні comboBox вчителів
        private void comboBoxTeacher_DropDown(object sender, EventArgs e)
        {
            SetComboBoxTeacher(comboBoxTeacher, textBoxClassroom);
        }
        //при зміні предмета
        private void comboBoxSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            cur_subject = comboBoxSubject.SelectedItem?.ToString();
            comboBoxTeacher.SelectedIndex = -1;
            if (comboBoxSubject.SelectedIndex == 0)
                Add_Subject();
            if (comboBoxSubject.SelectedIndex > 0)
            {
                comboBoxTeacher.Enabled = true;
                DeleteSubject.Enabled = true;
            }
            else
            {
                DeleteSubject.Enabled = false;
            }
        }
        //зміна активності кнопки збереження уроку
        private static void ChangeEnableButtonSave(Button btn, DataGridView dataGridView, ComboBox comboBoxSubject, ComboBox comboBoxTeacher, MaskedTextBox textBoxLesson)
        {
            if (dataGridView.CurrentCell.ColumnIndex >= 1 && comboBoxSubject.SelectedIndex >= 0 && comboBoxTeacher.SelectedIndex >= 0 && textBoxLesson.Text.Length > 0)
                btn.Enabled = true;
            else
                btn.Enabled = false;
        }

        //зміна активності кнопки видалення уроку
        private static void ChangeEnableButtonDelete(Button btn)
        {
            if (is_update)
                btn.Enabled = true;
            else
                btn.Enabled = false;
        }
        //зберегання даних уроку
        private void buttonSave_Click(object sender, EventArgs e)
        {
            var id_cur_subject = db.GetInt($"SELECT id FROM Subjects WHERE subject_name = N'{comboBoxSubject.SelectedItem}'");
            var id_cur_teacher = db.GetInt($"SELECT id_teacher FROM Teachers WHERE full_name = N'{comboBoxTeacher.SelectedItem}'");
            //якщо кімната на цьому уроці не зайнята
            if (db.GetInt($"SELECT classroom FROM Lessons WHERE day_of_week = {cur_day} AND lesson_number = {cur_lesson} AND classroom = {textBoxClassroom.Text}") == -999 || (is_update && cur_classroom == Convert.ToInt32(textBoxClassroom.Text)))
            {
                //якщо урок вже існує
                if (is_update)
                {
                    //оновити дані
                    db.SaveData($"UPDATE Lessons SET id_teacher = {id_cur_teacher}, id_subject = {id_cur_subject}, day_of_week = {cur_day}, classroom = {textBoxClassroom.Text} WHERE id_group = {cur_group} AND lesson_number = {cur_lesson}  AND day_of_week = {cur_day}");
                }
                else
                    //створити нові дані
                    db.SaveData($"INSERT INTO Lessons(id_subject, id_teacher, id_group, classroom, day_of_week, lesson_number) VALUES({id_cur_subject}, {id_cur_teacher}, {cur_group}, {textBoxClassroom.Text}, {cur_day}, {cur_lesson});");
                UpdateDataGrid(dataGridView,comboBoxGroup);
                //оновити дані
                SetComboBoxSubject(comboBoxSubject);
                SetComboBoxTeacher(comboBoxTeacher, textBoxClassroom);
                SetClassroom(textBoxClassroom);
            }
            else
                MessageBox.Show("Вибрана кімната на цьому уроці зайнята, оберіть будь ласка іншу");
        }
        //Додати вчителя
        static void Add_Teacher()
        {
            var id_subject = db.GetInt($"SELECT id FROM Subjects WHERE subject_name = N'{form1.comboBoxSubject.Text}'");
            string res;
            var addForm = new formAddNew(form1, db, 0, id_subject.ToString());
            addForm.Show();
            form1.comboBoxTeacher.SelectedIndex = -1;
            form1.Enabled = false;
        }
        //Додати предмет
        static void Add_Subject()
        {
            string res;
            var addForm = new formAddNew(form1, db, 1);
            addForm.Show();
            form1.comboBoxSubject.SelectedIndex = -1;
            form1.Enabled = false;
        }
        //Додати предмет
        static void Add_Group()
        {
            string res;
            var addForm = new formAddNew(form1, db, 2);
            addForm.Show();
            form1.comboBoxGroup.SelectedIndex = 1;
            form1.Enabled = false;
        }
        //при зміні вчителя
        private void comboBoxTeacher_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeEnableButtonSave(buttonSave, dataGridView, comboBoxSubject, comboBoxTeacher, textBoxClassroom);
            if (comboBoxTeacher.SelectedIndex > 0)
            {
                DeleteTeacher.Enabled = true;
                textBoxClassroom.Enabled = true;
            }
            else
            {
                textBoxClassroom.Enabled = false;
                DeleteTeacher.Enabled = false;
            }
            if (comboBoxTeacher.SelectedIndex == 0)
                Add_Teacher();
        }
        //при зміні кімнати
        private void textBoxClassroom_TextChanged(object sender, EventArgs e)
        {
            ChangeEnableButtonSave(buttonSave, dataGridView, comboBoxSubject, comboBoxTeacher, textBoxClassroom);
        }
        //видалити дані
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            //дуже іронічна назва функції
            db.SaveData($"DELETE Lessons WHERE id_group = {cur_group} AND lesson_number = {cur_lesson} AND day_of_week = {cur_day}");
            UpdateDataGrid(dataGridView, comboBoxGroup);
            //оновити дані
            SetComboBoxSubject(comboBoxSubject);
            SetComboBoxTeacher(comboBoxTeacher, textBoxClassroom);
            SetClassroom(textBoxClassroom);
        }

        private void DeleteTeacher_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show($"Видалити вчителя {comboBoxTeacher.SelectedItem}","Видалення", MessageBoxButtons.OKCancel);
            if (res == DialogResult.OK)
            {
                int id = db.GetInt($"SELECT id_teacher From Teachers WHERE full_name = N'{comboBoxTeacher.SelectedItem}'");
                db.SaveData($"DELETE FROM Lessons WHERE id_teacher = {id}");
                db.SaveData($"DELETE FROM Teachers WHERE id_teacher = {id}");
                SetComboBoxTeacher(comboBoxTeacher, textBoxClassroom); 
                UpdateDataGrid(dataGridView, comboBoxGroup);
            }
        }

        private void DeleteSubject_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show($"Видалити предмет {comboBoxSubject.SelectedItem}", "Видалення", MessageBoxButtons.OKCancel);
            if (res == DialogResult.OK)
            {
                int id = db.GetInt($"SELECT id From Subjects WHERE subject_name = N'{comboBoxSubject.SelectedItem}'");
                db.SaveData($"DELETE FROM Lessons WHERE id_subject = {id}");
                db.SaveData($"DELETE FROM Teachers WHERE id_subject = {id}");
                db.SaveData($"DELETE FROM Subjects WHERE id = {id}");
                comboBoxSubject.SelectedIndex = -1;
                SetComboBoxSubject(comboBoxSubject);
                UpdateDataGrid(dataGridView, comboBoxGroup);
            }
        }

        private void DeleteGroup_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show($"Видалити групу {comboBoxGroup.SelectedItem}", "Видалення", MessageBoxButtons.OKCancel);
            if (res == DialogResult.OK)
            {
                int id = db.GetInt($"SELECT id_group From Groups WHERE group_name = N'{comboBoxGroup.SelectedItem}'");
                db.SaveData($"DELETE FROM Lessons WHERE id_group = {id}");
                db.SaveData($"DELETE FROM Groups WHERE id_group = {id}");
                UpdateGroups();
            }
        }
        public void UpdateGroups()
        {
            GetGroupsFromBD();
            SetComboBoxGroupe(comboBoxGroup);
            UpdateDataGrid(dataGridView, comboBoxGroup);
            comboBoxGroup.SelectedIndex = 1;
        }

        private void comboBoxSubject_DropDown(object sender, EventArgs e)
        {
            SetComboBoxSubject(comboBoxSubject);
        }

        //милиця(костыль) - отримати ключ директорії за значенням
        static int GetKeyFromValue (Dictionary<int,string> dict, string value)
        {
            foreach(var elem in dict)
            {
                if (elem.Value.CompareTo(value) == 0) return elem.Key;
            }
            return 0;
        }
        //при зміні дня тижня
        private void comboBoxDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (flag_start)
            {
                GetLessonsFromBD(comboBoxDay.SelectedIndex + 1);
                SetDataGrid(comboBoxGroup.Text);
                cur_day = (1 + comboBoxDay.SelectedIndex).ToString();
                try
                {
                    //оновити дані
                    comboBoxSubject.SelectedIndex = -1;
                    SetComboBoxSubject(comboBoxSubject);
                    SetComboBoxTeacher(comboBoxTeacher, textBoxClassroom);
                    if (comboBoxTeacher.SelectedIndex > 0)
                        SetClassroom(textBoxClassroom);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        //при натисканні DataGridView
        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (flag_start)
            {
                comboBoxSubject.SelectedIndex = -1;
                //записати обрані значення
                cur_lesson = (1 + dataGridView.CurrentCell.RowIndex).ToString();
                cur_group = dataGridView.CurrentCell.OwningColumn.Name;
                cur_subject = dataGridView.CurrentCell.Value?.ToString();
                //вивести дані уроку
                if (cur_lesson.Length > 0 && cur_group.Length > 0)
                {
                    SetComboBoxSubject(comboBoxSubject);
                    SetComboBoxTeacher(comboBoxTeacher, textBoxClassroom);
                    SetClassroom(textBoxClassroom);
                }
                //змінити активність кнопок
                ChangeEnableButtonDelete(buttonDelete);
                buttonSave.Enabled = false; //тому що немає сенсу змінювати вже записані дані
                if (dataGridView.CurrentCell.ColumnIndex <= 0)
                {
                    comboBoxSubject.Enabled = false;
                    comboBoxTeacher.Enabled = false;
                    textBoxClassroom.Enabled = false;
                    buttonSave.Enabled = false;
                    buttonDelete.Enabled = false;
                }
                else
                {
                    comboBoxSubject.Enabled = true;
                    ChangeEnableButtonDelete(buttonDelete);
                }
            }
        }
    }
}
