using ComputerUtils.ConsoleUi;
using ComputerUtils.FileManaging;
using ComputerUtils.Logging;
using ComputerUtils.VarUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OculusGraphQLApiLib.Game
{
    public class GameDownloader
    {
        private static void Decompress(Stream input, string dest)
        {
            Ionic.Zlib.DeflateStream s = new Ionic.Zlib.DeflateStream(input, Ionic.Zlib.CompressionMode.Decompress);
            FileStream res = File.Open(dest, FileMode.Append);
            s.CopyTo(res);
            res.Close();
            return;
        }

        public static bool DownloadRiftGame(string destination, string access_token, string binaryId)
        {
            if (!destination.EndsWith(Path.DirectorySeparatorChar.ToString())) destination += Path.DirectorySeparatorChar;
            string manifestPath = destination +  "manifest.json";
            DownloadManifest(manifestPath, access_token, binaryId);
            if(!File.Exists(manifestPath))
            {
                Logger.Log("Manifest does not exist");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Manifest does not exist");
                Console.ForegroundColor = ConsoleColor.White;
                return false;
            }
            Console.WriteLine();
            Manifest manifest = JsonSerializer.Deserialize<Manifest>(File.ReadAllText(manifestPath));
            ProgressBarUI totalProgress = new ProgressBarUI();
            totalProgress.Start();
            DownloadProgressUI segmentDownloader = new DownloadProgressUI();
            FileManager.RecreateDirectoryIfExisting("tmp");
            long done = 0;
            Logger.notAllowedStrings.Add(access_token);
            long total = 0;
            foreach (KeyValuePair<string, ManifestFile> f in manifest.files) total += f.Value.size;
            List<KeyValuePair<DateTime, long>> lastBytes = new List<KeyValuePair<DateTime, long>>();
            totalProgress.UpdateProgress(done, total, SizeConverter.ByteSizeToString(done), SizeConverter.ByteSizeToString(total), "", true);
            foreach (KeyValuePair<string, ManifestFile> f in manifest.files)
            {
                List<byte> final = new List<byte>();
                string fileDest = destination + f.Key.Replace('/', Path.DirectorySeparatorChar);
                if (File.Exists(fileDest)) File.Delete(fileDest);
                FileManager.CreateDirectoryIfNotExisting(FileManager.GetParentDirIfExisting(destination + f.Key.Replace('/', Path.DirectorySeparatorChar)));
                foreach (object[] segment in f.Value.segments)
                {
                    string url = "https://securecdn.oculus.com/binaries/segment/?access_token=" + access_token + "&binary_id=" + binaryId + "&segment_sha256=" + segment[1];
                    segmentDownloader.StartDownload(url, "tmp" + Path.DirectorySeparatorChar + "file", true, true, new Dictionary<string, string> { { "User-Agent", Constants.UA } });
                    Stream s = File.OpenRead("tmp" + Path.DirectorySeparatorChar + "file");
                    s.ReadByte();
                    s.ReadByte();
                    Decompress(s, fileDest);
                    s.Close();
                    File.Delete("tmp" + Path.DirectorySeparatorChar +  "file");
                }

                done += new FileInfo(fileDest).Length;
                totalProgress.UpdateProgress(done, total, SizeConverter.ByteSizeToString(done), SizeConverter.ByteSizeToString(total), "", true);
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
            return Validator.ValidateGameInstall(destination, manifestPath);
        }

        public static bool DownloadManifest(string destination, string access_token, string binaryId)
        {
            string baseDownloadLink = "https://securecdn.oculus.com/binaries/download/?id=" + binaryId + "&access_token=" + access_token;
            FileManager.CreateDirectoryIfNotExisting(FileManager.GetParentDirIfExisting(destination));
            Logger.Log("Downloading manifest of " + binaryId);
            Console.WriteLine("Downloading manifest");
            DownloadProgressUI progressUI = new DownloadProgressUI();
            Logger.Log("Downloading manifest");
            Logger.notAllowedStrings.Add(access_token);
            if(!progressUI.StartDownload(baseDownloadLink + "&get_manifest=1", destination + ".zip"))
            {
                Logger.Log("Download of manifest failed. Aborting.", LoggingType.Warning);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                Console.WriteLine("Download of manifest failed. Do you own this game? If you do then please update your access token in case it's expired");
                Console.ForegroundColor = ConsoleColor.White;
                return false;
            }
            ZipArchive a = ZipFile.OpenRead(destination + ".zip");
            foreach(ZipArchiveEntry e in a.Entries)
            {
                if(e.Name.EndsWith(".json"))
                {
                    e.ExtractToFile(destination);
                }
            }
            if(!File.Exists(destination))
            {
                Logger.Log("Manifest download failed");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Manifest download failed");
                Console.ForegroundColor = ConsoleColor.White;
                return false;
            }
            Logger.Log("Download of Manifest succeeded");
            Console.ForegroundColor= ConsoleColor.Green;
            Console.WriteLine("Download of Manifest succeeded");
            Console.ForegroundColor = ConsoleColor.White;
            return true;
        }

        public static bool DownloadMontereyGame(string destination, string access_token, string binaryId)
        {
            string baseDownloadLink = "https://securecdn.oculus.com/binaries/download/?id=" + binaryId + "&access_token=" + access_token;
            Logger.Log("Starting download of " + binaryId);
            Console.WriteLine("Starting download of " + binaryId);
            DownloadProgressUI ui = new DownloadProgressUI();
            Logger.notAllowedStrings.Add(access_token);
            ui.StartDownload(baseDownloadLink, destination, true, true, new Dictionary<string, string> { { "User-Agent", Constants.UA } });
            Logger.Log("Download finished");
            return File.Exists(destination);
        }
    }
}
