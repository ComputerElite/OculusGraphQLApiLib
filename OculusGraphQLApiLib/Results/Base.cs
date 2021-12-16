using System.Collections.Generic;

namespace OculusGraphQLApiLib.Results
{
    public class GraphQLBase
    {
        public string __typename { get; set; } = "";

        public bool IsFacebookVideo()
        {
            return __typename == "FacebookVideo";
        }

        public bool IsApplication()
        {
            return __typename == "Application";
        }
    }

    public class Data<T>
    {
        public Node<T> data { get; set; } = new Node<T>();
        public List<Error> errors { get; set; } = new List<Error>();
    }

    public class ViewerData<T>
    {
        public Viewer<T> data { get; set; } = new Viewer<T>();
        public List<Error> errors { get; set; } = new List<Error>();
    }

    public class Viewer<T>
    {
        public T viewer { get; set; } = default;
        public string cursor { get; set; } = "";
    }

    public class Nodes<T>
    {
        public long count { get; set; } = 0;
        public List<T> nodes { get; set; } = new List<T>();
    }

    public class Edges<T>
    {
        public long count { get; set; } = 0;
        public List<T> edges { get; set; } = new List<T>();
    }

    public class Node<T>
    {
        public T node { get; set; } = default(T);
        public string cursor { get; set; } = "";
    }
}