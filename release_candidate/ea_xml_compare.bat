ECHO OFF

rem this exe calls the web services and creates the xml files 
rem it needs the config file to know which services to call, 
rem username and pwd, and the name of the file
echo Calling web services...
XmlDiffXmlFileFinder.exe ea_services.xml

rem this exe compares 2 files and produces a diffgram file.
rem the ignore file tells the exe which elements to ignore in the comparison 
rem the changes_view displays the original file and the changes in an html file
rem the diffgram is used later to produce a viewable diff
echo Completed calling web services. Starting comparing files...
XmlDiffView.exe /o /c /p /w "original_file.xml" "changed_file.xml" "changes_view.htm" "diffgram.xml" "ignore.xml" 

rem this exe will apply the changes found in the comparison to the original file, getting the desired output fle.
echo Completed calling compare. Calling XmlPatch...
XmlPatch.exe "original_file.xml" "diffgram.xml" "original_file_with_changes_applied.xml"

echo All done. 