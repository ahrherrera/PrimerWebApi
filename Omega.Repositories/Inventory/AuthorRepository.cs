using Omega.Model;
using Omega.Model.Entities;
using Omega.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Omega.Repositories.Inventory
{
    public class AuthorRepository : RepositoryBase<Author, InventoryContext>
    {

        public AuthorRepository(InventoryContext inventoryContext) : base(inventoryContext)
        {

        }

        public Author AddAuthor(Author author)
        {
            var createdAuthor = Add(author);
            SaveChanges();
            return createdAuthor;
        }

        public override long GetId(Expression<Func<Author, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public override List<long> GetIds(Expression<Func<Author, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
