using System;

namespace StoreApi.Models.PriceList
{
    public class PriceListCreate
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string DateTime { get; set; }
    }
}
