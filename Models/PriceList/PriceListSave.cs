using System;

namespace StoreApi.Models.PriceList
{
    public class PriceListSave
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public long UserId { get; set; }
    }
}
