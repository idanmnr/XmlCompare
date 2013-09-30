ECHO OFF

rem this exe calls the web services and creates the xml files 
rem it needs the config file to know which services to call, 
rem username and pwd, and the name of the file
echo Calling web services...
echo. 
XmlDiffXmlFileFinder.exe "C:\Dev\MSXmlDiffPatch\release candidate\test\animals_services.xml"

rem this exe compares 2 files and produces a diffgram file.
rem the ignore file tells the exe which elements to ignore in the comparison 
rem the changes_view displays the original file and the changes in an html file
rem the diffgram is used later to produce a viewable diff
echo. 
echo Completed calling web services. Starting comparing files...
echo. 
XmlDiffView.exe /o /c /p /w "C:\Dev\MSXmlDiffPatch\release candidate\test\animals.xml" "C:\Dev\MSXmlDiffPatch\release candidate\test\animals_change.xml" "C:\Dev\MSXmlDiffPatch\release candidate\test\animals_changes_view.htm" "C:\Dev\MSXmlDiffPatch\release candidate\test\animals_diffgram.xml" "C:\Dev\MSXmlDiffPatch\release candidate\test\animals_ignore.xml" 

rem this exe will apply the changes found in the comparison to the original file, getting the desired output fle.
echo. 
echo Completed calling compare. Calling XmlPatch...
echo. 
XmlPatch.exe "C:\Dev\MSXmlDiffPatch\release candidate\test\animals.xml" "C:\Dev\MSXmlDiffPatch\release candidate\test\animals_diffgram.xml"  "C:\Dev\MSXmlDiffPatch\release candidate\test\animals_with_changes_applied.xml"
echo. 
echo. 
echo All done. 