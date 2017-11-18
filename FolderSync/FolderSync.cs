using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FolderSync
{
    public class FolderSync
    {
        private readonly string _processedFilesPath;
        private HashSet<string> _processedFiles;

        public FolderSync(string registry)
        {
            _processedFilesPath = Path.Combine(registry, "files.txt");
        }

        public void CopyFiles(string source, string destination)
        {
            LoadProcessedFiles();
            var sourceFolder = source.TrimEnd('\\') + '\\';
            var desintationFolder = destination.TrimEnd('\\') + '\\';
            CopyUnprocessedFiles(sourceFolder, desintationFolder);
        }

        private void CopyUnprocessedFiles(string source, string destination)
        {
            foreach (string file in Directory.EnumerateFiles(source, "*.*", SearchOption.AllDirectories))
            {
                var relativePath = MakeRelativePath(source, file);
                if (!FileHasBeenProcessed(relativePath))
                {
                    CopyFile(file, Path.Combine(destination, relativePath));
                    RecordThatFileWasCopied(relativePath);
                }
            }
        }

        private void LoadProcessedFiles()
        {
            if (!File.Exists(_processedFilesPath))
            {
                File.Create(_processedFilesPath).Close();
            }
            _processedFiles = File.ReadAllLines(_processedFilesPath).Select(s => s.ToLower()).ToHashSet();
        }

        private bool FileHasBeenProcessed(string path)
        {
            return _processedFiles.Contains(path.ToLower());
        }

        private static void CopyFile(string source, string destination)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(destination));
            if (!File.Exists(destination))
            {
                File.Copy(source, destination);
            }
        }

        private void RecordThatFileWasCopied(string source)
        {
            File.AppendAllLines(_processedFilesPath, new[] {source.ToLower()});
        }

        // Adapted from https://stackoverflow.com/a/340454/113141
        public static string MakeRelativePath(string fromPath, string toPath)
        {
            var relativeUri = new Uri(fromPath).MakeRelativeUri(new Uri(toPath));
            var relativePath = Uri.UnescapeDataString(relativeUri.ToString());
            return relativePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
        }
    }
}
