using System;
using System.Collections.Generic;
using System.Text;

namespace Vubiquity.Test.Client
{
    public class ClientConfiguration : IClientConfiguration
    {
        public string BaseUri { get; set; }
        public string BasketResourceUri { get; set; }
        public string BasketItemResourceUri { get; set; }
    }
}
