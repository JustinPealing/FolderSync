open System
open System.Reflection
open FolderSync

let ERROR_BAD_ARGUMENTS = 160

let printUsage() = 
    printfn "FolderSync %s" (Assembly.GetExecutingAssembly().GetName().Version.ToString())
    printfn "usage: FolderSync.exe <source> <desintation> <record>"

[<EntryPoint>]
let main args =
    if args.Length < 3 then
        printUsage()
        ERROR_BAD_ARGUMENTS
    else
        FolderSync(args.[2]).CopyFiles args.[0] args.[1]
        0
