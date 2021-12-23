using ComputerUtils.Logging;
using System;

namespace OculusGraphQLApiLib
{
    public class TokenTools
    {
        public static bool IsUserTokenValid(string token)
        {
            //yes this is basic
            Logger.Log("Checking if token matches requirements");
            Console.ForegroundColor = ConsoleColor.Red;
            if (token.Contains("%"))
            {
                Logger.Log("Token contains %. Token most likely comes from an uri and won't work");
                Console.WriteLine("You got your token from the wrong place. Go to the payload tab. Don't get it from the url.");
                return false;
            }
            if (!token.StartsWith("OC"))
            {
                Logger.Log("Token doesn't start with OC");
                Console.WriteLine("Tokens must start with 'OC'. Please get a new one");
                return false;
            }
            if (token.Contains("|"))
            {
                Logger.Log("Token contains | which usually indicates an application token which is not valid for user tokens");
                Console.WriteLine("You seem to have entered a token of an application. Please get YOUR token. Usually this can be done by using another request in the network tab.");
                return false;
            }
            Console.ForegroundColor = ConsoleColor.White;
            return true;
        }
    }
}