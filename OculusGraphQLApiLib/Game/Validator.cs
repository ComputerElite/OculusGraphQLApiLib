using ComputerUtils.Logging;
using ComputerUtils.VarUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ComputerUtils.FileManaging;
using OculusGraphQLApiLib.Results;

namespace OculusGraphQLApiLib.Game
{
    public class Validator
    {
        public static bool RepairGameInstall(string gameDirectory, string manifestPath, string access_token, string binaryId)
        {
            return ValidateGameInstall(gameDirectory, manifestPath, true, access_token, binaryId);
        }
        
        public static bool RepairGameInstall(string gameDirectory, string manifestPath, string access_token)
        {
            Manifest manifest = JsonSerializer.Deserialize<Manifest>(File.ReadAllText(manifestPath));
            string id = GetBinaryId(manifest);
            if (id == "")
            {
                Logger.Log("Version not found, cannot validate", LoggingType.Warning);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Version not found, cannot validate. If you think this is an issue, contact ComputerElite");
                Console.ForegroundColor = ConsoleColor.White;
                return false;
            }
            Logger.Log("Found binary id " + id);
            Console.WriteLine("Found version on Oculus. Repairing files...");
            return ValidateGameInstall(gameDirectory, manifestPath, true, access_token, id);
        }

        public static string GetBinaryId(Manifest manifest)
        {
            // We got app id and version string, now we gotta ask the Oculus API for the binary id
            Logger.Log("No binary id passed, getting it from the Oculus API");
            Console.WriteLine("No binary id passed, getting it from the Oculus API. Please wait a second");
            Data<NodesPrimaryBinaryApplication> versionS = GraphQLClient.AllVersionsOfApp(manifest.appId);
            foreach (AndroidBinary v in versionS.data.node.primary_binaries.nodes)
            {
                if (v.versionCode == manifest.versionCode)
                {
                    // Found version
                    return v.id;
                }
            }

            return "";
        }


        public static string GetCheckedString(long done, long total)
        {
            return "Checked " + SizeConverter.ByteSizeToString(done) + " of " + SizeConverter.ByteSizeToString(total) + " (" + (done / (double)total * 100) + " %)";
        }

        public static bool ValidateGameInstall(string gameDirectory, string manifestPath, bool repair = false, string access_token = "", string binaryId = "")
        {
            Console.ForegroundColor = ConsoleColor.White;
            Logger.Log("Validating" + (repair ? " and repairing" : "") + " files of " + gameDirectory);
            Console.WriteLine("Validating" + (repair ? " and repairing" : "") + " files of " + gameDirectory);
            Logger.Log("Loading manifest");
            Console.WriteLine("Loading manifest");
            FileManager.RecreateDirectoryIfExisting(AppDomain.CurrentDomain.BaseDirectory + "tmp");
            if(!File.Exists(manifestPath))
            {
                Logger.Log(manifestPath + " does not exist. Is the right file passed and does it exist? The game may not be installed.");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Manifest couldn't be found, is the game installed?");
                return false;
            }
            Manifest manifest = JsonSerializer.Deserialize<Manifest>(File.ReadAllText(manifestPath));
            SHA256 shaCalculator = SHA256.Create();
            int i = 0;
            int valid = 0;
            long total = 0;
            long done = 0;
            foreach (KeyValuePair<string, ManifestFile> f in manifest.files) total += f.Value.size;
            foreach (KeyValuePair<string, ManifestFile> f in manifest.files)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Logger.Log("Validating " + f.Key);
                Console.WriteLine("Validating " + f.Key);
                string file = gameDirectory + f.Key;
                done += f.Value.size;
                i++;
                if (!File.Exists(file))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Logger.Log("File does not exist", LoggingType.Warning);
                    Console.WriteLine("File does not exist. Downloading");
                    if (repair)
                    {
                        Logger.Log("Redownloading " + f.Key + " to " + file);
                        if (GameDownloader.DownloadFile(f.Value, file, access_token, binaryId))
                        {
                            valid++;
                        }
                        Console.WriteLine(GetCheckedString(done, total));
                    }
                    continue;
                }
                try
                {
                    FileStream stream = File.OpenRead(file);
                    
					if (BitConverter.ToString(shaCalculator.ComputeHash(stream)).Replace("-", "").ToLower() != f.Value.sha256.ToLower())
                    {
                        stream.Close();
                        stream.Dispose();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Logger.Log("Hash does not match", LoggingType.Warning);
                        Console.WriteLine("Hash of " + f.Key + " doesn't match with the one in the manifest! " + (repair ? "Redownloading and fixing file. " : "") + GetCheckedString(done, total));
                        if (repair)
                        {
                            Logger.Log("Redownloading " + f.Key + " to " + file);
                            if (GameDownloader.DownloadFile(f.Value, file, access_token, binaryId)) valid++;
                            Console.WriteLine(GetCheckedString(done, total));
                        }
                    }
                    else
					{
						stream.Close();
						stream.Dispose();
						Console.ForegroundColor = ConsoleColor.Green;
                        Logger.Log("Hash checks out");
                        Console.WriteLine("Hash checks out. " + GetCheckedString(done, total));
                        valid++;
                    }
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Logger.Log("Hash couldn't be computed", LoggingType.Warning);
                    Console.WriteLine("Hash of " + f.Key + " is unable to get Computed (" + e.Message + "). " + GetCheckedString(done, total));
                }
                
            }
            if (i != valid)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Logger.Log(valid + " out of " + i + " files are valid");
                Console.WriteLine("Only " + valid + " out of " + i + " files are valid! " + (repair ? "The files were unable to get repaired." : "Have you modded any file?") + " You can reinstall the version by simply downloading it again via the tool.");
                Console.ForegroundColor = ConsoleColor.White;
                return false;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Logger.Log("Game OK");
                Console.WriteLine("Every file included with the game with the game " + (repair ? "has been checked and repaired" : "is the one it's intended to be") + ". All files ok");
                Console.ForegroundColor = ConsoleColor.White;
                return true;
            }
        }
    }
}
