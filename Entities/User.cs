using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StoreApi.Entities
{
    public class User: AuditedEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }

        [Required]
        public UserType UserType { get; set; }

    }
    public enum UserType
    {
        [Description("Unknown")]
        Unknown = 0,
        [Description("Administrator")]
        Administrator = 1,
        [Description("Admin")]
        Admin = 2,
        [Description("Customer")]
        Customer = 3
    }
}
