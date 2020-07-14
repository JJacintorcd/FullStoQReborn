using Microsoft.EntityFrameworkCore;
using Recodme.RD.FullStoQReborn.DataAccessLayer.Contexts;
using Recodme.RD.FullStoQReborn.DataLayer.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recodme.RD.FullStoQReborn.DataAccessLayer.Person
{
    public class ProfileDataAccessObject
    {
        private Context _context;
        public ProfileDataAccessObject()
        {
            _context = new Context();
        }

        #region C
        public void Create(Profile category)
        {
            _context.Profiles.Add(category);
            _context.SaveChanges();
        }

        public async Task CreateAsync(Profile category)
        {
            await _context.Profiles.AddAsync(category);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region R
        public Profile Read(Guid id)
        {
            return _context.Profiles.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Profile> ReadAsync(Guid id)
        {
            return await _context.Profiles.FirstOrDefaultAsync(x => x.Id == id);
        }
        #endregion

        #region U
        public void Update(Profile category)
        {
            _context.Entry(category).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task UpdateAsync(Profile category)
        {
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        #endregion

        #region D
        public void Delete(Profile category)
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

        public async Task DeleteAsync(Profile category)
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
        public List<Profile> List()
        {
            return _context.Set<Profile>().ToList();
        }
        public async Task<List<Profile>> ListAsync()
        {
            return await _context.Set<Profile>().ToListAsync();
        }
        #endregion
    }
}
