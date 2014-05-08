rem this exe calls the web services and creates the xml files 
rem it needs the config file to know which services to call, 
rem username and pwd, and the name of the file

echo Calling web services...
XmlDiffXmlFileFinder.exe ea_services.xml

rem this exe compares 2 files and produces a diffgram file.
rem the ignore file tells the exe which elements to ignore in the comparison 
rem the diffgram is used later to produce a viewable diff

echo Completed calling web services. Starting comparing files...
XmlDiffView.exe /o /c /p /w "R12 Staging Field Service.xml" "R12 Prod Field Service.xml" "ea_ignore.xml" "ea_diffgram.xml" "ea_diff_view.htm"

rem this exe will apply the changes found in the comparison to the original file, getting the desired output fle.
echo Completed calling compare. Calling XmlPatch...
XmlPatch.exe "R12 Staging Field Service.xml" "ea_diffgram.xml" "R12 Staging Field Service Patched.xml"
