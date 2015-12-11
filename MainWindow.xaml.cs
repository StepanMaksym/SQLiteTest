
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
        private const string SqlCreateTableCustomer =
                "create table Customer (Name varchar(20), SecondName varchar(20), Year varchar(20))";

        private string UpdateCustomerTable ="insert into Customer (Name, SecondName, Year) values ('{0}', '{1}', '{2}')";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

            //SQLiteConnection.CreateFile("MyDatabase.sqlite");
            SQLiteConnection conn = new SQLiteConnection("Data Source = MyDatabase.sqlite");
            conn.Open();
            //SQLiteCommand command = new SQLiteCommand(SqlCreateTableCustomer, conn);
            //command.ExecuteNonQuery();

            string Name = tbName.Text;
            string SecondName = tbSecondName.Text;
            string Year = tbYear.Text;

            SQLiteCommand updateCommand = new SQLiteCommand(string.Format(UpdateCustomerTable, Name, SecondName, Year), conn);
            updateCommand.ExecuteNonQuery();

            tbLog.Text = "Updated!\n";
            tbLog.Text = readFromCustomerDbTable(conn);

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

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            string DeleteFromCustonaerTable = @"delete from Customer";
            SQLiteConnection connDel = new SQLiteConnection("Data Source = MyDatabase.sqlite");
            connDel.Open();

            SQLiteCommand deleteCommand = new SQLiteCommand(DeleteFromCustonaerTable, connDel);
            deleteCommand.ExecuteNonQuery();

            connDel.Close();
            //tbLog.Text = "Deleted";
        }

        
    }
}
