using System;
using System.Collections.Generic;

namespace OculusGraphQLApiLib.Results
{
    public class GraphQLBase
    {
        public string __typename { get; set; } = "";
        public OculusTypeName typename_enum { get
            {
                OculusTypeName result = OculusTypeName.Unknown;
                if(!Enum.TryParse(__typename, true, out result)) result = OculusTypeName.Unknown;
                return result;
            } }
    }

    public class Data<T>
    {
        public Node<T> data { get; set; } = new Node<T>();
        public List<Error> errors { get; set; } = new List<Error>();
    }
    public class DataItem<T>
    {
        public Item<T> data { get; set; } = new Item<T>();
        public Extensions extensions { get; set; } = new Extensions();
    }

    public class Item<T>
    {
        public T item { get; set; } = default(T);
    }

    public class Extensions
    {
        public bool is_final { get; set; } = false;
    }

    public class PlainData<T>
    {
        public T data { get; set; } = default(T);
        public List<Error> errors { get; set; } = new List<Error>();
    }

    public class ViewerData<T>
    {
        public Viewer<T> data { get; set; } = new Viewer<T>();
        public List<Error> errors { get; set; } = new List<Error>();
    }

    public class Viewer<T>: GraphQLBase
    {
        public T viewer { get; set; } = default;
        public string cursor { get; set; } = "";
    }

    public class Nodes<T> : GraphQLBase
    {
        public long? count { get; set; } = 0;
        public List<T> nodes { get; set; } = new List<T>();
    }

    public class Edges<T>: GraphQLBase
    {
        public long? count { get; set; } = 0;
        public List<T> edges { get; set; } = new List<T>();
        public PageInfo page_info { get; set; } = new PageInfo();
    }

    public class PageInfo
    {
        public string end_cursor { get; set; } = "";
        public string start_cursor { get; set; } = "";
        public bool has_next_page { get; set; } = false;
    }

    public class Node<T>
    {
        public T node { get; set; } = default(T);
        public string cursor { get; set; } = "";
    }

    public enum OculusTypeName
    {
        Unknown,
        PCBinary,
        AndroidBinary,
        Application,
        FacebookVideo,
        AppItemBundle,
        IAPItem,
        AppStoreAllAppsSection,
        AppStoreOffer,
        XOCApplicationPDPMetadata,
        ApplicationSubmission
    }
}