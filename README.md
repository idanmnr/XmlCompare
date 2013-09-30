XmlCompare
==========

cmd line utility to compare xml files

Installation
============

copy the folder release_candidate to your local machine and you're done.
 
Setup and Configuration
============

There are 2 config files that need to be set up before running the application.
These files are in the config library.

ea_ignore.xml
In this file you specify which xml elements you want the compare tool to ignore. 
For each element you want to ignore, you declare an <ignoreElement> element and the value in <elementXpath> 
is an xpath value identifying the element to ignore.

ea_services.xml
In this file you specify which web services to contact and get the data from. 
You need to specify the service name, username, pwd, and full path of the output file. 
This file is read by the XmlDiffXmlFileFinder.exe 
