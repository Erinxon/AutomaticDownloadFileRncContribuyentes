using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DownloadFile.DB
{
    public  class CreateDatabse
    {
        private SqlConnection connection;
        public string server { get; set; }
        public string database { get; set; }
        public string Path { get; set; }

        public void Connection()
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
                {
                    ConnectionString = string.Format("Server={0};Database={1};Trusted_Connection=True;",this.server,this.database)
                };
                connection = new SqlConnection(builder.ConnectionString);             
            }
            catch (SqlException e)
            {
                Console.WriteLine("Ocurrio un error: " + e.Message + "\nPrecione enter para volver a intentarlo");
                Console.ReadKey();
                Console.Clear();
                Connection();
            }
        }

        public bool isConnection()
        {
            try
            {
                connection.Open();
                connection.Close();
                return true;
            }
            catch (SqlException e)
            {
                return false; 
            }
        }

        public void Create()
        {
            try
            {
                String sqlCreateTable = @"create table Contribuyentes(
                                            RNC varchar(50) not null primary key,
                                            RazonSocial varchar(250) null,
	                                        NombreComercial varchar(250) null,
	                                        ActividadEconomica varchar(250) null,
	                                        Fecha varchar(50) null,
	                                        Estatus varchar(50) null,
	                                        RegimenDePagos varchar(250) null,
                                           )";

                SqlCommand command = new SqlCommand(sqlCreateTable, connection);
                command.Connection.Open();          
                command.ExecuteReader();
                command.Connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine("Ocurrio un error: " + e.Message + "\nPrecione enter para volver a intentarlo");
                Console.ReadKey();
                Console.Clear();
                Create();
            }

        } 

        public void BulkInsert()
        {
            try
            {
                String sqlBULKINSERT = string.Format(@"BULK INSERT 
                                                Contribuyentes
                                            FROM
                                                '{0}'
                                            with(
                                                FIELDTERMINATOR = '|',
                                                ROWTERMINATOR = '\n',
                                                FIRSTROW = 1
                                            )", this.Path);

                SqlCommand command = new SqlCommand(sqlBULKINSERT, connection);
                command.Connection.Open();
                command.ExecuteReader();
                command.Connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine("Ocurrio un error: " + e.Message + "\nPrecione enter para volver a intentarlo");
                Console.ReadKey();
                Console.Clear();
                BulkInsert();
            }
           
        }

        public void DeleteTable()
        {
            try
            {
                String sqlDeteleTable = string.Format("drop table Contribuyentes");
                SqlCommand command = new SqlCommand(sqlDeteleTable,this.connection);
                command.Connection.Open();
                command.ExecuteReader();
                command.Connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void DeleteData()
        {
            try
            {
                String sqlDeteleTable = string.Format("delete Contribuyentes");
                SqlCommand command = new SqlCommand(sqlDeteleTable, this.connection);
                command.Connection.Open();
                command.ExecuteReader();
                command.Connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
