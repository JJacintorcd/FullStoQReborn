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
    public class CategoryDataAccessObject
    {
        private Context _context;
        public CategoryDataAccessObject()
        {
            _context = new Context();
        }

        #region C
        public void Create(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public async Task CreateAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region R
        public Category Read(Guid id)
        {
            return _context.Categories.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Category> ReadAsync(Guid id)
        {
            return await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }
        #endregion

        #region U
        public void Update(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        #endregion

        #region D
        public void Delete(Category category)
        {
            category.IsDeleted = true;
            Update(category);
        }

        public void Delete(Guid id)
        {
            var item = Read(id);
            if (item == null) return;
            Delete(item);
        }

        public async Task DeleteAsync(Category category)
        {
            category.IsDeleted = true;
            await UpdateAsync(category);
        }

        public async Task DeleteAsync(Guid id)
        {
            var item = ReadAsync(id).Result;
            if (item == null) return;
            await DeleteAsync(item);
        }
        #endregion

        #region L
        public List<Category> List()
        {
            return _context.Set<Category>().ToList();
        }
        public async Task<List<Category>> ListAsync()
        {
            return await _context.Set<Category>().ToListAsync();
        }
        #endregion
    }
}
