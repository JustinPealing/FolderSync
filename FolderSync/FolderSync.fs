namespace FolderSync

open System
open System.Collections.Generic
open System.IO
open System.Linq

type FolderSync(registry:string) =
    let _processedFilesPath = Path.Combine(registry, "files.txt")

    let loadProcessedFiles() = 
        if File.Exists _processedFilesPath
        then (File.ReadAllLines _processedFilesPath).Select(fun s -> s.ToLower()).ToHashSet()
        else HashSet<string>()

    // Adapted from https://stackoverflow.com/a/340454/113141
    let makeRelativePath fromPath toPath = 
        let relativeUri = Uri(fromPath).MakeRelativeUri(Uri(toPath))
        let relativePath = Uri.UnescapeDataString(relativeUri.ToString())
        relativePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar)

    let recordFileWasCopied (path:string) = 
        File.AppendAllLines(_processedFilesPath, [|path.ToLower()|])

    let fileHasBeenProcessed (processed:HashSet<string>) (path:string) =
        processed.Contains(path.ToLower())

    let copyFile source destination =
        Directory.CreateDirectory(Path.GetDirectoryName(destination)) |> ignore
        if not (File.Exists(destination)) then File.Copy(source, destination)

    let copyUnprocessedFiles processed source destination = 
        for file in Directory.EnumerateFiles(source, "*.*", SearchOption.AllDirectories) do
            let relativePath = makeRelativePath source file
            if not (fileHasBeenProcessed processed relativePath) then
                let destinationPath = Path.Combine(destination, relativePath)
                copyFile file destinationPath
                recordFileWasCopied relativePath

    member this.CopyFiles (source:string) (destination:string) = 
        let processed = loadProcessedFiles()
        let sourceFolder = source.TrimEnd('\\') + "\\"
        let desintationFolder = destination.TrimEnd('\\') + "\\"
        copyUnprocessedFiles processed sourceFolder desintationFolder
