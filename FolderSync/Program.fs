open FolderSync

let printUsage() = 
    printfn "FolderSync 0.1.0-beta"
    printfn "usage: FolderSync.exe <source> <desintation> <record>"

[<EntryPoint>]
let main args =
    if args.Length < 3 then
        printUsage()
        160 // ERROR_BAD_ARGUMENTS
    else
        FolderSync(args.[2]).CopyFiles args.[0] args.[1]
        0
