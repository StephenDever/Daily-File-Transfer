using System;
using System.IO;
using System.Collections;

namespace Daily_File_Transfer
{
    class Program
    {
        // We need the current date/time to compare against the file's last write time.
        public static DateTime localDate = DateTime.Now;
        // We want to check if a files has been modified in last 24 hours. Use this object to check.
        public static DateTime localDateMinusOneDay = localDate.AddDays(-1);
        // Here we put where the files will be sent to.
        private static string destinationFolder = @"C:\Users\Stephen\Desktop\B";

        static void Main(string[] args)
        {
            // Pass the directory we want to scan into directoryToScan method, defined below.
            directoryToScan(@"C:\Users\Stephen\Desktop\A");
            Console.WriteLine("Press Enter to continue");
            Console.ReadLine();
        }

        // This is the method that will scan the working directory for recently modified files
        // and copy the files to destinationFolder
        static void directoryToScan(string workingDirectory)
        {
            // This is an array that lists each file with file path.
            string[] filePaths = Directory.GetFiles(workingDirectory);

            // for each file, we want to check when it was last written to, 
            // and if written to in the last 24 hours, copy it to the destinationFolder.
            foreach (string filePath in filePaths)
            {
                Console.WriteLine(filePath);

                // fetches the date/time the files was last written to.
                DateTime writeTime = File.GetLastWriteTime(filePath);

                // If the file was written to in the last 24 hours, it copies to destinationFolder.
                if (writeTime >= localDateMinusOneDay)
                {
                    Console.WriteLine("File WILL be copied to {0}.", destinationFolder);
                    // This returns the file name without the directory part.
                    string fileName = Path.GetFileName(filePath);
                    // This appends the file name to the destinationFolder directory.
                    string destination = destinationFolder + "\\" + fileName;
                    // Finally, copy the file to the destinationFolder.
                    // If there is a file with the same name already in the destinationFolder,
                    // true ensures it will be overwritten with the file currently being copied over.
                    File.Copy(filePath, destination, true);
                }
                else
                {
                    Console.WriteLine("File will NOT be copied.");
                }
            }
        }
    }
}
