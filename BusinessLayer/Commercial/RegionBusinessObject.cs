using Recodme.RD.FullStoQReborn.BusinessLayer.OperationResults;
using Recodme.RD.FullStoQReborn.DataAccessLayer.Commercial;
using Recodme.RD.FullStoQReborn.DataLayer.Commercial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Recodme.RD.FullStoQReborn.BusinessLayer.Commercial
{
    public class RegionBusinessObject
    {
        private readonly RegionDataAccessObject _dao;

        public RegionBusinessObject()
        {
            _dao = new RegionDataAccessObject();

        }

        #region Create

        public OperationResult<bool> Create(Region item)
        {
            try
            {
                if (_dao.List().Any(x => x.Name == item.Name)) return new OperationResult<bool>() { Success = true, Result = false, Message = "Name already exists" };
                _dao.Create(item);
                return new OperationResult<bool>() { Success = true, Result = true };

            }
            catch (Exception e)
            {
                return new OperationResult<bool>() { Success = false, Exception = e };

            }

        }
        public async Task<OperationResult<bool>> CreateAsync(Region item)
        {
            try
            {
                if (_dao.ListAsync().Result.Any(x => x.Name == item.Name)) return new OperationResult<bool>() { Success = true, Result = false, Message = "Name already exists" };
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

        public OperationResult<Region> Read(Guid id)
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
                return new OperationResult<Region>() { Success = true, Result = result };

            }
            catch (Exception e)
            {
                return new OperationResult<Region>() { Success = false, Exception = e };

            }


        }

        public async Task<OperationResult<Region>> ReadAsync(Guid id)
        {
            try
            {
                var transactionOptions = new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TimeSpan.FromSeconds(30)

                };
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var res = await _dao.ReadAsync(id);
                transactionScope.Complete();
                return new OperationResult<Region>() { Success = true, Result = res };

            }
            catch (Exception e)
            {
                return new OperationResult<Region>() { Success = false, Exception = e };

            }

        }

        #endregion

        #region Update

        public OperationResult<bool> Update(Region item)
        {
            try
            {
                if (_dao.List().Any(x => x.Name == item.Name && x.Id != item.Id)) return new OperationResult<bool>() { Success = true, Result = false, Message = "Name already exists" };
                _dao.Update(item);
                return new OperationResult<bool>() { Success = true, Result = true };

            }
            catch (Exception e)
            {
                return new OperationResult<bool>() { Success = false, Exception = e };

            }

        }
        public async Task<OperationResult<bool>> UpdateAsync(Region item)
        {
            try
            {
                if (_dao.ListAsync().Result.Any(x => x.Name == item.Name && x.Id != item.Id)) return new OperationResult<bool>() { Success = true, Result = false, Message = "Name already exists" };
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

        public OperationResult Delete(Region item)
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
        public async Task<OperationResult> DeleteAsync(Region item)
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

        public OperationResult<List<Region>> List()
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

                return new OperationResult<List<Region>>() { Success = true, Result = result };

            }
            catch (Exception e)
            {
                return new OperationResult<List<Region>>() { Success = false, Exception = e };

            }

        }
        public async Task<OperationResult<List<Region>>> ListAsync()
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
                transactionScope.Complete();
                return new OperationResult<List<Region>>() { Success = true, Result = res };

            }
            catch (Exception e)
            {
                return new OperationResult<List<Region>>() { Success = false, Exception = e };

            }
        }

        #endregion

        #region List Not Deleted
        public OperationResult<List<Region>> ListNotDeleted()
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

                return new OperationResult<List<Region>>() { Success = true, Result = result };

            }
            catch (Exception e)
            {
                return new OperationResult<List<Region>>() { Success = false, Exception = e };

            }

        }
        public async Task<OperationResult<List<Region>>> ListNotDeletedAsync()
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
                return new OperationResult<List<Region>>() { Success = true, Result = result };

            }
            catch (Exception e)
            {
                return new OperationResult<List<Region>>() { Success = false, Exception = e };

            }
        }
        #endregion
    }
}
