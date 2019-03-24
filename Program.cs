using System;
using System.IO;
using System.Linq;

namespace FileCleaner
{
    class FileCleaner
    {
        static void Main(string[] args)
        {
            string helpMessage =    "\n\nThis program purges files from a directory older than N days. \n\n" +
                                    "Options:\n" +
                                    "-t, --target\t\tFolder path \n" +
                                    "-r, --recursive\t\trecursive flag \n" +
                                    "-d, --days\t\tdelete files older than N days \n" +
                                    "-k, --keep\t\tkeep K amount of files no matter what \n";

            string errorMessage  =  "\nExecution failed: \n\n" +
                                    "You must specify a folder path and the number of days old a file can be. \n" +
                                    @"Example: filecleaner -target C:\testFolder -days 30";

            string target = "";
            int keep = 0;
            int deleteFilesOlderThan = 0;
            bool recursive = false;
        
            //build up arg options
            for(var i = 0; i < args.Length; i++){
                switch(args[i].ToLower()){
                    case "-h":
                    case "-help":
                    case "--help":
                        Console.WriteLine(helpMessage);
                        Environment.Exit(0);
                        break;
                    case "--target":
                    case "-t":
                        target = args[i + 1];
                        break;
                    case "--recursive":
                    case "-r":
                        recursive = true;
                        break;
                    case "--days":
                    case "-d":
                        Int32.TryParse(args[i + 1], out deleteFilesOlderThan);
                        break;
                    case "--keep":
                    case "-k":
                        Int32.TryParse(args[i + 1], out keep);
                        break;
                }
            }

            //folder path and days are a requirement
            if(target.Length > 0 && Directory.Exists(target) &&  deleteFilesOlderThan > 0)
                DeleteFiles(target, deleteFilesOlderThan, recursive, keep);
            else 
                Console.WriteLine(errorMessage);
        }

        static void DeleteFiles(string target, int days, bool isRecursive, int keep)
        {
            string[] files;

            if(isRecursive)
                files = Directory.GetFiles(target, "*.*", SearchOption.AllDirectories);
            else 
                files = Directory.GetFiles(target);

            try{
                foreach (string file in files) {
                    FileInfo fi = new FileInfo(file);
                    if (fi.LastWriteTime < DateTime.Now.AddDays(-days))
                        if(fi.Directory.GetFiles().Length > keep)
                            fi.Delete();
                }
            } catch(Exception ex){
                Console.WriteLine("Error: Deleting Files: " + ex.Message);
            }
        }
    }
}
