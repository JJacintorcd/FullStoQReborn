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
    public class BrandBusinessObject
    {
        private readonly BrandDataAccessObject _dao;
        public BrandBusinessObject()
        {
            _dao = new BrandDataAccessObject();
        }



        #region Create
        public OperationResult Create(Brand brand)
        {
            try
            {
                _dao.Create(brand);
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }
        public async Task<OperationResult> CreateAsync(Brand brand)
        {
            try
            {

                await _dao.CreateAsync(brand);
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Read
        public OperationResult<Brand> Read(Guid id)
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
                var res = _dao.Read(id);
                transactionScope.Complete();
                return new OperationResult<Brand>() { Success = true, Result = res };
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
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required,
                    transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
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

        #region Update
        public OperationResult Update(Brand brand)
        {
            try
            {
                _dao.Update(brand);
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult> UpdateAsync(Brand brand)
        {
            try
            {
                await _dao.UpdateAsync(brand);
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Delete
        public OperationResult Delete(Brand brand)
        {
            try
            {
                _dao.Delete(brand);
                return new OperationResult() { Success = true };

            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }
        public async Task<OperationResult> DeleteAsync(Brand brand)
        {
            try
            {
                await _dao.DeleteAsync(brand);
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
        #endregion

        #region List
        public OperationResult<List<Brand>> List()
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

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required,
                    transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
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

        #region List Not Deleted
        public OperationResult<List<Brand>> ListNotDeleted()
        {
            try
            {
                var transactionOptions = new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TimeSpan.FromSeconds(30)
                };

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions,
                    TransactionScopeAsyncFlowOption.Enabled);
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

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions,
                    TransactionScopeAsyncFlowOption.Enabled);
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
    }
}
