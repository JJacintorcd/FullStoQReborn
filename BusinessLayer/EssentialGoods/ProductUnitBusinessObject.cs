﻿using Recodme.RD.FullStoQReborn.BusinessLayer.OperationResults;
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
    public class ProductUnitBusinessObject
    {
        private readonly ProductUnitDataAccessObject _dao;

        public ProductUnitBusinessObject()
        {
            _dao = new ProductUnitDataAccessObject();
        }

        #region Create
        public OperationResult<bool> Create(ProductUnit item)
        {
            try
            {
                if (_dao.List().Any(x => x.SerialNumber.ToLower().Trim() == item.SerialNumber.ToLower().Trim() && !x.IsDeleted)) return new OperationResult<bool>() { Success = true, Result = false, Message = "Serial number already exists" };
                _dao.Create(item);
                return new OperationResult<bool>() { Success = true, Result = true };
            }
            catch (Exception e)
            {
                return new OperationResult<bool>() { Success = false, Exception = e };
            }
        }
        public async Task<OperationResult<bool>> CreateAsync(ProductUnit item)
        {
            try
            {
                if (_dao.ListAsync().Result.Any(x => x.SerialNumber.ToLower().Trim() == item.SerialNumber.ToLower().Trim() && !x.IsDeleted)) return new OperationResult<bool>() { Success = true, Result = false, Message = "Serial number already exists" };
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
        public OperationResult<ProductUnit> Read(Guid id)
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
                return new OperationResult<ProductUnit>() { Success = true, Result = result };
            }
            catch (Exception e)
            {
                return new OperationResult<ProductUnit>() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult<ProductUnit>> ReadAsync(Guid id)
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
                return new OperationResult<ProductUnit>() { Success = true, Result = res };
            }
            catch (Exception e)
            {
                return new OperationResult<ProductUnit>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Update
        public OperationResult<bool> Update(ProductUnit item)
        {
            try
            {
                if (_dao.ListAsync().Result.Any(x => x.SerialNumber.ToLower().Trim() == item.SerialNumber.ToLower().Trim() && x.Id != item.Id && !x.IsDeleted)) return new OperationResult<bool>() { Success = true, Result = false, Message = "Serial number already exists" };
                _dao.Update(item);
                return new OperationResult<bool>() { Success = true, Result = true };
            }
            catch (Exception e)
            {
                return new OperationResult<bool>() { Success = false, Exception = e };
            }
        }
        public async Task<OperationResult<bool>> UpdateAsync(ProductUnit item)
        {
            try
            {
                if (_dao.ListAsync().Result.Any(x => x.SerialNumber.ToLower().Trim() == item.SerialNumber.ToLower().Trim() && x.Id != item.Id && !x.IsDeleted)) return new OperationResult<bool>() { Success = true, Result = false, Message = "Serial number already exists" };
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
        public OperationResult Delete(ProductUnit item)
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
        public async Task<OperationResult> DeleteAsync(ProductUnit item)
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
        public OperationResult<List<ProductUnit>> List()
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

                return new OperationResult<List<ProductUnit>>() { Success = true, Result = result };
            }
            catch (Exception e)
            {
                return new OperationResult<List<ProductUnit>>() { Success = false, Exception = e };
            }
        }
        public async Task<OperationResult<List<ProductUnit>>> ListAsync()
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
                return new OperationResult<List<ProductUnit>>() { Success = true, Result = res };
            }
            catch (Exception e)
            {
                return new OperationResult<List<ProductUnit>>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region List Not Deleted
        public OperationResult<List<ProductUnit>> ListNotDeleted()
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

                return new OperationResult<List<ProductUnit>>() { Success = true, Result = result };
            }
            catch (Exception e)
            {
                return new OperationResult<List<ProductUnit>>() { Success = false, Exception = e };
            }
        }
        public async Task<OperationResult<List<ProductUnit>>> ListNotDeletedAsync()
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
                return new OperationResult<List<ProductUnit>>() { Success = true, Result = result };
            }
            catch (Exception e)
            {
                return new OperationResult<List<ProductUnit>>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Reserve
        public OperationResult Reserve(Guid productUnitId, Guid shoppingBasketId)
        {
            try
            {
                var transactionOptions = new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TimeSpan.FromSeconds(30)
                };

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var productUnit = _dao.Read(productUnitId);
                productUnit.ShoppingBasketId = shoppingBasketId;
                productUnit.IsReserved = true;

                transactionScope.Complete();

                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }
        public async Task<OperationResult> ReserveAsync(Guid productUnitId, Guid shoppingBasketId)
        {
            try
            {
                var transactionOptions = new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TimeSpan.FromSeconds(30)
                };
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var productUnit = await _dao.ReadAsync(productUnitId);
                productUnit.ShoppingBasketId = shoppingBasketId;
                productUnit.IsReserved = true;

                transactionScope.Complete();

                return new OperationResult<List<ProductUnit>>() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<ProductUnit>>() { Success = false, Exception = e };
            }
        }
        #endregion
    }
}
