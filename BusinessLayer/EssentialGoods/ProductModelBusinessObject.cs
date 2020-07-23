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

        TransactionOptions transactionOptions = new TransactionOptions
        {
            IsolationLevel = IsolationLevel.ReadCommitted,
            Timeout = TimeSpan.FromSeconds(30)
        };

        #region Create

        public OperationResult<bool> Create(ProductModel item)
        {
            try
            {
                if (_dao.List().Any(x => x.BarCode.ToLower().Trim() == item.BarCode.ToLower().Trim() && !x.IsDeleted)) return new OperationResult<bool>() { Success = true, Result = false, Message = "Bar Code already exists" };

                if (item.Amount <= 0) return new OperationResult<bool>() { Success = true, Result = false, Message = "Amount must be more than zero" };
                if (item.Price <= 0) return new OperationResult<bool>() { Success = true, Result = false, Message = "Price must be more than zero" };

                _dao.Create(item);
                return new OperationResult<bool>() { Success = true, Result = true };

            }
            catch (Exception e)
            {
                return new OperationResult<bool>() { Success = false, Exception = e };

            }

        }
        public async Task<OperationResult<bool>> CreateAsync(ProductModel item)
        {
            try
            {
                //if (_dao.ListAsync().Result.Any(x => x.BarCode.ToLower().Trim() == item.BarCode.ToLower().Trim() && !x.IsDeleted)) return new OperationResult<bool>() { Success = true, Result = false, Message = "Bar Code already exists" };

                //if (item.Amount <= 0) return new OperationResult<bool>() { Success = true, Result = false, Message = "Amount must be more than zero" };
                //if (item.Price <= 0) return new OperationResult<bool>() { Success = true, Result = false, Message = "Price must be more than zero" };
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

        public OperationResult<bool> Update(ProductModel item)
        {
            try
            {
                if (_dao.List().Any(x => x.BarCode.ToLower().Trim() == item.BarCode.ToLower().Trim() && x.Id != item.Id && !x.IsDeleted)) return new OperationResult<bool>() { Success = true, Result = false, Message = "Bar Code already exists" };
                if (item.Amount <= 0) return new OperationResult<bool>() { Success = true, Result = false, Message = "Amount must be more than zero" };
                if (item.Price <= 0) return new OperationResult<bool>() { Success = true, Result = false, Message = "Price must be more than zero" };

                _dao.Update(item);
                return new OperationResult<bool>() { Success = true, Result = true };

            }
            catch (Exception e)
            {
                return new OperationResult<bool>() { Success = false, Exception = e };

            }

        }
        public async Task<OperationResult<bool>> UpdateAsync(ProductModel item)
        {
            try
            {
                if (_dao.ListAsync().Result.Any(x => x.BarCode.ToLower().Trim() == item.BarCode.ToLower().Trim() && x.Id != item.Id && !x.IsDeleted)) return new OperationResult<bool>() { Success = true, Result = false, Message = "Bar Code already exists" };
                if (item.Amount <= 0) return new OperationResult<bool>() { Success = true, Result = false, Message = "Amount must be more than zero" };
                if (item.Price <= 0) return new OperationResult<bool>() { Success = true, Result = false, Message = "Price must be more than zero" };

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

        #region Filter
        public OperationResult<List<ProductModel>> Filter(Func<ProductModel, bool> predicate)
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = _dao.List();
                result = result.Where(predicate).ToList();
                transactionScope.Complete();
                return new OperationResult<List<ProductModel>> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<ProductModel>>() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult<List<ProductModel>>> FilterAsync(Func<ProductModel, bool> predicate)
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = await _dao.ListAsync();
                result = result.Where(predicate).ToList();
                transactionScope.Complete();
                return new OperationResult<List<ProductModel>> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<ProductModel>>() { Success = false, Exception = e };
            }
        }
        #endregion
    }
}
