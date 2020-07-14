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
    public class BrandDataAccessObject
    {
        private readonly Context _context;
        public BrandDataAccessObject()
        {
            _context = new Context();
        }

        #region C
        public void Create(Brand brand)
        {
            _context.Brands.Add(brand);
            _context.SaveChanges();
        }

        public async Task CreateAsync(Brand brand)
        {
            await _context.Brands.AddAsync(brand);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region R
        public Brand Read(Guid id)
        {
            return _context.Brands.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Brand> ReadAsync(Guid id)
        {
            return await _context.Brands.FirstOrDefaultAsync(x => x.Id == id);
        }
        #endregion

        #region U
        public void Update(Brand brand)
        {
            _context.Entry(brand).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task UpdateAsync(Brand brand)
        {
            _context.Entry(brand).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        #endregion

        #region D
        public void Delete(Brand brand)
        {
            brand.IsDeleted = true;
            Update(brand);
        }

        public void Delete(Guid id)
        {
            var item = Read(id);
            if (item == null) return;
            Delete(item);
        }

        public async Task DeleteAsync(Brand brand)
        {
            brand.IsDeleted = true;
            await UpdateAsync(brand);
        }

        public async Task DeleteAsync(Guid id)
        {
            var item = ReadAsync(id).Result;
            if (item == null) return;
            await DeleteAsync(item);
        }
        #endregion

        #region L
        public List<Brand> List()
        {
            return _context.Set<Brand>().ToList();
        }
        public async Task<List<Brand>> ListAsync()
        {
            return await _context.Set<Brand>().ToListAsync();
        }
        #endregion
    }
}
