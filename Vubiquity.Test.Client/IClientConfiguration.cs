using System;
using System.Collections.Generic;
using System.Text;

namespace Vubiquity.Test.Client
{
    public interface IClientConfiguration
    {
        string BaseUri { get; set; }
        string BasketResourceUri { get; set; }
        string BasketItemResourceUri { get; set; }
    }
}
