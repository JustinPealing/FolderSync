# FolderSync

Synchronizes files between two folders, ensuring that files are copied only once, even if they are deleted from the destination folder.

## How to run

    FolderSync.exe <source> <destination> <registry>

Where `<source>` is the path to the source directory, `<destination>` is the path to the destination directory and `<registry>` is the path to a folder in which FolderSync can track which files have been copied. E.g.

    FolderSync.exe "c:\source" "c:\destination" "c:\registry"

## How to build

The following commands will build the project for the `win-x64` target.

    cd FolderSync
    dotnet publish -c Release -r win-x64
