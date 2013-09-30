XmlCompare
==========
This project is a cmd line utility for comparing xml files.
You can configure it to ignore certain xml elements that you do not want to be compared. 

The application is built from 3 separate cmd line exe's:
1. XmlDiffXmlFileFinder.exe: This application calls web services defined in a config file and saves the results to files.
Its these files that we later want to compare.
2. XmlDiffView.exe: This application compares 2 files using the ignore config file, generates the comparison diffgram file 
and a html file that shows the 2 files side by side with highlighted changes. 
3. XmlPatch.exe: This application takes the original file and applies the diffgram file to generate the changed file.

Installation
============

copy the folder release_candidate to your local machine and you're done.
 
Setup and Configuration
============

There are 2 config files that need to be set up before running the application.
These files are in the config library.

ea_ignore.xml
-------------
In this file you specify which xml elements you want the compare tool to ignore. 
For each element you want to ignore, you declare an <ignoreElement> element and the value in <elementXpath> 
is an xpath value identifying the element to ignore.
you also need to specify the namespaces used in the xml files you are comparing

ea_services.xml
---------------
In this file you specify which web services to contact and get the data from. 
You need to specify the service name, username, pwd, and full path of the output file. 

**important** - the full paths you specify in this file should then be copied to the ea_xml_compare.bat file.

This file is read by the XmlDiffXmlFileFinder.exe 

Running the Application
========================
To run the application, open the command prompt in the release_candidate location. 
Run the ea_xml_compare.bat batch file.
Before running the bratch file, make sure the file paths are all correct in the batch file. 


