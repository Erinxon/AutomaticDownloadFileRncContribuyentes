using DownloadFile;
using DownloadFile.DB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace AutomationDownloadFileContribuyentes
{
    class Program
    {

        static void Main(string[] args)
        {
            start();
        }

        static void start()
        {
            Console.WriteLine("Precione enter para empezar..");
            Console.ReadKey();
            Console.WriteLine("Descargando archivo...");           
            Descargar();
            Console.WriteLine("\nDescomprimiendo archivo...");
            Descromprimir();
            Thread.Sleep(1000);
            Console.WriteLine("\nFormateando archivo...");
            FormatearTxt();
            Thread.Sleep(1000);
            Console.WriteLine("\nCreando la base de datos.....");
            inputDataDb();
            Thread.Sleep(1000);
            Console.WriteLine("\nBase de datos actualizada con éxito.\nPrecione enter para cerrar el programa.");
            Console.ReadKey();
            Environment.Exit(1);
        }

        static void inputDataDb()
        {
            Console.WriteLine("Escriba el nombre del servidor de la base de datos");
            string server  = Console.ReadLine();
            Console.WriteLine("Escriba el nombre de la base de datos");
            string database = Console.ReadLine();

            if (!server.Equals("") && !database.Equals(""))
            {
                CreateDb(server, database);
            }
            else
            {
                Console.WriteLine("Error: no puede dejar ningun campo vacio\n precione enter para rententarlo");
                Console.ReadKey();
                Console.Clear();
                inputDataDb();
            }
        }

        static void CreateDb(string serverInput, string databaseInput)
        {
            try
            {
                CreateDatabse createDatabse = new CreateDatabse();
                createDatabse.server = serverInput;
                createDatabse.database = databaseInput;
                createDatabse.Path = @"C:\TMP\RNC.TXT";
                createDatabse.Connection();
                if (createDatabse.isConnection())
                {
                    createDatabse.Create();
                    createDatabse.BulkInsert();
                }
                else
                {
                    Console.WriteLine("Error al conectarse a la base de datos");
                    inputDataDb();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error: " + ex.Message + "\nAsegurece de proporcionar los datos correctos\nPrecione enter para volver a intentarlo");
                Console.ReadKey();
                Console.Clear();
                inputDataDb();
            }

        }

        static void Descargar()
        {
            try
            {
                Download download = new Download
                {
                    address = "https://dgii.gov.do/app/webApps/Consultas/RNC/DGII_RNC.zip",
                    saveLocation = @"C:\TMP\DGII_RNC.zip"
                };

                download.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error: " + ex.Message + "\nPrecione enter para volver a intentarlo");
                Console.ReadKey();
                Console.Clear();
                Descargar();
            }
           
        }

        static void Descromprimir()
        {
            try
            {
                DecompressFile decompressFile = new DecompressFile
                {
                    Path = @"C:\TMP\DGII_RNC.zip",
                    directorioDestindo = @"C:\TMP\TMP"
                };
                decompressFile.Extract();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error: " + ex.Message + "\nAsegurece de proporcionar los datos correctos\nPrecione enter para volver a intentarlo");
                Console.ReadKey();
                Console.Clear();
                Descromprimir();
            }


        }

        static void FormatearTxt()
        {
            FormatFile formatFile = new FormatFile
            {
                Path = @"C:\TMP\TMP\DGII_RNC.TXT",
                saveLocation = @"C:\TMP\RNC.TXT"
            };
            formatFile.OpenFile();
        }
    }
}
