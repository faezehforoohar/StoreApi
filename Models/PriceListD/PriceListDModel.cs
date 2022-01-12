﻿using StoreApi.Entities;
using System;

namespace StoreApi.Models.PriceListD
{
  public class PriceListDModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long Price { get; set; }
        public string PartNumber { get; set; }
        public int YearModel { get; set; }
        public Color Color { get; set; }
        public bool HasGuarantee { get; set; }
        public long PriceListId { get; set; }
        //public PriceListDModel PriceList { get; set; }
        public long Row { get; set; }
    }
}