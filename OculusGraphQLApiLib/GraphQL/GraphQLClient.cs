using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Web;
using ComputerUtils.Logging;
using OculusGraphQLApiLib.Results;

namespace OculusGraphQLApiLib
{
    public class GraphQLClient
    {
        public string uri { get; set; } = "";
        public GraphQLOptions options { get; set; } = new GraphQLOptions();
        public const string oculusUri = "https://graph.oculus.com/graphql";
        public static string userToken = "";
        public const string oculusStoreToken = "OC|752908224809889|";
        public static string forcedLocale = "";
        public static bool throwException = true;
        public static bool log = true;
        public static int retryTimes = 3;
        public static Dictionary<string, string> customHeaders = new Dictionary<string, string>();
        public static JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };
        public delegate void OAuthException(string message);

        public static event OAuthException OnOAuthException;

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

        public string GetForcedLocale()
        {
            return forcedLocale != "" ? "?forced_locale=" + forcedLocale : "";
        }

        public string Request(GraphQLOptions options)
        {
            WebClient c = new WebClient();
            //c.Headers.Add("x-requested-with", "RiftDowngrader");
            if (log) Logger.Log("Doing POST Request to " + uri + " with args " + options.ToLoggingString());
            try
            {
                string returning = c.UploadString(uri + GetForcedLocale(), "POST", options.ToStringEncoded());
                return returning;
            }
            catch (WebException e)
            {
                string response = new StreamReader(e.Response.GetResponseStream()).ReadToEnd();
                if (log) Logger.Log("Request failed (" + e.Status + "): \n" + response, LoggingType.Error);
                Console.ForegroundColor = ConsoleColor.Red;
                if(log) Console.WriteLine("Request to Oculus failed. Please try again later and/or contact ComputerElite.");
                if(throwException) throw new Exception(e.Status.ToString().StartsWith("4") ? "I fuqed up" : "Some Request to Oculus failed so yeah idk how to handle it.");
            }
            return "{}";
        }

        public string Request(bool asBody = false, int retry = 0, string status = "200")
        {
            if (retry == retryTimes)
            {
                if (log) Logger.Log("Retry limit exceeded. Stopping requests");
                Console.ForegroundColor = ConsoleColor.Red;
                if (log) Console.WriteLine("Request to Oculus failed. Please try again later and/or contact ComputerElite.");
                if (throwException) throw new Exception(status.StartsWith("4") ? "I fuqed up" : "Some Request to Oculus failed so yeah idk how to handle it.");
                return "{}";
            }
            if(log && retry != 0) Logger.Log("Starting retry number " + retry);
            WebClient c = new WebClient();
            c.Headers.Add("User-Agent", Constants.UA);
            if (customHeaders != null)
            {
                foreach (KeyValuePair<string, string> header in customHeaders)
                {
                    c.Headers.Add(header.Key, header.Value);
                }
            }
            if (log) Logger.Log("Doing POST Request to " + uri + " with args " + options.ToLoggingString());
            try
            {
                string res = "";
                if (asBody) res = c.UploadString(uri + GetForcedLocale(), "POST", options.ToStringEncoded());
                else res = c.UploadString(uri + "?" + this.options.ToString() + GetForcedLocale().Replace("?", "&"), "POST", "");
                if(log) Logger.Log(res);
                return res;
            }
            catch (WebException e)
            {
                string response = new StreamReader(e.Response.GetResponseStream()).ReadToEnd();
                try
                {
                    ErrorContainer error = JsonSerializer.Deserialize<ErrorContainer>(response);
                    if (error.error.type.ToLower() == "oauthexception")
                    {
                        if (log) Logger.Log("OAuthException: " + error.error.message, LoggingType.Error);
                        OnOAuthException?.Invoke(error.error.message);
                        return "{}";
                    }
                }
                catch (Exception ex)
                {
                    if(log) Logger.Log("Couldn't parse error message: " + ex, LoggingType.Warning);
                }
                if (log) Logger.Log("Request failed, retrying (" + e.Status.ToString() + ", " + (int)e.Status + "): \n" + response, LoggingType.Error);
                return Request(asBody, retry + 1, e.Status.ToString());
            }
            return "{}";
        }

        public static Data<Application> VersionHistory(string appid)
        {
            GraphQLClient c = OculusTemplate();
            c.options.doc_id = "1586217024733717";
            c.options.variables = "{\"id\":\"" + appid + "\"}";
            return JsonSerializer.Deserialize<Data<Application>>(c.Request(), jsonOptions);
        }

        public static ViewerData<OculusUserWrapper> GetCurrentUser()
        {
            GraphQLClient c = OculusTemplate();
            c.options.doc_id = "4149322231793299";
            c.options.variables = "{}";
            return JsonSerializer.Deserialize<ViewerData<OculusUserWrapper>>(c.Request(), jsonOptions);
        }

		public static ReleaseChannelSettingWrapper ChangeSelectedReleaseChannel(string appId, string releaseChannelId)
		{
			GraphQLClient c = OculusTemplate();
			c.options.doc_id = "5380372352071467";
			c.options.variables = "{\"input\":{\"client_mutation_id\":\"2\",\"app_id\":\"" + appId + "\",\"release_channel_id\":\"" + releaseChannelId + "\"}}";
			return JsonSerializer.Deserialize<ReleaseChannelSettingWrapper>(c.Request(), jsonOptions);
		}
        
        public static Data<Application?> GetAppSharingCapabilities(string appId)
        {
            GraphQLClient c = OculusTemplate();
            c.options.doc_id = "6362663970520085";
            c.options.variables = "{\"applicationID\":\"" + appId + "\"}";
            return JsonSerializer.Deserialize<Data<Application?>>(c.Request(), jsonOptions);
        }
        public static Data<ApplicationGrouping?> GetDLCsDeveloper(string groupingId, string cursor = null, int count = 50)
        {
            GraphQLClient c = OculusTemplate();
            if (cursor == null)
            {
                c.options.doc_id = "6980015785377989";
                c.options.variables =
                    "{\"applicationGroupingID\":\"" + groupingId +
                    "\",\"isShownInStore\":null,\"skuOrDisplayNameFilter\":\"\"}";
                return JsonSerializer.Deserialize<Data<ApplicationGrouping?>>(c.Request(), jsonOptions);
            }
            else
            {
                
                c.options.doc_id = "6895974417154683";
                c.options.variables =
                    "{\"after\":\"" + cursor + "\",\"first\":" + count + ",\"id\":\"" + groupingId +
                    "\",\"isShownInStore\":null,\"skuOrDisplayNameFilter\":\"\"}";
                return JsonSerializer.Deserialize<Data<ApplicationGrouping?>>(c.Request(), jsonOptions);
            }
        }

        public static Data<Application> GetAchievements(string appId)
        {
            GraphQLClient c = OculusTemplate();
            c.options.doc_id = "6783430901678561";
            c.options.variables = "{\"applicationID\":\"" + appId + "\"}";
            return JsonSerializer.Deserialize<Data<Application>>(c.Request(), jsonOptions);
        }
        
        public static Data<AchievementDefinition> GetAchievement(string achievementId)
        {
            GraphQLClient c = OculusTemplate();
            c.options.doc_id = "6060216934017996";
            c.options.variables = "{\"achievementID\":\"" + achievementId + "\"}";
            return JsonSerializer.Deserialize<Data<AchievementDefinition>>(c.Request(), jsonOptions);
        }
        
        public static Data<ApplicationForApplicationGroupingNodes> GetAddOnDeveloper(string addOnId, string applicationId)
        {
            GraphQLClient c = OculusTemplate();
            c.options.doc_id = "6596628703765622";
            c.options.variables =
                "{\"addOnID\":\"" + addOnId + "\",\"applicationID\":\"" + applicationId + "\"}";
            return JsonSerializer.Deserialize<Data<ApplicationForApplicationGroupingNodes>>(c.Request(), jsonOptions);
        }

		public static Data<AppStoreAllAppsSection> AllApps(Headset headset, string cursor = null, int maxApps = 500)
        {
            GraphQLClient c = OculusTemplate();
            c.options.doc_id = "6318857928214261";
            string id = "";
            switch(headset)
            {
                case Headset.MONTEREY:
                    id = "391914765228253";
                    break;
                case Headset.HOLLYWOOD:
                    id = "391914765228253";
                    break;
                case Headset.PANTHER:
                    id = "391914765228253";
                    break;
                case Headset.EUREKA:
                    id = "391914765228253";
                    break;
                case Headset.SEACLIFF:
                    id = "391914765228253";
                    break;
                case Headset.RIFT:
                    id = "1736210353282450";
                    break;
                case Headset.LAGUNA:
                    id = "1736210353282450";
                    break;
                case Headset.GEARVR:
                    id = "174868819587665";
                    break;
                case Headset.PACIFIC:
                    id = "174868819587665";
                    break;
            }
            c.options.variables = "{\"sectionId\":\"" + id + "\",\"sortOrder\":[],\"itemCount\":" + maxApps + ",\"cursor\":" + (cursor == null ? "null" : "\"" + cursor + "\"") + ",\"hmdType\":\"" + HeadsetTools.GetHeadsetCodeName(headset) + "\"}";
            return JsonSerializer.Deserialize<Data<AppStoreAllAppsSection>>(c.Request());
        }

        public static Data<NodesPrimaryBinaryApplication> AllVersionsOfApp(string appid) // DONE
        {
            GraphQLClient c = OculusTemplate();
            c.options.doc_id = "2885322071572384";
            c.options.variables = "{\"applicationID\":\"" + appid + "\"}";
            return JsonSerializer.Deserialize<Data<NodesPrimaryBinaryApplication>>(c.Request(), jsonOptions);
        }

        public static Data<EdgesPrimaryBinaryApplication> AllVersionOfAppCursor(string appId, string cursor = null,
            int count = 100)
        {
            GraphQLClient c = OculusTemplate();
            c.options.doc_id = "4410563505712309";
            c.options.variables = "{\"after\":\"" + cursor + "\",\"first\":" + count + ",\"id\":\"" + appId + "\"}";
            return JsonSerializer.Deserialize<Data<EdgesPrimaryBinaryApplication>>(c.Request(), jsonOptions);
        }

        public static ViewerData<OculusUserWrapper> GetActiveEntitelments() // DONE
        {
            GraphQLClient c = OculusTemplate();
            c.options.doc_id = "4850747515044496";
            c.options.variables = "{}";
            return JsonSerializer.Deserialize<ViewerData<OculusUserWrapper>>(c.Request(), jsonOptions);
        }

        public static Data<EdgesPrimaryBinaryApplication> ReleaseChannelsOfApp(string appid) // DONE
        {
            GraphQLClient c = OculusTemplate();
            c.options.doc_id = "3828663700542720";
            c.options.variables = "{\"applicationID\":\"" + appid + "\"}";
            return JsonSerializer.Deserialize<Data<EdgesPrimaryBinaryApplication>>(c.Request(), jsonOptions);
        }

        public static Data<ReleaseChannel> ReleaseChannelReleases(string channelId) // DONE
        {
            GraphQLClient c = OculusTemplate();
            c.options.doc_id = "3973666182694273";
            c.options.variables = "{\"releaseChannelID\":\"" + channelId + "\"}";
            return JsonSerializer.Deserialize<Data<ReleaseChannel>>(c.Request(), jsonOptions);
        }

        public static ViewerData<ContextualSearch> StoreSearch(string query, Headset headset) // DONE
        {
            GraphQLClient c = OculusTemplate();
            c.options.doc_id = "3928907833885295";
            c.options.variables = "{\"query\":\"" + query + "\",\"hmdType\":\"" + HeadsetTools.GetHeadsetCodeName(headset) + "\",\"firstSearchResultItems\":100}";
            return JsonSerializer.Deserialize<ViewerData<ContextualSearch>>(c.Request(), jsonOptions);
        }

        public static GraphQLClient CurrentVersionOfApp(string appid)
        {
            GraphQLClient c = OculusTemplate();
            c.options.doc_id = "1586217024733717";
            c.options.variables = "{\"id\":\"" + appid + "\"}";
            return c;
        }
        
        /// <summary>
        /// GOD DAMN I LOVE THIS ENDPOINT
        /// </summary>
        /// <param name="appid"></param>
        /// <returns></returns>
        public static Data<Application> AppDetailsDeveloperAll(string appid)
        {
            GraphQLClient c = OculusTemplate();
            c.options.doc_id = "6771539532935162";
            c.options.variables = "{\"applicationID\":\"" + appid + "\"}";
            return JsonSerializer.Deserialize<Data<Application>>(c.Request(), jsonOptions);
        }
        
        public static Data<PDPMetadata> PDPMetadata(string pdpMetadataId)
        {
            GraphQLClient c = OculusTemplate();
            c.options.doc_id = "6759554484128015";
            c.options.variables = "{\"pdpMetadataID\":\"" + pdpMetadataId + "\"}";
            return JsonSerializer.Deserialize<Data<PDPMetadata>>(c.Request(), jsonOptions);
        }
        public static Data<ApplicationRevision> AppSubmission(string submissionId)
        {
            GraphQLClient c = OculusTemplate();
            c.options.doc_id = "6848217698591088";
            c.options.variables = "{\"submissionID\":\"" + submissionId + "\"}";
            return JsonSerializer.Deserialize<Data<ApplicationRevision>>(c.Request(), jsonOptions);
        }

        public static DataItem<Application> GetAppDetail(string id, Headset headset)
        {
            GraphQLClient c = OculusTemplate();
            c.options.doc_id = "6549406941839522";
            c.options.variables = "{\"itemId\":\"" + id + "\",\"hmdType\":\"" + HeadsetTools.GetHeadsetCodeName(headset) + "\"}";
            return JsonSerializer.Deserialize<DataItem<Application>>(c.Request(), jsonOptions);
        }

        public static DataItem<Application> AppDetailsMetaStore(string id)
        {
            GraphQLClient c = OculusTemplate();
            c.options.doc_id = "7278635282149220";
            c.options.variables = "{\"itemId\":\"" + id + "\",\"hmdType\":null,\"requestPDPAssetsAsPNG\":false}";
            return JsonSerializer.Deserialize<DataItem<Application>>(c.Request(), jsonOptions);
        }

        public static Data<Application> GetDLCs(string appId)
        {
            GraphQLClient c = OculusTemplate();
            c.options.doc_id = "3853229151363174";
            c.options.variables = "{\"id\":\"" + appId + "\",\"first\":200,\"last\":null,\"after\":null,\"before\":null,\"forward\":true}";
            return JsonSerializer.Deserialize<Data<Application>>(c.Request(), jsonOptions);
        }

        public static Data<OculusBinary> GetBinaryDetails(string binaryId)
        {
            GraphQLClient c = OculusTemplate();
            c.options.doc_id = "4734929166632773";
            c.options.variables = "{\"binaryID\":\"" + binaryId + "\"}";
            return JsonSerializer.Deserialize<Data<OculusBinary>>(c.Request(), jsonOptions);
        }

        public static Data<OculusBinary> GetMoreBinaryDetails(string binaryId)
        {
            GraphQLClient c = OculusTemplate();
            c.options.doc_id = "24072064135771905";
            c.options.variables = "{\"binaryID\":\"" + binaryId + "\"}";
            return JsonSerializer.Deserialize<Data<OculusBinary>>(c.Request(), jsonOptions);
        }

        public static PlainData<AppBinaryInfoContainer> GetAssetFiles(string appId, long versionCode)
        {
            GraphQLClient c = OculusTemplate();
            c.options.doc = "query ($params: AppBinaryInfoArgs!) { app_binary_info(args: $params) { info { binary { ... on OculusBinary { id package_name version_code asset_files { edges { node { ... on AssetFile {  file_name uri size  } } } } } } } }}";
            c.options.variables = "{\"params\":{\"app_params\":[{\"app_id\":\"" + appId + "\",\"version_code\":\"" + versionCode + "\"}]}}";
            return JsonSerializer.Deserialize<PlainData<AppBinaryInfoContainer>>(c.Request(), jsonOptions);
        }

        public static GraphQLClient OculusTemplate()
        {
            GraphQLClient c = new GraphQLClient(oculusUri);
            GraphQLOptions o = new GraphQLOptions();
            o.access_token = userToken != "" ? userToken : oculusStoreToken;
            c.options = o;
            return c;
        }
    }

    public class GraphQLOptions
    {
        public string access_token { get; set; } = "";
        public string variables { get; set; } = "";
        public string doc_id { get; set; } = "";
        public string doc { get; set; } = "";

        public override string ToString()
        {
            return "access_token=" + access_token + "&variables=" + variables + "&doc_id=" + doc_id + "&doc=" + doc;
        }

        public string ToStringEncoded()
        {
            return "access_token=" + HttpUtility.UrlEncode(access_token) + "&variables=" + HttpUtility.UrlEncode(variables) + "&doc_id=" + doc_id + "&doc=" + HttpUtility.UrlEncode(doc);
        }

        public string ToLoggingString()
        {
            return "access_token=aSecret:)&variables=" + variables + "&doc_id=" + doc_id + "&doc=" + doc;
        }
    }}