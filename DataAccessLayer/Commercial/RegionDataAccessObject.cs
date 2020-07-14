using Microsoft.EntityFrameworkCore;
using Recodme.RD.FullStoQReborn.DataAccessLayer.Contexts;
using Recodme.RD.FullStoQReborn.DataLayer.Commercial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recodme.RD.FullStoQReborn.DataAccessLayer.Commercial
{
    public class RegionDataAccessObject
    {
        private Context _context;

        public RegionDataAccessObject()
        {
            _context = new Context();
        }

        #region Create
        public void Create(Region region)
        {
            _context.Regions.Add(region);
            _context.SaveChanges();
        }

        public async Task CreateAsync(Region region)
        {
            await _context.Regions.AddAsync(region);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Read
        public Region Read(Guid id)
        {
            return _context.Regions.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Region> ReadAsync(Guid id)
        {
            return await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }
        #endregion

        #region Update
        public void Update(Region region)
        {
            _context.Entry(region).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task UpdateAsync(Region region)
        {
            _context.Entry(region).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Delete
        public void Delete(Region region)
        {
            region.IsDeleted = true;
            Update(region);
        }

        public void Delete(Guid id)
        {
            var item = Read(id);
            if (item == null) return;
            Delete(item);
        }

        public async Task DeleteAsync(Region region)
        {
            region.IsDeleted = true;
            await UpdateAsync(region);
        }

        public async Task DeleteAsync(Guid id)
        {
            var item = ReadAsync(id).Result;
            if (item == null) return;
            await DeleteAsync(item);
        }
        #endregion

        #region List
        public List<Region> List()
        {
            return _context.Set<Region>().ToList();
        }
        public async Task<List<Region>> ListAsync()
        {
            return await _context.Set<Region>().ToListAsync();
        }
        #endregion       
    }
}