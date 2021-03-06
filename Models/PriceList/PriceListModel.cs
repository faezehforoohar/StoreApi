using StoreApi.Entities;
using System;

namespace StoreApi.Models.PriceList
{
  public class PriceListModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string DateTime { get; set; }
        public long UserId { get; set; }
        public long Row { get; set; }
    }
}