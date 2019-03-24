# filecleaner
CLI tool that purges older files.  Meant to be used with a task scheduler.

## Problem
Applications that keep backups and logs without any means to remove old ones after moving to a remote location.  Generally, a script is created to achieve this but is written without any considerations to common edge cases.

## Options
-t, --target            Folder path  
-r, --recursive         recursive flag  
-d, --days              delete files older than N days  
-k, --keep              keep K amount of files no matter what  

## Examples

//Delete files in the test folder older than 30 days  
filecleaner -t c:\test -d 30

//Delete files in the test folder and all sub folders older than 20 days  
filecleaner -t c:\test -d 20 -r

//Delete files in the test folder older than 20 days but keep 10 files no matter how old they are  
filecleaner -t c:\test -d 20 -k 10
