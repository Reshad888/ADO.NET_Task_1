using Microsoft.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ADO.NET_Task_1
{
    public partial class MainWindow : Window
    {
        // Connection String
        string connectionString =
        "Data Source = DESKTOP-RH41O1K\\SQLEXPRESS;" +
        "Initial Catalog = Library;" +
        "Integrated Security = True;" +
        "Connect Timeout = 30;" +
        "Encrypt = True;" +
        "Trust Server Certificate = True;" +
        "Application Intent = ReadWrite;" +
        "Multi Subnet Failover = False";


        public MainWindow()
        {
            InitializeComponent();

            // SQL Connection
            SqlConnection? sqlConnection = new SqlConnection(connectionString);


            try
            {
                sqlConnection.Open();

                // Select Querry
                string SelectQuerry = "SELECT Name FROM Categories";
                SqlCommand sqlCommand = new SqlCommand(SelectQuerry, sqlConnection);

                // SQL Data Reader
                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    CategoriesComboBox.Items.Add(reader["Name"].ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }


        private void CategoriesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // SQL Connection
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            try
            {
                sqlConnection.Open();

                // Select Querry
                string selectQuerry = $"SELECT Books.[Name] FROM Books INNER JOIN Categories ON Books.Id_Category = Categories.Id WHERE Categories.[Name] = '{CategoriesComboBox.Text}'";
                SqlCommand sqlCommand = new SqlCommand(selectQuerry, sqlConnection);

                SqlDataReader reader = sqlCommand.ExecuteReader();

                BookComboBox.Items.Clear();

                while (reader.Read())
                {
                    BookComboBox.Items.Add(reader["Name"].ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void QueryButton_Click(object sender, RoutedEventArgs e)
        {
            // SQL Connection
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            try
            {
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand(QueryTextBox.Text, sqlConnection);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}