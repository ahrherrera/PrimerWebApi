using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Omega.Model.Entities
{
    [Table("Users", Schema = "Inventory")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

        public int? Age { get; set; }

        [DefaultValue(true)]
        public bool IsActive { get; set; }
    }
}
