using System;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;

namespace QGISLoader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.InitJumpList();
        }

        public void InitJumpList()
        {
            var jlist = new JumpList();
            string installpath = @"C:\OSGeo4W\bin";

            var nicenames = new Dictionary<string, string>
            {
                {"qgis.bat", "QGIS 2.16"},
                {"qgis-ltr.bat", "QGIS 2.14 - LTR"},
                {"qgis-dev-g7.0.4.bat", "QGIS Dev"},
            };

            foreach (var version in QGIS.GetVersions(installpath))
            {
                string cat = "Desktop";
                string icon = System.IO.Path.Combine(installpath, "qgis-bin.exe");
                string name = version.Key;
                if (name.ToLower().Contains("browser"))
                {
                    var found = jlist.JumpItems.Any(item => item.CustomCategory == "Browser");
                    if (found)
                        continue;
                    cat = "Browser";
                    icon = System.IO.Path.Combine(installpath, "qgis-browser-bin.exe");
                }
                else if (name.ToLower().Contains("designer"))
                {
                    var found = jlist.JumpItems.Any(item => item.CustomCategory == "Tools");
                    if (found)
                        continue;
                    cat = "Tools";
                    icon = System.IO.Path.Combine(installpath, "designer.exe");
                }
                else if (nicenames.ContainsKey(name.ToLower()))
                {
                    name = nicenames[name.ToLower()];
                }
                else
                {
                    continue;
                }
                jlist.JumpItems.Add(MakeTask(version.Value, name, cat, icon));
            }
            string projecticon = System.IO.Path.Combine(installpath, "qgis-bin.exe");
            jlist.JumpItems.Add(MakeTask(@"F:\gis_data\QGIS Projects\Perth.qgs", "Perth.qgs" , "Projects", projecticon));
            jlist.JumpItems.Add(MakeTask(@"F:\gis_data\QGIS Projects\Perth.qgs", "Sewer.qgs" , "Projects", projecticon));
            jlist.JumpItems.Add(MakeTask(@"F:\gis_data\QGIS Projects\Perth.qgs", "Training.qgs" , "Projects", projecticon));
            jlist.ShowRecentCategory = true;
            jlist.Apply();
        }

        public static JumpTask MakeTask(string path, string name, string cat, string icon)
        {
            var task = new JumpTask();
            task.Arguments = "--noplugins";
            task.IconResourcePath = icon;
            task.ApplicationPath = path;
            task.Title = name;
            task.CustomCategory = cat;
            return task;
        }
    }
}
