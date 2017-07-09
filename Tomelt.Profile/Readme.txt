﻿
== creating profiling image ==

Create a SQL Server database named Tomelt, on the . instance. This can be changed in profiling-setup-commands.txt

From the base of the checkout, execute
>./build.cmd profiling

* the ./tomelt.proj creates an tomelt web site at ./build/Profiling
* the ./src/Tomelt.Profile/profiling-setup-commands.txt holds the additional tomelt.exe steps used to initialize

The localhost:80 web server should be pointed to the ./build/Profiling folder at this point
The "admin" password is "profiling-secret" without quotes



== repeatable load for code profiling ==

Attach your profiler to the web server
Run one of the unit tests from the ./src/Tomelt.Profile class library
Each test produces a specific, repeatable set of requests against localhost:80



== stess load wcat capacity profiling ==

This will be done from a command prompt running as administrator in the ./src/Tomelt.Profile folder

If wcat is not already configured for your system, execute
./src/Tomelt.Profile>Initialize.cmd

To run one of the scenario scripts, like dashboard.txt, execute
./src/Tomelt.Profile>Go.cmd dashboard

Scenarios are located at ./src/Tomelt.Profile/Scripts with a .txt extension
They generate a variable, high level of load against localhost:80

(see wcat documentation for further information about scenario files and wcat capabilities)

