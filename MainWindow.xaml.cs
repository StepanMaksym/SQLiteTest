
using System;
using System.Data.SQLite;
using System.Text;
using System.Windows;


namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //SQLiteConnection.CreateFile("MyDatabase.sqlite");
        //SQLiteCommand command = new SQLiteCommand(SqlCreateTableCustomer, conn);
        //command.ExecuteNonQuery();
        private const string SqlCreateTableCustomer ="create table Customer (Name varchar(20), SecondName varchar(20), Year varchar(20))";
        private string UpdateCustomerTable ="insert into Customer (Name, SecondName, Year) values ('{0}', '{1}', '{2}')";
        private string DeleteAll = "Delete From Customer";
        SQLiteConnection conn = new SQLiteConnection("Data Source = MyDatabase.sqlite");

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            conn.Open();
            if (string.IsNullOrWhiteSpace(tbName.Text) || string.IsNullOrWhiteSpace(tbSecondName.Text) || string.IsNullOrWhiteSpace(tbYear.Text)) { MessageBox.Show("Enter all values"); }
            else
            {
                SQLiteCommand updateCommand = new SQLiteCommand(string.Format(UpdateCustomerTable, tbName.Text, tbSecondName.Text, tbYear.Text), conn);
                updateCommand.ExecuteNonQuery();
                tbName.Text = tbSecondName.Text = tbYear.Text = "";
            }
            conn.Close();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand("Delete From Customer Where (Name = '" + tbName.Text + "' and SecondName = '" + tbSecondName.Text + "' and Year = '" + tbYear.Text + "' )", conn);
            cmd.ExecuteNonQuery();
            tbName.Text = tbSecondName.Text = tbYear.Text = "";
            conn.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SecondWindow secondWindow = new SecondWindow();
            secondWindow.Show();
        }

        private void btnDELETEALL_Click(object sender, RoutedEventArgs e)
        {
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(DeleteAll, conn);
            cmd.ExecuteNonQuery();
            tbLog.Text = "";
            conn.Close();
        }
        
        private string readFromCustomerDbTable(SQLiteConnection connection)
        {
            SQLiteCommand selectCommand = new SQLiteCommand("select * from Customer", connection);
            SQLiteDataReader reader = selectCommand.ExecuteReader();
            StringBuilder sb = new StringBuilder();
            while (reader.Read())
            {
                sb.Append("Name: " + reader["Name"] + "Second Name: " + reader["SecondName"] + "Year" + reader["Year"]);
            }
            return sb.ToString();
        }
    }
}
