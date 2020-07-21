using Recodme.RD.FullStoQReborn.BusinessLayer.OperationResults;
using Recodme.RD.FullStoQReborn.DataAccessLayer.EssentialGoods;
using Recodme.RD.FullStoQReborn.DataLayer.EssentialGoods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Recodme.RD.FullStoQReborn.BusinessLayer.EssentialGoods
{
    public class ProductModelBusinessObject
    {
        private readonly ProductModelDataAccessObject _dao;

        public ProductModelBusinessObject()
        {
            _dao = new ProductModelDataAccessObject();

        }

        #region Create

        public OperationResult Create(ProductModel item)
        {
            try
            {
                if (_dao.List().Any(x => x.BarCode == item.BarCode)) return new OperationResult() { Success = false, Message = "Bar code already exists" };
                _dao.Create(item);
                return new OperationResult() { Success = true };

            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };

            }

        }
        public async Task<OperationResult> CreateAsync(ProductModel item)
        {
            try
            {
                if (_dao.ListAsync().Result.Any(x => x.BarCode == item.BarCode)) return new OperationResult() { Success = false, Message = "Bar code already exists" };
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

        public OperationResult<ProductModel> Read(Guid id)
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
                return new OperationResult<ProductModel>() { Success = true, Result = result };

            }
            catch (Exception e)
            {
                return new OperationResult<ProductModel>() { Success = false, Exception = e };

            }


        }

        public async Task<OperationResult<ProductModel>> ReadAsync(Guid id)
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
                return new OperationResult<ProductModel>() { Success = true, Result = res };

            }
            catch (Exception e)
            {
                return new OperationResult<ProductModel>() { Success = false, Exception = e };

            }

        }

        #endregion

        #region Update

        public OperationResult Update(ProductModel item)
        {
            try
            {
                if (_dao.ListAsync().Result.Any(x => x.BarCode == item.BarCode)) return new OperationResult() { Success = false, Message = "Bar code already exists" };
                _dao.Update(item);
                return new OperationResult() { Success = true };

            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };

            }

        }
        public async Task<OperationResult> UpdateAsync(ProductModel item)
        {
            try
            {
                if (_dao.ListAsync().Result.Any(x => x.BarCode == item.BarCode)) return new OperationResult() { Success = false, Message = "Bar code already exists" };
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

        public OperationResult Delete(ProductModel item)
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
        public async Task<OperationResult> DeleteAsync(ProductModel item)
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

        public OperationResult<List<ProductModel>> List()
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

                return new OperationResult<List<ProductModel>>() { Success = true, Result = result };

            }
            catch (Exception e)
            {
                return new OperationResult<List<ProductModel>>() { Success = false, Exception = e };

            }

        }
        public async Task<OperationResult<List<ProductModel>>> ListAsync()
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
                return new OperationResult<List<ProductModel>>() { Success = true, Result = res };

            }
            catch (Exception e)
            {
                return new OperationResult<List<ProductModel>>() { Success = false, Exception = e };

            }
        }

        #endregion

        #region List Not Deleted
        public OperationResult<List<ProductModel>> ListNotDeleted()
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

                return new OperationResult<List<ProductModel>>() { Success = true, Result = result };

            }
            catch (Exception e)
            {
                return new OperationResult<List<ProductModel>>() { Success = false, Exception = e };

            }

        }
        public async Task<OperationResult<List<ProductModel>>> ListNotDeletedAsync()
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
                return new OperationResult<List<ProductModel>>() { Success = true, Result = result };

            }
            catch (Exception e)
            {
                return new OperationResult<List<ProductModel>>() { Success = false, Exception = e };

            }
        }
        #endregion
    }
}
