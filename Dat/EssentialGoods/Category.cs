﻿using Recodme.RD.FullStoQReborn.Data.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recodme.RD.FullStoQReborn.DataLayer.EssentialGoods
{
    public class Category : NamedEntity
    {
        public virtual ICollection<ProductModel> ProductModels { get; set; }

        public Category(string name) : base(name)
        {
        }

        public Category(Guid id, DateTime createdAt, DateTime updatedAt, bool isDeleted, string name)
            : base(id, createdAt, updatedAt, isDeleted, name)
        {
        }
    }
}
