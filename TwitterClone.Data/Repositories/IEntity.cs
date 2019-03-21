using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterClone.Data.Repositories
{
    public interface IEntity<TId>
    {
        TId Id { get; set; }
    }
}
