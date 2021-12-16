using ComputerUtils.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OculusGraphQLApiLib.Game
{
    public class Validator
    {
        public static bool ValidateGameInstall(string gameDirectory, string manifestPath)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Logger.Log("Validating files of " + gameDirectory);
            Console.WriteLine("Validating files of " + gameDirectory);
            Logger.Log("Loading manifest");
            Console.WriteLine("Loading manifest");
            Manifest manifest = JsonSerializer.Deserialize<Manifest>(File.ReadAllText(manifestPath));
            SHA256 shaCalculator = SHA256.Create();
            int i = 0;
            int valid = 0;
            foreach (KeyValuePair<string, ManifestFile> f in manifest.files)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Logger.Log("Validating " + f.Key);
                Console.WriteLine("Validating " + f.Key);
                if (!File.Exists(gameDirectory + f.Key))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Logger.Log("File does not exist", LoggingType.Warning);
                    Console.WriteLine("File does not exist");
                    continue;
                }
                if (BitConverter.ToString(shaCalculator.ComputeHash(File.ReadAllBytes(gameDirectory + f.Key))).Replace("-", "").ToLower() != f.Value.sha256.ToLower())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Logger.Log("Hash does not match", LoggingType.Warning);
                    Console.WriteLine("Hash of " + f.Key + " doesn't match with the one in the manifest!");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Logger.Log("Hash checks out");
                    Console.WriteLine("Hash checks out.");
                    valid++;
                }
                i++;
            }
            if (i != valid)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Logger.Log(valid + " out of " + i + " files are valid");
                Console.WriteLine("Only " + valid + " out of " + i + " files are valid! Have you modded any file? You can reinstall the version by simply downloading it again via the tool.");
                Console.ForegroundColor = ConsoleColor.White;
                return false;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Logger.Log("Game OK");
                Console.WriteLine("Every included file with the game is the one it's intended to be. All files ok");
                Console.ForegroundColor = ConsoleColor.White;
                return true;
            }
        }
    }
}
