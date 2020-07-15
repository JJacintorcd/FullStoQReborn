using Microsoft.EntityFrameworkCore;
using Recodme.RD.FullStoQReborn.DataAccessLayer.Contexts;
using Recodme.RD.FullStoQReborn.DataLayer.EssentialGoods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recodme.RD.FullStoQReborn.DataAccessLayer.EssentialGoods
{
    public class ProductUnitDataAccessObject
    {
        private readonly Context _context;

        public ProductUnitDataAccessObject()
        {
            _context = new Context();
        }

        #region List
        public List<ProductUnit> List()
        {
            return _context.Set<ProductUnit>().ToList();
        }
        public async Task<List<ProductUnit>> ListAsync()
        {
            return await _context.Set<ProductUnit>().ToListAsync();
        }
        #endregion

        #region Create
        public void Create(ProductUnit productModel)
        {
            _context.ProductUnits.Add(productModel);
            _context.SaveChanges();
        }

        public async Task CreateAsync(ProductUnit productModel)
        {
            await _context.ProductUnits.AddAsync(productModel);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Read
        public ProductUnit Read(Guid id)
        {
            return _context.ProductUnits.FirstOrDefault(x => x.Id == id);
        }

        public async Task<ProductUnit> ReadAsync(Guid id)
        {
            return await _context.ProductUnits.FirstOrDefaultAsync(x => x.Id == id);
        }
        #endregion

        #region Update
        public void Update(ProductUnit productModel)
        {
            _context.Entry(productModel).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task UpdateAsync(ProductUnit productModel)
        {
            _context.Entry(productModel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Delete
        public void Delete(ProductUnit productModel)
        {
            productModel.IsDeleted = true;
            Update(productModel);
        }

        public void Delete(Guid id)
        {
            var item = Read(id);
            if (item == null) return;
            Delete(item);
        }

        public async Task DeleteAsync(ProductUnit productModel)
        {
            productModel.IsDeleted = true;
            await UpdateAsync(productModel);
        }

        public async Task DeleteAsync(Guid id)
        {
            var item = ReadAsync(id).Result;
            if (item == null) return;
            await DeleteAsync(item);
        }
        #endregion
    }
}
