using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using NaturalSort.Extension;

namespace d
{
    class Program
    {
        static void Main(string[] args)
        {

            // Init fileSpec search string
            var fileSpec = ".";
            if (args.Length > 0)
            {
                fileSpec = args[0];  // Can reference a file or directory
            }

            // Expand fileSpec to absolute path
            string searchSpec;
            string wildcardSpec = "*";
            try
            {
                // If fileSpec contains wildcard chars, throws ArgumentException
                searchSpec = Path.GetFullPath(fileSpec);  // Expand to absolute path
            }
            catch (ArgumentException)
            {
                // fileSpec includes wildcard chars
                // Trim fileSpec, keeping any preceding path
                var indexBackslash = fileSpec.LastIndexOf(Path.DirectorySeparatorChar);
                var indexSlash = fileSpec.LastIndexOf(Path.AltDirectorySeparatorChar);
                var index = Math.Max(indexBackslash, indexSlash);
                if (index >= 0)
                {
                    // Get absolute path without wildcard portion
                    var trimPathSpec = fileSpec.Substring(0, index + 1);  // Keep the last slash
                    searchSpec = Path.GetFullPath(trimPathSpec);
                    wildcardSpec = fileSpec.Substring(index + 1);
                }
                else
                {
                    searchSpec = Directory.GetCurrentDirectory();
                    wildcardSpec = fileSpec;
                }
            }

            // Construct and output drive volume info
            var driveSpec = Path.GetPathRoot(searchSpec);
            var driveInfo = new DriveInfo(driveSpec);
            Console.WriteLine($" Volume in drive {driveSpec[0]} is {driveInfo.VolumeLabel}");

            // Construct and output volume serial number -- SKIP

            // Output directory spec
            string dirSpec;
            if (Directory.Exists(searchSpec))
            {
                dirSpec = searchSpec;
            }
            else
            {
                dirSpec = Path.GetDirectoryName(searchSpec);
            }
            Console.WriteLine(Environment.NewLine + " Directory of " + dirSpec + Environment.NewLine);

            // Init file and directory accumulation variables
            var numFiles = 0;
            var fileSize = 0L;
            var numDirs = 0;
            
            // If searchSpec references a single file, process it
            if (File.Exists(searchSpec))
            {
                fileSize = ProcessFile(searchSpec);
                ++numFiles;
            }
            else
            {
                // searchSpec references a directory or contains wildcards
                if (Directory.Exists(searchSpec))
                {
                    // Process directories
                    // var searchConfig = new EnumerationOptions(); 
                    var directories = Directory.GetDirectories(searchSpec, wildcardSpec);
                    numDirs = directories.Length;
                    var directoriesOrdered = directories.OrderBy(x => x, StringComparison.OrdinalIgnoreCase.WithNaturalSort());
                    foreach (var directory in directoriesOrdered)
                    {
                        ProcessDirectory(directory);
                    }
                }

                // Process files
                try
                {
                    var files = Directory.GetFiles(searchSpec, wildcardSpec);
                    numFiles = files.Length;
                    var filesOrdered = files.OrderBy(x => x, StringComparison.OrdinalIgnoreCase.WithNaturalSort());
                    foreach (var file in filesOrdered)
                    {
                        fileSize += ProcessFile(file);
                    }
                }catch (DirectoryNotFoundException)
                {
                    // Fall thru... (neither dirs nor files were found)
                }
            }

            // Find any directories or files?
            if (numDirs == 0 && numFiles == 0)
            {
                Console.WriteLine("File Not Found");
                Environment.Exit(0);
            }

            // Output accumulation lines
            // File accumulation
            var numFilesStr = String.Format("{0,15:n0}", numFiles);
            var fileSizeStr = String.Format("{0,15:n0}", fileSize);
            Console.WriteLine($" {numFilesStr} File(s) {fileSizeStr} bytes");

            // Directory accumulation
            var numDirsStr = String.Format("{0,15:n0}", numDirs);
            var freeSpace = String.Format("{0,15:n0}", driveInfo.AvailableFreeSpace);
            Console.WriteLine($" {numDirsStr} Dir(s)  {freeSpace} bytes free");
        }

        const string DateTimeFormat = "yyyy-MMM-dd  hh:mm tt";

        static void ProcessDirectory(string dirSpec)
        {
            // Get the directory's info
            var di = new DirectoryInfo(dirSpec);

            // Get and format the directory's last write time, i.e., creation time
            var dirTime = di.LastWriteTime.ToString(DateTimeFormat, CultureInfo.InvariantCulture);

            // Output line of directory information
            Console.WriteLine($"{dirTime}    <DIR>           {di.Name}");
        }

        static long ProcessFile(string fileSpec)
        {
            // Get the file's info
            var fi = new FileInfo(fileSpec);

            // Get and format the file's last write time
            var fileTime = fi.LastWriteTime.ToString(DateTimeFormat, CultureInfo.InvariantCulture);

            // Get the file's attributes
            // var attributes = File.GetAttributes(fileSpec);

            // Get and format the file's size in bytes
            var fSize = fi.Length;
            var fileSize = String.Format("{0,17:n0}", fSize);

            // Output line of file information
            Console.WriteLine($"{fileTime} {fileSize}  {Path.GetFileName(fileSpec)}" );

            return fSize;
        }
    }
}
