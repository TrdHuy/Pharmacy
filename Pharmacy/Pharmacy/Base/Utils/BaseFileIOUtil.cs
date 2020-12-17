using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pharmacy.Base.Utils
{
    public class BaseFileIOUtil
    {
        public static OpenFileDialog OpenFile(string filter, string folder, string title)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.DefaultExt = ".txt";
            openFileDialog.Filter = filter;

            if (!String.IsNullOrEmpty(folder))
            {
                var directory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\" + folder;
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                openFileDialog.InitialDirectory = directory;
            }

            openFileDialog.Title = !String.IsNullOrEmpty(title) ? title : "Choose your file";

            return openFileDialog;
        }

        public static SaveFileDialog SaveFile(string filter, string folder)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.DefaultExt = ".txt";
            saveFileDialog.Filter = filter;

            var directory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\" + folder;
            if (!Directory.Exists(directory) && !String.IsNullOrEmpty(folder))
            {
                Directory.CreateDirectory(directory);
            }
            saveFileDialog.InitialDirectory = directory;
            saveFileDialog.Title = "Save an log file";

            return saveFileDialog;
        }
    }
}
