using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApi.Entities
{
    public class PriceList:AuditedEntity
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        #region FK Definition

        public long? UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        #endregion
    }
}
