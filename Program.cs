using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Image_Organizer__C_Sharp_Port_
{
    class Program
    {
        //sorts files - also ignores the log file in sorting
        private static List<FileInfo> sortFilesByDateCreated(string path)
        {
            return new DirectoryInfo(@path).GetFiles().Where(s => !s.Name.EndsWith(".log"))
                .OrderBy(f => f.LastWriteTime)
                .ToList();
        }
        
        
        static void Main(string[] args)
        {
            
            //String path = args[0]; //TODO: Make the code actually use that argument from the main program
            string path = "C:\\Users\\archi\\OneDrive\\Music\\Geo Dash ringtones";
            string intendedName = Path.GetFileName(path);
            Directory.SetCurrentDirectory(path);

            //Sorts the files
            List<FileInfo> sortedFiles = sortFilesByDateCreated(path);
            sortedFiles.ForEach(Console.WriteLine);

            //TODO: Make log file with a dictionary of changes
            Dictionary<string, string> logFile = new Dictionary<string, string>();
            int id = 1;
            foreach (var oldFile in sortedFiles)
            {
                logFile.Add(oldFile.Name, intendedName +" " + id + Path.GetExtension(oldFile.FullName));
                id++;
            }
            
            
            //Writing to logfile
            var ts = Stopwatch.GetTimestamp();
            DateTime dt = DateTime.Now;
            string formattedDate = dt.ToString("hh.mm.sss MM-dd-yy");
            using (StreamWriter sw = new StreamWriter( formattedDate + ".log"))
            {
                foreach (KeyValuePair<string, string> kvp in logFile)
                {
                    sw.WriteLine("Original File Name = {0}, New File Name = {1}", kvp.Key, kvp.Value);
                }
            }
            
            
            /*foreach (KeyValuePair<string, string> kvp in logFile)
            {
                //textBox3.Text += ("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            }*/
            
                

            /*int id = 1;
            Directory.SetCurrentDirectory(path);
            foreach (var file in sortedFiles)
            {
                File.Move(file.Name,   intendedName + " " + id + Path.GetExtension(file.FullName));
                id++;
            }*/
            
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
    
}