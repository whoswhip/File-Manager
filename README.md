# File-Manager
A cool little c# application that lets you rename files and detect duplicates easily 

## Credits
File Renamer & Multiple Gallery-dl's: Whoswhip
<br/>
Duplicate File Detecter: Themida
<br/>
Gallery-dl: Mike Fährmann

## Features
### File renamer
This allows you to easily rename all files in a specified folder incrementally, randomize the name or do both, randomize then incrementally.
This feature can come in handy in very certain area where you would either just like randomzied file names or files named incrementally to make them look nicer

### Duplicate File Detecter (Made by: Themida)
This uses SHA256 to scans all files in a folder for duplicate files, when the SHA256 matches with another file after all files are scanned in the folder it will prompt you to delete any duplicates.
This is quite helpful when cleaning out your download directory for any duplicates so you can save on space.

### Multiple Gallery-dl's 
This allows you to easily run multiple instances at once so you can scrape all you ever want! 
This does not directly use galler-dl it basically opens command prompt and types in your url like "gallery-dl "{url}" 
Right now this does NOT support oauth or cookies, only websites that dont need you to be logged in.
Check supported websites at https://github.com/mikf/gallery-dl/blob/master/docs/supportedsites.md
# Showcase
https://github.com/whoswhip/File-Manager/assets/124531971/c1107e88-875b-401a-9065-3475732f903f


