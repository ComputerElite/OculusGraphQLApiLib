using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using ComputerUtils.Logging;
using OculusGraphQLApiLib.Results;

namespace OculusGraphQLApiLib
{
    public class GraphQLClient
    {
        public string uri { get; set; } = "";
        public GraphQLOptions options { get; set; } = new GraphQLOptions();
        public const string oculusUri = "https://graph.oculus.com/graphql";
        public static string oculusStoreToken = "OC|752908224809889|";

        public GraphQLClient(string uri, GraphQLOptions options)
        {
            this.uri = uri;
            this.options = options;
        }

        public GraphQLClient(string uri)
        {
            this.uri = uri;
        }

        public GraphQLClient() { }

        public string Request(GraphQLOptions options)
        {
            WebClient c = new WebClient();
            //c.Headers.Add("x-requested-with", "RiftDowngrader");
            Logger.Log("Doing POST Request to " + uri + " with args " + options.ToLoggingString());
            try
            {
                string returning = c.UploadString(uri, "POST", options.ToString());
                return returning;
            }
            catch (WebException e)
            {
                Logger.Log("Request failed (" + e.Status + "): \n" + new StreamReader(e.Response.GetResponseStream()).ReadToEnd(), LoggingType.Error);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Request to Oculus failed. Please try again later and/or contact ComputerElite.");
                throw new Exception(e.Status.ToString().StartsWith("4") ? "I fuqed up" : "Some Request to Oculus failed so yeah idk how to handle it.");
            }

        }

        public string Request(bool asBody = false, Dictionary<string, string> customHeaders = null)
        {
            WebClient c = new WebClient();
            //c.Headers.Add("x-requested-with", "RiftDowngrader");
            if (customHeaders != null)
            {
                foreach (KeyValuePair<string, string> header in customHeaders)
                {
                    c.Headers.Add(header.Key, header.Value);
                }
            }
            Logger.Log("Doing POST Request to " + uri + " with args " + options.ToLoggingString());
            try
            {
                if (asBody)
                {
                    return c.UploadString(uri, "POST", this.options.ToString());
                }
                return c.UploadString(uri + "?" + this.options.ToString(), "POST", "");
            }
            catch (WebException e)
            {
                Logger.Log("Request failed (" + e.Status + "): \n" + new StreamReader(e.Response.GetResponseStream()).ReadToEnd(), LoggingType.Error);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Request to Oculus failed. Please try again later and/or contact ComputerElite.");
                throw new Exception(e.Status.ToString().StartsWith("4") ? "I fuqed up" : "Some Request to Oculus failed so yeah idk how to handle it.");
            }
        }

        public static Data<Application> VersionHistory(string appid)
        {
            GraphQLClient c = OculusTemplate();
            c.options.doc_id = "1586217024733717";
            c.options.variables = "{\"id\":\"" + appid + "\"}";
            return JsonSerializer.Deserialize<Data<Application>>(c.Request());
        }

        public static ViewerData<OculusUserWrapper> GetCurrentUser()
        {
            GraphQLClient c = OculusTemplate();
            c.options.doc_id = "4149322231793299";
            c.options.variables = "{}";
            return JsonSerializer.Deserialize<ViewerData<OculusUserWrapper>>(c.Request());
        }

        public static GraphQLClient AllApps(Headset headset)
        {
            GraphQLClient c = OculusTemplate();
            c.options.doc_id = "3821696797949516";
            string id = "";
            switch(headset)
            {
                case Headset.MONTEREY:
                    id = "1888816384764129";
                    break;
                case Headset.RIFT:
                    id = "1736210353282450";
                    break;
                case Headset.GEARVR:
                    id = "174868819587665";
                    break;
                case Headset.PACIFIC:
                    id = "174868819587665";
                    break;
            }
            c.options.variables = "{\"sectionId\":\"" + id + "\",\"sortOrder\":null,\"sectionItemCount\":1500,\"sectionCursor\":null,\"hmdType\":\"" + Enum.GetName(typeof(Headset), headset) + "\"}";
            return c;
        }

        public static Data<NodesPrimaryBinaryApplication> AllVersionsOfApp(string appid) // DONE
        {
            GraphQLClient c = OculusTemplate();
            c.options.doc_id = "2885322071572384";
            c.options.variables = "{\"applicationID\":\"" + appid + "\"}";
            return JsonSerializer.Deserialize<Data<NodesPrimaryBinaryApplication>>(c.Request());
        }

        public static Data<EdgesPrimaryBinaryApplication> ReleaseChannelsOfApp(string appid) // DONE
        {
            GraphQLClient c = OculusTemplate();
            c.options.doc_id = "3828663700542720";
            c.options.variables = "{\"applicationID\":\"" + appid + "\"}";
            return JsonSerializer.Deserialize<Data<EdgesPrimaryBinaryApplication>>(c.Request());
        }

        public static Data<ReleaseChannel> ReleaseChannelReleases(string channelId) // DONE
        {
            GraphQLClient c = OculusTemplate();
            c.options.doc_id = "3973666182694273";
            c.options.variables = "{\"releaseChannelID\":\"" + channelId + "\"}";
            return JsonSerializer.Deserialize<Data<ReleaseChannel>>(c.Request());
        }

        public static ViewerData<ContextualSearch> StoreSearch(string query, Headset headset) // DONE
        {
            GraphQLClient c = OculusTemplate();
            c.options.doc_id = "3928907833885295";
            c.options.variables = "{\"query\":\"" + query + "\",\"hmdType\":\"" + Enum.GetName(typeof(Headset), headset) + "\",\"firstSearchResultItems\":100}";
            return JsonSerializer.Deserialize<ViewerData<ContextualSearch>>(c.Request());
        }

        public static GraphQLClient CurrentVersionOfApp(string appid)
        {
            GraphQLClient c = OculusTemplate();
            c.options.doc_id = "1586217024733717";
            c.options.variables = "{\"id\":\"" + appid + "\"}";
            return c;
        }

        public static GraphQLClient OculusTemplate()
        {
            GraphQLClient c = new GraphQLClient(oculusUri);
            GraphQLOptions o = new GraphQLOptions();
            o.access_token = oculusStoreToken;
            c.options = o;
            return c;
        }
    }

    public class GraphQLOptions
    {
        public string access_token { get; set; } = "";
        public string variables { get; set; } = "";
        public string doc_id { get; set; } = "";

        public override string ToString()
        {
            return "access_token=" + access_token + "&variables=" + variables + "&doc_id=" + doc_id;
        }

        public string ToLoggingString()
        {
            return "access_token=aSecret:)&variables=" + variables + "&doc_id=" + doc_id;
        }
    }}