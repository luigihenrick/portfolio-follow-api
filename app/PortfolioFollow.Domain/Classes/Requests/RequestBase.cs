using System;
using System.Collections.Generic;
using System.Text;

namespace PortfolioFollow.Domain.Classes.Requests
{
    public abstract class RequestBase
    {
        public RequestBase(AssetType type)
        {
            Type = type;
        }

        public AssetType Type { get; }
    }
}
