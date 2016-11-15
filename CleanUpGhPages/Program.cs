using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanUpGhPages
{
    class Program
    {
        static void Main(string[] args)
        {
            const string distFolderName = "dist";
            List<string> excludedFiles = new List<string>() {".gitignore","404.html","CleanUpGhPages.exe"};
            if (!Directory.Exists(distFolderName))
                Console.WriteLine("dist folder not exist");

            string currentDirectory = Directory.GetCurrentDirectory();
            string distDirectory = Path.Combine(currentDirectory, distFolderName);
            var distFiles = Directory.GetFiles(distFolderName).Select(x=>new FileInfo(x)).ToList();
            var rootFiles = Directory.GetFiles(currentDirectory).Select(x => new FileInfo(x)).Where(x => !excludedFiles.Contains(x.Name));
            List<string> deleteFiles = new List<string>();
            foreach (var file in rootFiles)
            {
                if (distFiles.All(x => x.Name != file.Name))
                {
                    deleteFiles.Add(file.FullName);
                    Console.WriteLine(file.FullName);
                    //File.Delete(file.FullName);
                }
            }
            if (deleteFiles.Any())
            {
                Console.Write("Delete files (y/n)? : ");
                string answer = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(answer) && answer.Equals("y", StringComparison.OrdinalIgnoreCase))
                {
                    foreach (var deleteFile in deleteFiles)
                    {
                        File.Delete(deleteFile);
                    }
                }
                Console.WriteLine("Done");
            }
            else 
                Console.WriteLine("No obsolete files found");
            Console.ReadLine();
        }
    }
}
