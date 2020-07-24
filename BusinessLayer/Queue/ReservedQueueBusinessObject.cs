using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Recodme.RD.FullStoQReborn.BusinessLayer.OperationResults;
using Recodme.RD.FullStoQReborn.DataAccessLayer.Queue;
using Recodme.RD.FullStoQReborn.DataLayer.Person;
using Recodme.RD.FullStoQReborn.DataLayer.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Recodme.RD.FullStoQReborn.BusinessLayer.Queue
{
    public class ReservedQueueBusinessObject
    {
        private readonly ReservedQueueDataAccessObject _dao;

        public ReservedQueueBusinessObject()
        {
            _dao = new ReservedQueueDataAccessObject();

        }

        #region Create
        public OperationResult<bool> Create(ReservedQueue item)
        {
            try
            {
                var reserveCountToday = _dao.List().Count(x => x.ProfileId == item.ProfileId && x.CreatedAt.Day == DateTime.UtcNow.Day && !x.IsDeleted && x.CreatedAt.AddHours(2) > DateTime.UtcNow);
                var reserveCancelationCountToday = _dao.List().Count(x => x.ProfileId == item.ProfileId && x.CreatedAt.Day == DateTime.UtcNow.Day && x.IsDeleted);
                if (reserveCountToday > 0 && reserveCancelationCountToday < 2) return new OperationResult<bool>() { Success = true, Result = false, Message = "Daily limit reached" };
                _dao.Create(item);
                return new OperationResult<bool>() { Success = true, Result = true };
            }
            catch (Exception e)
            {
                return new OperationResult<bool>() { Success = false, Exception = e };
            }
        }
        public async Task<OperationResult<bool>> CreateAsync(ReservedQueue item)
        {
            try
            {
                var reserveList = await _dao.ListAsync();
                var reserveCountToday = reserveList.Count(x => x.ProfileId == item.ProfileId && x.CreatedAt.Day == DateTime.UtcNow.Day && !x.IsDeleted && x.CreatedAt.AddHours(2) > DateTime.UtcNow);
                var reserveCancelledList = await _dao.ListAsync();
                var reserveCancelationCountToday = reserveCancelledList.Count(x => x.ProfileId == item.ProfileId && x.CreatedAt.Day == DateTime.UtcNow.Day && x.IsDeleted);
                if (reserveCountToday > 0 && reserveCancelationCountToday < 2) return new OperationResult<bool>() { Success = true, Result = false, Message = "Daily limit reached" };
                await _dao.CreateAsync(item);
                return new OperationResult<bool>() { Success = true, Result = true };
            }
            catch (Exception e)
            {
                return new OperationResult<bool>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Read
        public OperationResult<ReservedQueue> Read(Guid id)
        {
            try
            {
                var transactionOptions = new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TimeSpan.FromSeconds(30)
                };
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = _dao.Read(id);
                transactionScope.Complete();
                return new OperationResult<ReservedQueue>() { Success = true, Result = result };
            }

            catch (Exception e)
            {
                return new OperationResult<ReservedQueue>() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult<ReservedQueue>> ReadAsync(Guid id)
        {
            try
            {
                var transactionOptions = new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TimeSpan.FromSeconds(30)
                };
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required,
                    transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = await _dao.ReadAsync(id);
                transactionScope.Complete();
                return new OperationResult<ReservedQueue>() { Success = true, Result = result };
            }

            catch (Exception e)
            {
                return new OperationResult<ReservedQueue>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Update
        public OperationResult<bool> Update(ReservedQueue item)
        {
            try
            {
                var reserveCountToday = _dao.List().Count(x => x.ProfileId == item.ProfileId && x.CreatedAt.Day == DateTime.UtcNow.Day);
                if (reserveCountToday > 0) return new OperationResult<bool>() { Success = true, Result = false };
                _dao.Update(item);
                return new OperationResult<bool>() { Success = true, Result = true };
            }
            catch (Exception e)
            {
                return new OperationResult<bool>() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult<bool>> UpdateAsync(ReservedQueue item)
        {
            try
            {
                var reserveList = await _dao.ListAsync();
                var reserveCountToday = reserveList.Count(x => x.ProfileId == item.ProfileId && x.CreatedAt.Day == DateTime.UtcNow.Day);
                if (reserveCountToday > 0) return new OperationResult<bool>() { Success = true, Result = false };
                await _dao.UpdateAsync(item);
                return new OperationResult<bool>() { Success = true, Result = true };
            }
            catch (Exception e)
            {
                return new OperationResult<bool>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Delete
        public OperationResult Delete(ReservedQueue item)
        {
            try
            {
                _dao.Delete(item);
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult> DeleteAsync(Guid id)
        {
            try
            {
                await _dao.DeleteAsync(id);
                return new OperationResult() { Success = true };
            }

            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }

        public OperationResult Delete(Guid id)
        {
            try
            {
                _dao.Delete(id);
                return new OperationResult() { Success = true };
            }

            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult> DeleteAsync(ReservedQueue item)
        {
            try
            {
                await _dao.DeleteAsync(item);
                return new OperationResult() { Success = true };
            }

            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }
        #endregion

        #region List
        public OperationResult<List<ReservedQueue>> List()
        {
            try
            {
                var transactionOptions = new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TimeSpan.FromSeconds(30)
                };

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = _dao.List();
                transactionScope.Complete();

                return new OperationResult<List<ReservedQueue>>() { Success = true, Result = result };
            }

            catch (Exception e)
            {
                return new OperationResult<List<ReservedQueue>>() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult<List<ReservedQueue>>> ListAsync()
        {
            try
            {
                var transactionOptions = new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TimeSpan.FromSeconds(30)
                };

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = await _dao.ListAsync();
                transactionScope.Complete();

                return new OperationResult<List<ReservedQueue>>() { Success = true, Result = result };
            }

            catch (Exception e)
            {
                return new OperationResult<List<ReservedQueue>>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region List Not Deleted
        public OperationResult<List<ReservedQueue>> ListNotDeleted()
        {
            try
            {
                var transactionOptions = new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TimeSpan.FromSeconds(30)
                };

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = _dao.List().Where(x => !x.IsDeleted).ToList();

                transactionScope.Complete();

                return new OperationResult<List<ReservedQueue>>() { Success = true, Result = result };
            }

            catch (Exception e)
            {
                return new OperationResult<List<ReservedQueue>>() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult<List<ReservedQueue>>> ListNotDeletedAsync()
        {
            try
            {
                var transactionOptions = new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TimeSpan.FromSeconds(30)
                };

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var res = await _dao.ListAsync();
                var result = res.Where(x => !x.IsDeleted).ToList();
                transactionScope.Complete();
                return new OperationResult<List<ReservedQueue>>() { Success = true, Result = result };
            }

            catch (Exception e)
            {
                return new OperationResult<List<ReservedQueue>>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Reserve Limits
        public OperationResult<bool> TwoHourLimitReserve(Guid id)
        {
            try
            {
                var item = _dao.Read(id);
                var reserveHour = item.CreatedAt;
                var hourLimit = reserveHour.AddHours(2);
                if (DateTime.UtcNow > hourLimit)
                {
                    _dao.Delete(item);
                    return new OperationResult<bool>() { Success = true, Result = false };
                }
                return new OperationResult<bool>() { Success = true, Result = true };
            }
            catch (Exception e)
            {
                return new OperationResult<bool>() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult<bool>> TwoHourLimitReserveAsync(Guid id)
        {
            try
            {
                var item = _dao.ReadAsync(id).Result;
                var reserveHour = item.CreatedAt;
                var hourLimit = reserveHour.AddHours(2);
                //if (DateTime.UtcNow > hourLimit)
                //{
                //    await _dao.DeleteAsync(item);
                //    return new OperationResult<bool>() { Success = true, Result = false };
                //}
                return new OperationResult<bool>() { Success = true, Result = true };
            }
            catch (Exception e)
            {
                return new OperationResult<bool>() { Success = false, Exception = e };
            }
        }
        #endregion
    }
}
