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
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace DynamicReport
{
    /// <summary>
    /// Interaction logic for PrivateReport.xaml
    /// </summary>
    public partial class PrivateReport : Window
    {
        public PrivateReport(SqlCommand cmd, List<string> columns)
        {
            InitializeComponent();
            //var reader = cmd.ExecuteReader();
            //foreach (var item in columns)
            //{  
            //    mainGrid
            //        .Columns
            //        .Add(new DataGridTextColumn() {Header = item});
            //}
            //while (reader.Read())
            //{
            //        mainGrid.Items.Add()
            //}
            //DataGridColumn column = new DataGridTextColumn() {Header = item};
                    
        }
    }
}
