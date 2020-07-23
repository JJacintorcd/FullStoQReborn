using Recodme.RD.FullStoQReborn.BusinessLayer.OperationResults;
using Recodme.RD.FullStoQReborn.DataAccessLayer.EssentialGoods;
using Recodme.RD.FullStoQReborn.DataLayer.EssentialGoods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Recodme.RD.FullStoQReborn.BusinessLayer.EssentialGoods
{
    public class BrandBusinessObject
    {
        private readonly BrandDataAccessObject _dao;

        public BrandBusinessObject()
        {
            _dao = new BrandDataAccessObject();
        }

        TransactionOptions transactionOptions = new TransactionOptions
        {
            IsolationLevel = IsolationLevel.ReadCommitted,
            Timeout = TimeSpan.FromSeconds(30)
        };

        #region C
        public OperationResult<bool> Create(Brand item)
        {
            try
            {
                if (_dao.List().Any(x => x.Name.ToLower().Trim() == item.Name.ToLower().Trim() && !x.IsDeleted)) return new OperationResult<bool>() { Success = true, Result = false, Message = "Name already exists" };
                _dao.Create(item);
                return new OperationResult<bool>() { Success = true, Result = true };
            }
            catch (Exception e)
            {
                return new OperationResult<bool>() { Success = false, Exception = e };
            }
        }
        public async Task<OperationResult<bool>> CreateAsync(Brand item)
        {
            try
            {
                if (_dao.ListAsync().Result.Any(x => x.Name.ToLower().Trim() == item.Name.ToLower().Trim() && !x.IsDeleted)) return new OperationResult<bool>() { Success = true, Result = false, Message = "Name already exists" };
                await _dao.CreateAsync(item);
                return new OperationResult<bool>() { Success = true, Result = true };
            }
            catch (Exception e)
            {
                return new OperationResult<bool>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region R
        public OperationResult<Brand> Read(Guid id)
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
                return new OperationResult<Brand>() { Success = true, Result = result };
            }
            catch (Exception e)
            {
                return new OperationResult<Brand>() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult<Brand>> ReadAsync(Guid id)
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
                return new OperationResult<Brand>() { Success = true, Result = res };
            }
            catch (Exception e)
            {
                return new OperationResult<Brand>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region U
        public OperationResult<bool> Update(Brand item)
        {
            try
            {
                if (_dao.List().Any(x => x.Name.ToLower().Trim() == item.Name.ToLower().Trim() && x.Id != item.Id && !x.IsDeleted)) return new OperationResult<bool>() { Success = true, Result = false, Message = "Name already exists" };
                _dao.Update(item);
                return new OperationResult<bool>() { Success = true, Result = true };
            }
            catch (Exception e)
            {
                return new OperationResult<bool>() { Success = false, Exception = e };
            }
        }
        public async Task<OperationResult<bool>> UpdateAsync(Brand item)
        {
            try
            {
                if (_dao.List().Any(x => x.Name.ToLower().Trim() == item.Name.ToLower().Trim() && x.Id != item.Id && !x.IsDeleted)) return new OperationResult<bool>() { Success = true, Result = false, Message = "Name already exists" };
                await _dao.UpdateAsync(item);
                return new OperationResult<bool>() { Success = true, Result = true };
            }
            catch (Exception e)
            {
                return new OperationResult<bool>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region D
        public OperationResult Delete(Brand item)
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
        public async Task<OperationResult> DeleteAsync(Brand item)
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

        #region L
        public OperationResult<List<Brand>> List()
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

                return new OperationResult<List<Brand>>() { Success = true, Result = result };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Brand>>() { Success = false, Exception = e };
            }
        }
        public async Task<OperationResult<List<Brand>>> ListAsync()
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
                return new OperationResult<List<Brand>>() { Success = true, Result = res };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Brand>>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region LND
        public OperationResult<List<Brand>> ListNotDeleted()
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

                return new OperationResult<List<Brand>>() { Success = true, Result = result };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Brand>>() { Success = false, Exception = e };
            }
        }
        public async Task<OperationResult<List<Brand>>> ListNotDeletedAsync()
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
                return new OperationResult<List<Brand>>() { Success = true, Result = result };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Brand>>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Filter
        public OperationResult<List<Brand>> Filter(Func<Brand, bool> predicate)
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = _dao.List();
                result = result.Where(predicate).ToList();
                transactionScope.Complete();
                return new OperationResult<List<Brand>> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Brand>>() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult<List<Brand>>> FilterAsync(Func<Brand, bool> predicate)
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = await _dao.ListAsync();
                result = result.Where(predicate).ToList();
                transactionScope.Complete();
                return new OperationResult<List<Brand>> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Brand>>() { Success = false, Exception = e };
            }
        }
        #endregion
    }
}
