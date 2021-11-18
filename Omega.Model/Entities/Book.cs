using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Omega.Model.Entities
{
    [Table("Books", Schema = "Inventory")]
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        public string BookName { get; set; }
        public string BookCode { get; set; }
        public DateTime PublishDate { get; set; }
        public int Quantity { get; set; }

        [DefaultValue(true)]
        public bool IsActive { get; set; }

        // TODO: Referencia del Autor
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
