using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace DynamicReport
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SqlConnection con;
        private string SelectedTable = string.Empty;
        private List<string> QueryColumns = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
            con = new SqlConnection();
            con.ConnectionString =
                ConfigurationManager
                .ConnectionStrings["defConnection"]
                .ConnectionString;

            con.Open();

            DataTable tables2 =
                con.GetSchema("Tables");



            Console.WriteLine("The tables are:");
            foreach (DataRow row in tables2.Rows)
            {
                Label label = new Label();
                label.MouseDown += Label_MouseDown;
                label.Content = row.ItemArray[2];
                wrap1.Children.Add(label);
            }
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DataTable tables2 =
           con.GetSchema("Tables");
            Label label = (Label)sender;
            SelectedTable = label.Content.ToString();

            QueryColumns.Clear();

            DataTable test = con.GetSchema("Columns",
                new[] { "CRCMS_new", null, label.Content.ToString() });

            foreach (DataRow row in test.Rows)
            {
                ListBoxItem itm = new ListBoxItem();
                string name =
                    row.ItemArray[3].ToString();
                itm.Content = name;
                tableRow.Items.Add(itm);
            }

            //foreach (DataRow row in tables2.Rows)
            //{
            //    if (row.ItemArray[2].ToString().Equals(label.Content.ToString()))
            //    {
            //        ListBoxItem itm = new ListBoxItem();
            //        itm.Content = row["COLUMN_NAME"].ToString();
            //        tableRow.Items.Add(itm);
            //        break;
            //    }
            //}


        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ListBoxItem item = (ListBoxItem)tableRow.SelectedItem;
            string columnName = item.Content.ToString();
            QueryColumns.Add(columnName);
            string query = "SELECT ";
            foreach (var column in QueryColumns)
            {
                query += column + ",";
            }
            query = query.Substring(0, query.LastIndexOf(','));
            query += string.Format(" FROM {0}", SelectedTable);
            rtbx.Text = query;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = rtbx.Text;
                 PrivateReport report = new PrivateReport(cmd,QueryColumns);
            report.Show();
        }
    }
}
