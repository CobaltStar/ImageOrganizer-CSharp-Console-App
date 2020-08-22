using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Channels;

namespace Image_Organizer__C_Sharp_Port_
{
    class Program
    {
        private static List<FileInfo> sortFilesByDateCreated(string path)
        {
            return new DirectoryInfo(@path).GetFiles()
                .OrderBy(f => f.LastWriteTime)
                .ToList();
        }
        
        
        static void Main(string[] args)
        {
            
            //String path = args[0]; //TODO: Make the code actually use that argument from the main program
            string path = "C:\\Users\\archi\\OneDrive\\Music\\Geo Dash ringtones";
            string intendedName = Path.GetFileName(path);


            List<FileInfo> sortedFiles = sortFilesByDateCreated(path);
            sortedFiles.ForEach(Console.WriteLine);

            int id = 1;
            Directory.SetCurrentDirectory(path);
            foreach (var file in sortedFiles)
            {
                File.Move(file.Name,   intendedName + " " + id + Path.GetExtension(file.FullName));
                id++;
            }
            
            /*DialogResult res = MessageBox.Show("Are you sure you want to Delete", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);  
            if (res == DialogResult.OK) {  
                MessageBox.Show("You have clicked Ok Button");  
                //Some task…  
            }  
            if (res == DialogResult.Cancel) {  
                MessageBox.Show("You have clicked Cancel Button");  
                //Some task…  
            }   */

        }
    }

    internal class DialogResult
    {
    }
}
