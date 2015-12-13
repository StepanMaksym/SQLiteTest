using System;
using System.Data.SQLite;
using System.Text;
using System.Windows;

namespace WpfApplication1
{
    public partial class SecondWindow : Window
    {
        SQLiteConnection conn = new SQLiteConnection("Data Source = MyDatabase.sqlite");
        
        public SecondWindow()
        {
            InitializeComponent();
            tbSec.Text = readFromCustomerDbTable(conn);
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Hide();
           
        }

        private string readFromCustomerDbTable(SQLiteConnection connection)
        {
            conn.Open();
            SQLiteCommand selectCommand = new SQLiteCommand("select * from Customer", connection);
            SQLiteDataReader reader = selectCommand.ExecuteReader();
            StringBuilder sb = new StringBuilder();
            while (reader.Read())
            {
                sb.Append(" Name: " + reader["Name"] + " Second Name: " + reader["SecondName"] + " Year " + reader["Year"]);
            }
            conn.Close();
            return sb.ToString();
            }
    }
}
