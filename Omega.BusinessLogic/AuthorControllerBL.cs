using Omega.BusinessLogic.Base;
using Omega.Entities;
using Omega.Model;
using Omega.Model.Entities;
using Omega.Repositories.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omega.BusinessLogic
{
    public class AuthorControllerBL : ControllerBLBase
    {
        public AuthorControllerBL(OmegaContext context) : base(context)
        {

        }

        public Author Add(Author author)
        {
            using (AuthorRepository authorRepository = new AuthorRepository(Context))
            {
                return authorRepository.AddAuthor(author);
            }
        }

        public List<Author> GetAllAuthors()
        {
            using (AuthorRepository authorRepository = new AuthorRepository(Context))
            {
                return authorRepository.GetAll();
            }
        }
    }
}
