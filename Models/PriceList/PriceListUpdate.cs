using System;

namespace StoreApi.Models.PriceList
{
    public class PriceListUpdate
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
    }
}
