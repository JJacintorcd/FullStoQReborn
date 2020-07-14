using Microsoft.EntityFrameworkCore;
using Recodme.RD.FullStoQReborn.DataAccessLayer.Contexts;
using Recodme.RD.FullStoQReborn.DataLayer.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recodme.RD.FullStoQReborn.DataAccessLayer.Queue
{
    public class StoreQueueDataAccessObject
    {
        private readonly Context _context;

        public StoreQueueDataAccessObject()
        {
            _context = new Context();
        }

        #region C

        public void Create(StoreQueue storeQueue)
        {
            _context.StoreQueues.Add(storeQueue);
            _context.SaveChanges();

        }

        public async Task CreateAsync(StoreQueue storeQueue)
        {
            await _context.StoreQueues.AddAsync(storeQueue);
            await _context.SaveChangesAsync();

        }

        #endregion

        #region R

        public StoreQueue Read(Guid id)
        {
            return _context.StoreQueues.FirstOrDefault(x => x.Id == id);

        }

        public async Task<StoreQueue> ReadAsync(Guid id)
        {
            return await _context.StoreQueues.FirstOrDefaultAsync(x => x.Id == id);

        }

        #endregion

        #region U

        public void Update(StoreQueue storeQueue)
        {
            _context.Entry(storeQueue).State = EntityState.Modified;
            _context.SaveChanges();

        }

        public async Task UpdateAsync(StoreQueue storeQueue)
        {
            _context.Entry(storeQueue).State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }

        #endregion

        #region D

        public void Delete(StoreQueue storeQueue)
        {
            storeQueue.IsDeleted = true;
            Update(storeQueue);

        }

        public void Delete(Guid id)
        {
            var item = Read(id);
            if (item == null) return;
            Delete(item);

        }

        public async Task DeleteAsync(StoreQueue storeQueue)
        {
            storeQueue.IsDeleted = true;
            await UpdateAsync(storeQueue);

        }

        public async Task DeleteAsync(Guid id)
        {
            var item = ReadAsync(id).Result;
            if (item == null) return;
            await DeleteAsync(item);

        }

        #endregion

        #region List
        public List<StoreQueue> List()
        {
            return _context.Set<StoreQueue>().ToList();
        }
        public async Task<List<StoreQueue>> ListAsync()
        {
            return await _context.Set<StoreQueue>().ToListAsync();
        }
        #endregion

    }
}
