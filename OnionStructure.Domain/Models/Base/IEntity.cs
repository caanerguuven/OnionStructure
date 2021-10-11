using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionStructure.Domain.Models.Base
{
    public interface IEntity<TId>
    {
        TId Id { get; set; }
    }
}
