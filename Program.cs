using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace com.archit.das
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

        private static void logFileClearing(string path)
        {
            var oldLogFiles = new DirectoryInfo(@path).GetFiles().Where(s => s.Name.EndsWith(".log"))
                .OrderBy(f => f.LastWriteTime)
                .ToList();

            foreach (var logFile in oldLogFiles)
            {
                try
                {
                    File.Delete(logFile.Name);
                }
                catch (IOException e)
                {
                    //TODO: Figure out why this is happening but program is working as expected
                    Console.WriteLine("IO exception caught");
                }
            }
        }
        
        


        static void Main(string[] args)
        {
            String path = args[0]; //TODO: Make the code actually use that argument from the main program
            //string path = "C:\\Users\\archi\\OneDrive\\Music\\Geo Dash ringtones";
            string intendedName = Path.GetFileName(path);
            Directory.SetCurrentDirectory(path);

            //Sorts the files
            List<FileInfo> sortedFiles = sortFilesByDateCreated(path);

            //TODO: Make log file with a dictionary of changes
            Dictionary<string, string> logFile = new Dictionary<string, string>();
            int id = 1;
            foreach (var oldFile in sortedFiles)
            {
                logFile.Add(oldFile.Name, intendedName + " " + id + Path.GetExtension(oldFile.FullName));
                id++;
            }


            //Writing to logfile and creating dialog box information
            var ts = Stopwatch.GetTimestamp();
            DateTime dt = DateTime.Now;
            string formattedDate = dt.ToString("MM/dd/yy hh:mm:sss");
            StringBuilder sb = new StringBuilder(); //Dialog Box Content
            foreach (KeyValuePair<string, string> kvp in logFile)
            {
                sb.AppendFormat("{0} -> {1}{2}", kvp.Key, kvp.Value, Environment.NewLine);
            }
            
            
            string dialogBoxData = sb.ToString().TrimEnd();
            DialogResult res = MessageBox.Show(dialogBoxData, "Rename Files?",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res == DialogResult.OK)
            {
                
                try
                {
                    using (StreamWriter sw = File.AppendText( "Images.log"))
                    {
                        
                        //logFileClearing(path);
                        sw.WriteLine(formattedDate);
                        foreach (KeyValuePair<string, string> kvp in logFile)
                        {
                            sw.WriteLine("Original File Name = {0}, New File Name = {1}", kvp.Key, kvp.Value);
                            File.Move(kvp.Key, kvp.Value);
                        }
                    }

                    MessageBox.Show("Files have been sorted");
                    
                }
                catch (Exception e)
                {
                    MessageBox.Show("An exception has occured: \n" + e.Message);
                }
            }

            if (res == DialogResult.Cancel)
            {
                MessageBox.Show("Sorting has been cancelled");
            }
            
        }
    }
}