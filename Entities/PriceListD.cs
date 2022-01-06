using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace StoreApi.Entities
{
    public class PriceListD : AuditedEntity
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public long Price { get; set; }

        public string PortNumber { get; set; }

        public DateTime YearModel { get; set; }

        public Color Color { get; set; }

        public bool HasGuarantee { get; set; }

        #region FK Definition

        public long PriceListId { get; set; }

        [ForeignKey("PriceListId")]
        public PriceList PriceList { get; set; }

        #endregion
    }
    public enum Color
    {
        [Description("Unknown")]
        Unknown = 0,
        [Description("White")]
        White = 1,
        [Description("Black")]
        Black = 2,
        [Description("Blue")]
        Blue = 3,
        [Description("BlueBlack")]
        BlueBlack = 4,
    }
}
