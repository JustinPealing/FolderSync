open System
open System.Reflection
open FolderSync

let version = 
    Attribute.GetCustomAttribute(
        Assembly.GetEntryAssembly(), 
        typedefof<AssemblyInformationalVersionAttribute>) :?> AssemblyInformationalVersionAttribute

let printUsage() = 
    printfn "FolderSync %s" version.InformationalVersion
    printfn "usage: FolderSync.exe <source> <desintation> <record>"

[<EntryPoint>]
let main args =
    if args.Length < 3 then
        printUsage()
        160 // ERROR_BAD_ARGUMENTS
    else
        FolderSync(args.[2]).CopyFiles args.[0] args.[1]
        0
