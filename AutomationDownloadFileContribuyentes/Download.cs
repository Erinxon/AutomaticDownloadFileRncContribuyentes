using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Threading.Tasks;

namespace DownloadFile
{
    public class Download
    {
        public string saveLocation { get; set; }
        public string address { get; set; }

        public void Start()
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFileCompleted += (sender, e) => Console.WriteLine("Archivo descargado correctamente");
                webClient.DownloadProgressChanged += (sender, e) =>
                {                   
                    Console.WriteLine("Progreso: " + (e.BytesReceived / 1048576).ToString() + " MB" + " de " + (e.TotalBytesToReceive / 1048576).ToString() + " MB");
                };
                webClient.DownloadFileTaskAsync(new Uri(address), saveLocation).Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error: " + ex.Message + "\nPrecione enter para volver a intentarlo");
                Console.ReadKey();
                Console.Clear();
                Start();
            }    
        }

    }
}
