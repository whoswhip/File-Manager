# File-Manager
A cool little c# application that lets you rename files, detect duplicate files, use multiple gallery-dl instances at once, send all files in a directory that are =< 25MB to a discord webhook, and generate a secure password or username!

## Credits
File Renamer, Multiple Gallery-dl's, Files to Webhook, Password Generator & Username Generator: Whoswhip
<br/>
Duplicate File Detecter: Themida
<br/>
Gallery-dl: Mike Fährmann

## Features
### File renamer
This allows you to easily rename all files in a specified folder incrementally, randomize the name or do both, randomize then incrementally, aswell as being able to rename them incrementally/randomized with a prefix.
This feature can come in handy in very certain area where you would either just like randomzied file names or files named incrementally to make them look nicer

### Duplicate File Detecter (Made by: Themida)
This uses SHA256 to scans all files in a folder for duplicate files, when the SHA256 matches with another file after all files are scanned in the folder it will prompt you to delete any duplicates.
This is quite helpful when cleaning out your download directory for any duplicates so you can save on space.

### Multiple Gallery-dl's 
This allows you to easily run multiple instances at once so you can scrape all you ever want! 
This does not directly use galler-dl it basically opens command prompt and types in your url like "gallery-dl "{url}" 
Right now this does NOT support oauth or cookies, only websites that dont need you to be logged in.
Check supported websites at https://github.com/mikf/gallery-dl/blob/master/docs/supportedsites.md

### Files to Webhook
This will send ALL files within the directory you provide that are equal to are less than 25 megabytes to a DISCORD webhook.
### Generators 
#### Password Generator 
This will generate a secure password using the RNG Crypto Service, you can generate a random length or put a specified length. This is from my [other repo](https://github.com/whoswhip/password-generator)
#### Username Generator
This will generate a username based off [this list of english words](https://github.com/dwyl/english-words/blob/master/words.txt), enabling prefix will put an emotion at the start of the username like a RecRoom Junior account, you also choose how many words will be in the username, or enable a random number being placed at the end of your username.
# Showcase
## THIS IS OLD
https://github.com/whoswhip/File-Manager/assets/124531971/c1107e88-875b-401a-9065-3475732f903f


