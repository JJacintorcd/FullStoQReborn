using Microsoft.EntityFrameworkCore;
using Recodme.RD.FullStoQReborn.DataAccessLayer.Contexts;
using Recodme.RD.FullStoQReborn.DataLayer.EssentialGoods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.RD.FullStoQReborn.DataAccessLayer.EssentialGoods
{
    public class ShoppingBasketDataAccessObject
    {
        private Context _context;

        public ShoppingBasketDataAccessObject()
        {
            _context = new Context();
        }

        #region List
        public List<ShoppingBasket> List()
        {
            return _context.Set<ShoppingBasket>().ToList();
        }
        public async Task<List<ShoppingBasket>> ListAsync()
        {
            return await _context.Set<ShoppingBasket>().ToListAsync();
        }
        #endregion

        #region Create
        public void Create(ShoppingBasket ShoppingBasket)
        {
            _context.ShoppingBaskets.Add(ShoppingBasket);
            _context.SaveChanges();
        }

        public async Task CreateAsync(ShoppingBasket ShoppingBasket)
        {
            await _context.ShoppingBaskets.AddAsync(ShoppingBasket);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Read
        public ShoppingBasket Read(Guid id)
        {
            return _context.ShoppingBaskets.FirstOrDefault(x => x.Id == id);
        }

        public async Task<ShoppingBasket> ReadAsync(Guid id)
        {
            return await _context.ShoppingBaskets.FirstOrDefaultAsync(x => x.Id == id);
        }
        #endregion

        #region Update
        public void Update(ShoppingBasket ShoppingBasket)
        {
            _context.Entry(ShoppingBasket).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task UpdateAsync(ShoppingBasket ShoppingBasket)
        {
            _context.Entry(ShoppingBasket).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Delete
        public void Delete(ShoppingBasket ShoppingBasket)
        {
            ShoppingBasket.IsDeleted = true;
            Update(ShoppingBasket);
        }

        public void Delete(Guid id)
        {
            var item = Read(id);
            if (item == null) return;
            Delete(item);
        }

        public async Task DeleteAsync(ShoppingBasket ShoppingBasket)
        {
            ShoppingBasket.IsDeleted = true;
            await UpdateAsync(ShoppingBasket);
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
