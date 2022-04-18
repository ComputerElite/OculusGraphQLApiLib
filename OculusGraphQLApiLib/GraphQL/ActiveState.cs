using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OculusGraphQLApiLib.GraphQL
{
    public enum ActiveState
    {
        PERMANENT
    }

    public enum GrantReason
    {
        UNKNOWN,
        PAID_OFFER,
        NUX,
        RELEASE_CHANNEL_OFFER
    }
}
