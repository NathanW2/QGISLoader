using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QGISLoader
{
    public class QGIS
    {
        public static Dictionary<string, string> GetVersions(string installpath)
        {
            string[] files = Directory.GetFiles(installpath, "qgis*.bat");
            var data = files.ToDictionary(file => Path.GetFileName(file), 
                                          file => file);
            return data;
        }
    }
}
