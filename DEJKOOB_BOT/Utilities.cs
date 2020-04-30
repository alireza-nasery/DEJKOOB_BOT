using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEJKOOB_BOT
{
    static public class Utilities
    {




        static public string ReadFromeFile(string path)
        {
            try
            {
                string data = string.Empty;
                using (StreamReader reader = new StreamReader(path))
                {
                    data = reader.ReadToEnd();
                }
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }







        static public string WriteInTxtFile(string path, string Line)
        {
            using (var writer = new StreamWriter(path, false))
            {
                writer.WriteLine(Line);
            }
            return Line;
        }
    }
}
