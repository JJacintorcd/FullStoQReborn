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
    public class ProductModelDataAccessObject
    {
        private readonly Context _context;

        public ProductModelDataAccessObject()
        {
            _context = new Context();
        }

        #region Create
        public void Create(ProductModel productModel)
        {
            _context.ProductModels.Add(productModel);
            _context.SaveChanges();
        }

        public async Task CreateAsync(ProductModel productModel)
        {
            await _context.ProductModels.AddAsync(productModel);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Read
        public ProductModel Read(Guid id)
        {
            return _context.ProductModels.FirstOrDefault(x => x.Id == id);
        }

        public async Task<ProductModel> ReadAsync(Guid id)
        {
            return await _context.ProductModels.FirstOrDefaultAsync(x => x.Id == id);
        }
        #endregion

        #region Update
        public void Update(ProductModel productModel)
        {
            _context.Entry(productModel).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task UpdateAsync(ProductModel productModel)
        {
            _context.Entry(productModel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Delete
        public void Delete(ProductModel productModel)
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

        public async Task DeleteAsync(ProductModel productModel)
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


        #region List
        public List<ProductModel> List()
        {
            return _context.Set<ProductModel>().ToList();
        }
        public async Task<List<ProductModel>> ListAsync()
        {
            return await _context.Set<ProductModel>().ToListAsync();
        }
        #endregion
    }
}
