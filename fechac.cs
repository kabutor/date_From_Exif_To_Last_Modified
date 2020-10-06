
using System;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.Win32;
using System.IO;
using System.Collections.Generic;


public class Program
{
    public static void Main(string[] args)
    {

        List<string> valores = new List<string>();
        
        if (args.Length < 1)
        {
            Console.WriteLine("Ejecutar con el nombre del directorio");
        } else if (args.Length < 2)
        {

            string[] filePaths = Directory.GetFiles(args[0]);

            foreach (string filepath in filePaths)
            {
                string temp = filepath.Substring(filepath.Length - 3);
                if (temp.ToLower() == "jpg")
                {
                    valores.Add(filepath);
                }

            }

        }
        foreach (string valor in valores) {
            Console.WriteLine("Fichero: " + valor);
            //llama funcion cambia valor con cada fichero
            Program.cambia_valor(valor);
        }
    }

    //FUNCION QUE CAMBIA EL VALOR DEL FICHERO QUE SE LE PASA
        public static void cambia_valor(string fichero)
        {
            bool DEBUG = false;
            Image image = new Bitmap(fichero);

            // Get the PropertyItems property from image.
            PropertyItem[] propItems = image.PropertyItems;

            // For each PropertyItem in the array, display the ID, type, and
            // length.
            int count = 0;
            foreach (PropertyItem propItem in propItems)
            {
                //132 Datetime
                //9004 Date Adquisition
                if (propItem.Id.ToString("x") == "9004")
                {
                    //Console.WriteLine("Property Item " + count.ToString() + " id: 0x" + propItem.Id.ToString("x") + " type: " + propItem.Type.ToString() + " length: " + propItem.Len.ToString() + " bytes");
                    System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                    string fecha = encoding.GetString(propItem.Value);
                //reemplaza los primeros caracteres para coger la fecha
                Console.WriteLine("--------------------------------");
                    Console.WriteLine(fecha);
                    fecha = (fecha.Substring(0, 10).Replace(":", "/")) + fecha.Substring(10, 10);
                    if (DEBUG) { Console.WriteLine("Path " + fichero); }
                    if (DEBUG) { Console.WriteLine("Exif " + fecha); }
                    DateTime nuevafecha = DateTime.Parse(fecha);

                    if (DEBUG) { Console.WriteLine("Nueva " + nuevafecha); }
                    image.Dispose();

                    File.SetLastWriteTime(fichero, nuevafecha);
                    break;
                }

                count++;
            }
        }
}

