using System;
using System.IO;
using System.Linq;

namespace FileCleaner
{
    class FileCleaner
    {
        static void Main(string[] args)
        {
            bool recursive = false;
            bool help = false;
            string target = "";
            int keep = 0;
            int deleteFilesOlderThan = 0;
        
            //build up arg options
            for(var i = 0; i < args.Length; i++){
                switch(args[i].ToLower()){
                    case "-h":
                    case "-help":
                    case "--help":
                        Console.WriteLine("\n\n This program purges files from a directory older than N days. \n" +
                                            "\t\t -target or -t: Folder path \n" +
                                            "\t\t -recursive or -r: recursive flag \n" +
                                            "\t\t -days or -d: delete files older than N days \n" +
                                            "\t\t -keep or -k: keep K amount of files no matter what \n" );
                        break;
                    case "-target":
                    case "-t":
                        target = args[i + 1];
                        break;
                    case "-recursive":
                    case "-r":
                        recursive = true;
                        break;
                    case "-days":
                    case "-d":
                        Int32.TryParse(args[i + 1], out deleteFilesOlderThan);
                        break;
                    case "-keep":
                        Int32.TryParse(args[i + 1], out keep);
                        break;
                }
            }

            Console.WriteLine(keep);
            
            //folder path and days are a requirement
            if(target.Length > 0 && Directory.Exists(target) &&  deleteFilesOlderThan > 0 && !help){
                DeleteFiles(target, deleteFilesOlderThan, recursive, keep);
            } else {
                Console.WriteLine(  "\nExecution failed: \n" +
                                    "You must specify a folder path and the number of days old a file can be. \n" +
                                    @"Example: filecleaner -target C:\testFolder -days 30");
            }
        }

        static void DeleteFiles(string target, int days, bool isRecursive, int keep)
        {
            string[] files;

            if(isRecursive)
                files = Directory.GetFiles(target, "*.*", SearchOption.AllDirectories);
            else 
                files = Directory.GetFiles(target);

            foreach (string file in files) {
                FileInfo fi = new FileInfo(file);
                if (fi.LastWriteTime < DateTime.Now.AddDays(-days))
                    if(fi.Directory.GetFiles().Length > keep)
                        fi.Delete();
            }
        }
    }
}
