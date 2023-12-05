namespace OculusGraphQLApiLib.Results;

public enum ExternalSubscriptionType
{
    UNKNOWN = -1,
    NOT_REQUIRED = 0,
    REQUIRED_FOR_SOME_CONTENT = 1,
    REQUIRED = 2,
}