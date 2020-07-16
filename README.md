# d.exe : A `dir` and `ls` replacement

## Overview

The `d` program is a simple directory/file listing replacement for the `dir` and `ls` shell 
commands found in Windows/DOS, Linux, and macOS.

Program output lists directories first, followed by files. The items in each group 
are listed in alphabetical order. Hidden and system items are included in the output.

## Run the program
The executable accepts an optional `drive:/path/file` argument that can include `?` and `*` 
wildcards. No other command-line arguments or options are supported.

## Build from source
The program is written in C# using .NET Framework 4.

Dependencies include the `Serilog` and `Serilog.Sinks.Console` packages to implement debug logging. 

Visual Studio solution and project files are provided. No special compiler or linker options 
are necessary; just load the solution in Visual Studio and build it. Visual Studio should install 
the two required logging packages automatically when the solution is opened.

## Sample output
    $ d
     Volume in drive C is Windows
    
     Directory of C:\CSharpCode\d
    
    2020-Jul-02  03:17 PM   <DIR>            .git
    2020-Jun-30  01:58 PM   <DIR>            .vs
    2020-Jul-02  02:32 PM   <DIR>            bin
    2020-Jul-02  02:24 PM   <DIR>            obj
    2020-Jun-30  05:24 PM   <DIR>            packages
    2020-Jun-30  01:58 PM   <DIR>            Properties
    2020-Jul-02  02:54 PM                29  .gitignore
    2020-Jun-30  01:58 PM               189  App.config
    2020-Jul-15  05:54 PM             2,963  d.csproj
    2020-Jul-02  02:19 PM               292  d.csproj.user
    2020-Jun-30  01:58 PM             1,107  d.sln
    2020-Jun-30  05:24 PM               218  packages.config
    2020-Jul-02  02:28 PM             6,376  Program.cs
    2020-Jul-15  06:18 PM               743  README.md
                   8 File(s)          11,917 bytes
                   6 Dir(s)  374,058,573,824 bytes free
