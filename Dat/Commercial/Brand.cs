using Recodme.RD.FullStoQReborn.Data.Base;
using Recodme.RD.FullStoQReborn.DataLayer.EssentialGoods;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recodme.RD.FullStoQReborn.DataLayer.Commercial
{
    public class Brand : NamedEntity
    {
        public virtual ICollection<ProductModel> ProductModels { get; set; }

        public Brand(string name) : base(name)
        {
        }

        public Brand(Guid id, DateTime createdAt, DateTime updatedAt, bool isDeleted, string name) :
            base(id, createdAt, updatedAt, isDeleted, name)
        {
        }
    }
}
