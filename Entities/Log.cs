using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApi.Entities
{
    [Table("Logs")]
    public class Log : AuditedEntity
    {
        [Required]
        public LogType LogType { get; set; }

        [Required]
        public DateTime Date_Time { get; set; }

        [Required]
        public long RowId { get; set; }

        public string Description { get; set; }

        [Required]
        public string TableName { get; set; }

        #region FK Definition

        public long UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        #endregion

    }
    public enum LogType
    {
        [Description("Insert")]
        Insert = 1,
        [Description("Update")]
        Update = 2,
        [Description("Delete")]
        Delete = 3,
        [Description("Login")]
        Login = 4,
        [Description("Error")]
        Error = 5,
    }
}
