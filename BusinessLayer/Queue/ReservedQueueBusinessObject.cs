using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Recodme.RD.FullStoQReborn.BusinessLayer.OperationResults;
using Recodme.RD.FullStoQReborn.DataAccessLayer.Queue;
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
        public OperationResult Create(ReservedQueue item)
        {
            try
            {
                _dao.Create(item);
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }
        public async Task<OperationResult> CreateAsync(ReservedQueue item)
        {
            try
            {
                await _dao.CreateAsync(item);
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
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
        public OperationResult Update(ReservedQueue item)
        {
            try
            {
                _dao.Update(item);
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult> UpdateAsync(ReservedQueue item)
        {
            try
            {
                await _dao.UpdateAsync(item);
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
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

        public OperationResult TwoHourLimitReserve(ReservedQueue item)
        {
            try
            {
                var reserveHour = item.CreatedAt;
                var hourLimit = reserveHour.AddHours(2);
                if (DateTime.UtcNow > hourLimit)_dao.Delete(item);
                return new OperationResult<ReservedQueue>() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<ReservedQueue>() { Success = false, Exception = e };
            }

        }
    }
}
