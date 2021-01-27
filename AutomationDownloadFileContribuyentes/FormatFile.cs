using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DownloadFile
{
    public class FormatFile
    {
        public string Path { get; set; }
        public string saveLocation { get; set; }

        public void OpenFile()
        {
            try
            {        
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                string line;
                StreamReader file = new StreamReader(this.Path, Encoding.GetEncoding(1252));
                List<string> list = new List<string>();
                while ((line = file.ReadLine()) != null)
                {
                    line = line.Replace("| | | | |", "|");
                    line = line.Replace("| |", "|Null|");
                    line = line.Replace("||", "|Null|");
                    line = line.Replace("/", "-");
                    list.Add(line);
                }
                Format(list);
                file.Close();
                Console.WriteLine("Archivo formateado correctamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error: " + ex.Message + "\nPrecione enter para volver a intentarlo");
                Console.ReadKey();
                Console.Clear();
                OpenFile();
            }
        }

        private void Format(List<string> line)
        {
            File.WriteAllLines(this.saveLocation, line, Encoding.GetEncoding(1252));
        }
    }
}
