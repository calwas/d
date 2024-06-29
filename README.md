# d.exe : A `dir` and `ls` replacement

## Overview

The `d` program is a simple directory/file listing replacement for the `dir` and `ls` shell 
commands found in Windows/DOS, Linux, and macOS.

Program output lists directories first, followed by files. The items in each group 
are listed in naturally-sorted alphabetical order, e.g., File1, File2, File10. Hidden and 
system items are included in the output.

## Run the program
The executable accepts an optional `drive:/path/file` argument that can include `?` and `*` 
wildcards. No other command-line arguments or options are supported.

## Build from source
The program is written in C# using .NET Framework 4.8.1.

The `NaturalSort.Extension` package is used to naturally sort directories and files, e.g., 
File1, File2, File10.

Visual Studio solution and project files are provided. No special compiler or linker options 
are necessary; just load the solution in Visual Studio and build it. Visual Studio should install 
the required sorting package automatically when the solution is opened.

## Sample output

```
   $ d                                                   
    Volume in drive C is Windows                         
                                                         
    Directory of C:\CSharpCode\d                         
                                                         
   2020-Jul-03  05:59 PM    <DIR>           bin          
   2024-Jun-26  08:16 PM    <DIR>           obj          
   2020-Jul-03  05:51 PM    <DIR>           Properties   
   2021-Nov-26  02:36 PM    <DIR>           .git         
   2024-Jun-26  06:55 PM    <DIR>           .vs          
   2024-Jun-26  07:42 PM               565  App.config   
   2024-Jun-26  08:17 PM             3,844  d.csproj     
   2024-Jun-26  07:59 PM               629  d.csproj.user
   2020-Jul-03  05:51 PM             1,107  d.sln        
   2024-Jun-28  06:24 PM             6,414  Program.cs   
   2024-Jun-28  06:28 PM             2,066  README.md    
   2020-Jul-03  05:51 PM                29  .gitignore   
                  7 File(s)          14,654 bytes        
                  5 Dir(s)   44,709,969,920 bytes free   
```
