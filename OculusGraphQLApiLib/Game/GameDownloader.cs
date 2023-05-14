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
        public static string customManifestError = "";
        private static void Decompress(Stream input, string dest)
        {
            Ionic.Zlib.DeflateStream s = new Ionic.Zlib.DeflateStream(input, Ionic.Zlib.CompressionMode.Decompress);
            FileStream res = File.Open(dest, FileMode.Append);
            s.CopyTo(res);
            s.Close();
            res.Close();
            res.Dispose();
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
            totalProgress.eTARange = 20;
            DownloadProgressUI segmentDownloader = new DownloadProgressUI();
            segmentDownloader.connections = 10;
            FileManager.RecreateDirectoryIfExisting("tmp");
            long done = 0;
            Logger.notAllowedStrings.Add(access_token);
            long total = 0;
            foreach (KeyValuePair<string, ManifestFile> f in manifest.files) total += f.Value.size;
            List<KeyValuePair<DateTime, long>> lastBytes = new List<KeyValuePair<DateTime, long>>();
            totalProgress.UpdateProgress(done, total, SizeConverter.ByteSizeToString(done), SizeConverter.ByteSizeToString(total), "", true);
            foreach (KeyValuePair<string, ManifestFile> f in manifest.files)
            {

                string fileDest = destination + f.Key.Replace('/', Path.DirectorySeparatorChar);
                Console.WriteLine();
                DownloadFile(f.Value, fileDest, access_token, binaryId, segmentDownloader);
                done += new FileInfo(fileDest).Length;
                totalProgress.UpdateProgress(done, total, SizeConverter.ByteSizeToString(done), SizeConverter.ByteSizeToString(total), "", true);
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
            return Validator.ValidateGameInstall(destination, manifestPath);
        }

        public static bool DownloadFile(ManifestFile file, string fileDest, string access_token, string binaryId, DownloadProgressUI downloadProgressUI = null)
        {
            if(!Logger.notAllowedStrings.Contains(access_token)) Logger.notAllowedStrings.Add(access_token);
            if (downloadProgressUI == null) downloadProgressUI = new DownloadProgressUI();
            if (File.Exists(fileDest)) File.Delete(fileDest);
            FileManager.CreateDirectoryIfNotExisting(FileManager.GetParentDirIfExisting(fileDest));
            int done = 0;
            foreach (object[] segment in file.segments)
			{
				done++;
				Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Downloading file segment " + done + " / " + file.segments.Length);
                string url = "https://securecdn.oculus.com/binaries/segment/?access_token=" + access_token + "&binary_id=" + binaryId + "&segment_sha256=" + segment[1];
                if (!downloadProgressUI.StartDownload(url, AppDomain.CurrentDomain.BaseDirectory + "tmp" + Path.DirectorySeparatorChar + "file", true, true, new Dictionary<string, string> { { "User-Agent", Constants.UA } })) return false;
                Stream s = File.OpenRead(AppDomain.CurrentDomain.BaseDirectory + "tmp" + Path.DirectorySeparatorChar + "file");
                s.ReadByte();
                s.ReadByte();
                Decompress(s, fileDest);
                s.Close();
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "tmp" + Path.DirectorySeparatorChar + "file");
            }
            return true;
        }

        public static bool DownloadManifest(string destination, string access_token, string binaryId)
        {
            string baseDownloadLink = "https://securecdn.oculus.com/binaries/download/?id=" + binaryId + "&access_token=" + access_token;
            FileManager.CreateDirectoryIfNotExisting(FileManager.GetParentDirIfExisting(destination));
            if (File.Exists(destination)) File.Delete(destination);
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
                Console.WriteLine("Download of manifest failed.\n\nDo you own this game?\n\n-If you do, check if you got the right headset selected in the main menu.\n- If that's the case update your access_token in case it's expired.\n\n " + customManifestError);
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

        public static bool DownloadGearVRGame(string destination, string access_token, string binaryId)
        {
            return DownloadMontereyGame(destination, access_token, binaryId);
        }

        public static bool DownloadPacificGame(string destination, string access_token, string binaryId)
        {
            return DownloadMontereyGame(destination, access_token, binaryId);
        }

        public static bool DownloadMontereyGame(string destination, string access_token, string binaryId)
        {
            string baseDownloadLink = "https://securecdn.oculus.com/binaries/download/?id=" + binaryId + "&access_token=" + access_token;
            Logger.Log("Starting download of " + binaryId);
            Console.WriteLine("Starting download of " + binaryId);
            DownloadProgressUI ui = new DownloadProgressUI();
            ui.connections = 10;
            Logger.notAllowedStrings.Add(access_token);
            if(!ui.StartDownload(baseDownloadLink, destination, true, true, new Dictionary<string, string> { { "User-Agent", Constants.UA } }))
            {
                return false;
            }
            Logger.Log("Download finished");
            return File.Exists(destination);
        }

        public static bool DownloadObbFiles(string destinationDir, string access_token, List<Obb> obbs)
        {
            Logger.Log("Downloading " + obbs.Count + " obb files");
            FileManager.CreateDirectoryIfNotExisting(destinationDir);
            ProgressBarUI totalProgress = new ProgressBarUI();
            totalProgress.Start();
            totalProgress.eTARange = 20;
            DownloadProgressUI segmentDownloader = new DownloadProgressUI();
            segmentDownloader.connections = 10;
            FileManager.RecreateDirectoryIfExisting("tmp");
            long done = 0;
            long doneFiles = 0;
            Logger.notAllowedStrings.Add(access_token);
            long total = 0;
            long totalFiles = 0;
            foreach (Obb f in obbs) total += f.bytes;
            totalFiles = obbs.Count;
            List<KeyValuePair<DateTime, long>> lastBytes = new List<KeyValuePair<DateTime, long>>();
            totalProgress.UpdateProgress(done, total, doneFiles + " files (" + SizeConverter.ByteSizeToString(done) + ")", totalFiles + " files (" + SizeConverter.ByteSizeToString(total) + ")", "", true);
            foreach (Obb f in obbs)
            {
                string fileDest = destinationDir + f.filename;
                Console.WriteLine();
                segmentDownloader.StartDownload("https://securecdn.oculus.com/binaries/download/?id=" + f.id + "&access_token=" + access_token, fileDest, true, true, new Dictionary<string, string> { { "User-Agent", Constants.UA } });
                done += new FileInfo(fileDest).Length;
                doneFiles++;
                totalProgress.UpdateProgress(done, total, doneFiles + " files (" + SizeConverter.ByteSizeToString(done) + ")", totalFiles + " files (" + SizeConverter.ByteSizeToString(total) + ")", "", true);
            }
            return true;
        }
    }

    public class Obb
    {
        public string id { get; set; } = "";
        public string filename { get; set; } = "";
        public long bytes { get; set; } = 0;
    }
}
