using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SchoolTimetebale
{

    //підключення до бд
    public class ConnectDB
    {
        SqlConnection conn = null;
        public ConnectDB(string connString)
        {
            conn = new SqlConnection(connString);
        }
        public SqlDataReader GetData(string command)
        {
            Console.WriteLine("GetData " + command);
            try
            {
                conn.Close();
                conn.Open();
                var output = new SqlCommand(command, conn).ExecuteReader();
                return output;
            }
            catch (Exception ex)
            {
                MessageBox.Show("GetData " + ex.Message);
            }
            return null;
        }
        public string GetString(string command)
        {
            Console.WriteLine("GetString " + command);
            try
            {
                conn.Close();
                conn.Open();
                var output = new SqlCommand(command, conn).ExecuteReader();
                if (output.Read())
                    return output.GetString(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("GetString " + ex.Message);
            }
            return null;
        }
        public int GetInt(string command)
        {
            Console.WriteLine("GetInt " + command);
            try
            {
                conn.Close();
                conn.Open();
                var output = new SqlCommand(command, conn).ExecuteReader();
                if (output.Read())
                    return output.GetInt32(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("GetInt " + ex.Message);
            }
            return -999;
        }
        public void SaveData(string command)
        {
            try
            {
                conn.Close();
                conn.Open();
                new SqlCommand(command, conn).ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("SaveData " + ex.Message);
            }
            Console.WriteLine("SaveData " + command);
        }
    }
}