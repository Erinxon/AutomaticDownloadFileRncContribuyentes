using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Text;

namespace DownloadFile
{
    public class DecompressFile
    {
        public string Path { get; set; }
        public string directorioDestindo { get; set; }

        public  void Extract()
        {
            try
            {
                ZipFile.ExtractToDirectory(Path, directorioDestindo, true);
                Console.WriteLine("Archivo descomprimido correctamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error: " + ex.Message + "\nPrecione enter para volver a intentarlo");
                Console.ReadKey();
                Console.Clear();
                Extract();
            }
           
        }
    }
}
