﻿using ComputerUtils.Logging;
using System;
using System.Text.RegularExpressions;

namespace OculusGraphQLApiLib
{
    public class TokenTools
    {
        public static bool IsUserTokenValid(string token, bool output = true)
        {
            //yes this is basic
            Logger.Log("Checking if token matches requirements");
            Console.ForegroundColor = ConsoleColor.Red;
            if (token.Contains("%"))
            {
                Logger.Log("Token contains %. Token most likely comes from an uri and won't work");
                if(output) Console.WriteLine("You got your token from the wrong place. Go to the payload tab. Don't get it from the url.");
                return false;
            }
            if (!token.StartsWith("OC") && !token.StartsWith("FRL"))
            {
                Logger.Log("Token doesn't start with OC or FRL");
                if(output) Console.WriteLine("Tokens must start with 'OC' or 'FRL'. Please get a new one");
                return false;
            }
            if (token.Contains("|"))
            {
                Logger.Log("Token contains | which usually indicates an application token which is not valid for user tokens");
                if(output) Console.WriteLine("You seem to have entered a token of an application. Please get YOUR token. Usually this can be done by using another request in the network tab.");
                return false;
            }
            if (Regex.IsMatch(token, "OC[0-9]{15}")) {
                Logger.Log("Token matches /OC[0-9}{15}/ which usually indicates a changed oculus store token");
                if(output) Console.WriteLine("Don't change your token. This will only cause issues. Check another request for the right token.");
                return false;
            }
            Console.ForegroundColor = ConsoleColor.White;
            return true;
        }
        public static string GetUserTokenErrorMessage(string token)
        {
            //yes this is basic
            Logger.Log("Checking if token matches requirements");
            Console.ForegroundColor = ConsoleColor.Red;
            if (token.Contains("%"))
            {
                Logger.Log("Token contains %. Token most likely comes from an uri and won't work");
                return "You got your token from the wrong place. Go to the payload tab. Don't get it from the url.";
            }
            if (!token.StartsWith("OC") && !token.StartsWith("FRL"))
            {
                Logger.Log("Token doesn't start with OC or FRL");
                return "Tokens must start with 'OC' or 'FRL'. Please get a new one";
            }
            if (token.Contains("|"))
            {
                Logger.Log("Token contains | which usually indicates an application token which is not valid for user tokens");
                return "You seem to have entered a token of an application. Please get YOUR token. Usually this can be done by using another request in the network tab.";
            }
            if (Regex.IsMatch(token, "OC[0-9]{15}"))
            {
                Logger.Log("Token matches /OC[0-9}{15}/ which usually indicates a changed oculus store token");
                return "Don't change your token. This will only cause issues. Check another request for the right token.";
            }
            Console.ForegroundColor = ConsoleColor.White;
            return "";
        }
    }
}