using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_02
{
    class Program
    {
        static void Main(string[] args)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["OleDbConnection"].ConnectionString;
            OleDbConnection con = new OleDbConnection();
            con.ConnectionString = connectionString;
            con.Open();

            var schemaTable = 
                con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, 
                new Object[] { null, null, null, "newEquipment" });


            schemaTable = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables,
              new Object[] { null, null, "newEquipment", "TABLE" });

            for (int i = 0; i < schemaTable.Columns.Count; i++)
            {
                Console.WriteLine(schemaTable.Columns[i].ToString() + " : " +
                                  schemaTable.Rows[0][i].ToString());
            }



            for (int i = 0; i < schemaTable.Rows.Count; i++)
            {
                Console.WriteLine(schemaTable.Rows[i].ItemArray[2].ToString());
            }


            for (int i = 0; i < schemaTable.Columns.Count; i++)
            {
                Console.WriteLine(schemaTable.Columns[i].ToString());
            }


            for (int i = 0; i < schemaTable.Rows.Count; i++)
            {
                Console.WriteLine(schemaTable.Rows[i].ItemArray[2].ToString());
            }


            Console.WriteLine("--------------------");

            DataTable tables2 = 
                con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, 
                new object[] { null, null, null, "TABLE" });

            Console.WriteLine("The tables are:");
            foreach (DataRow row in tables2.Rows)
                Console.Write("  {0}", row[2]);


            object[] objArrRestrict;
            objArrRestrict = new object[] { null, null, "newEquipment", null };
            DataTable schemaCols;
            schemaCols = con.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, 
                objArrRestrict);
            
            //List the schema info for the selected table
            foreach (DataRow row in schemaCols.Rows)
            {
                Console.WriteLine(row["COLUMN_NAME"]);
            }

            DataTable tables = con.GetSchema("Tables");


        
            for (int i = 0; i < schemaTable.Rows.Count; i++)
            {
                Console.WriteLine(schemaTable.Rows[i].ItemArray[3].ToString());
            }

            DataTable dtCols = con.GetSchema("Columns");
            //Чтобы получить информацию обо всех столбцах всех таблиц вашей БД
            var shema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            //Если требуется получить столбцы конкретной таблицы
            object[] r = { "newEquipment" };
            var shemaWithRestrictions = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            //var t = shemaWithRestrictions.Get
            //просматривает записи таблицы со списком полей
            DataTable shemaWithRestrictions2 = con.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, null, "newEquipment"});
            foreach (var item in shemaWithRestrictions2.Rows)
            {
                Console.WriteLine(item);
            }



            string connectionStrng = connectionString;

          

            //Или же инициализировать объект OleDbConnection с помощью конструктора класса. 
            //OleDbConnection conConstructor = new OleDbConnection(connectionStrng);

            //Пулсоеденений
            for (int i = 1; i <= 5; i++)
            {
                con.Open();

                System.Threading.Thread.Sleep(0);

                con.Close();
                //Что делать, если не надо помещать соединения в пул
                //OleDbConnection.ReleaseObjectPool();
            }

            //Создание объектов Command 
            OleDbCommand command = new OleDbCommand();
            command.Connection = con;
            //ИЛИ
            //В некоторых случаях, когда мне нужно создать объект Command однократного использования, 
            //я применяю метод CreateCommand.
            OleDbCommand command2 = con.CreateCommand();

            command.CommandText = "CREATE TABLE MyTable ...";
            command.ExecuteNonQuery();
            command.Dispose();//необязательный вызов

            //лучше испоьзовать так
            using (OleDbCommand cmd = new OleDbCommand())
            {
                command.CommandText = "CREATE TABLE MyTable ...";
                command.ExecuteNonQuery();
            }


            //----------------
            
           


            SqlConnectionStringBuilder connect = new SqlConnectionStringBuilder();
            connect.InitialCatalog = "MCS";
            connect.DataSource = @"DESKTOP-C90AOHQ";
            connect.UserID = "sa";
            connect.Password = "Mc123456";

            //connect.ConnectTimeout = 30;
            //connect.IntegratedSecurity = true;
            // Создание открытого подключения

            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = connect.ConnectionString;
                cn.Open();
                cn.ChangeDatabase("CRCMS_new");
                // Работа с базой данных
                cn.Close();

                #region  Эффективное использование соединений
                //try
                //{
                //    //Открыть подключение
                //    cn.Open();
                //}
                //catch (SqlException ex)
                //{
                //    // Протоколировать исключение
                //    Console.WriteLine(ex.Message);
                //}
                //finally
                //{
                //    // Гарантировать освобождение подключения
                //    cn.Close();
                //}
                #endregion
            }
        }
    }
}
