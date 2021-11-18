using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Omega.Model.Entities
{
    [Table("Authors", Schema = "Inventory")]
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }

        public string AuthorName { get; set; }
        public string AuthorLastName { get; set; }
        [DefaultValue(true)]
        public bool IsActive { get; set; }

        // TODO: Agregar referencia de Libros
        public List<Book> Books { get; set; }
    }
}
