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
    public class CategoryBusinessObject
    {
        private readonly CategoryDataAccessObject _dao;

        TransactionOptions transactionOptions = new TransactionOptions
        {
            IsolationLevel = IsolationLevel.ReadCommitted,
            Timeout = TimeSpan.FromSeconds(30)
        };

        public CategoryBusinessObject()
        {
            _dao = new CategoryDataAccessObject();
        }

        #region C
        public OperationResult<bool> Create(Category item)
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
        public async Task<OperationResult<bool>> CreateAsync(Category item)
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
        public OperationResult<Category> Read(Guid id)
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
                return new OperationResult<Category>() { Success = true, Result = result };
            }
            catch (Exception e)
            {
                return new OperationResult<Category>() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult<Category>> ReadAsync(Guid id)
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
                return new OperationResult<Category>() { Success = true, Result = res };
            }
            catch (Exception e)
            {
                return new OperationResult<Category>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region U
        public OperationResult<bool> Update(Category item)
        {
            try
            {
                if (_dao.ListAsync().Result.Any(x => x.Name.ToLower().Trim() == item.Name.ToLower().Trim() && x.Id != item.Id && !x.IsDeleted)) return new OperationResult<bool>() { Success = true, Result = false, Message = "Name already exists" };
                _dao.Update(item);
                return new OperationResult<bool>() { Success = true, Result = true };
            }
            catch (Exception e)
            {
                return new OperationResult<bool>() { Success = false, Exception = e };
            }
        }
        public async Task<OperationResult<bool>> UpdateAsync(Category item)
        {
            try
            {
                if (_dao.ListAsync().Result.Any(x => x.Name.ToLower().Trim() == item.Name.ToLower().Trim() && x.Id != item.Id && !x.IsDeleted)) return new OperationResult<bool>() { Success = true, Result = false, Message = "Name already exists" };
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
        public OperationResult Delete(Category item)
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
        public async Task<OperationResult> DeleteAsync(Category item)
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
        public OperationResult<List<Category>> List()
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

                return new OperationResult<List<Category>>() { Success = true, Result = result };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Category>>() { Success = false, Exception = e };
            }
        }
        public async Task<OperationResult<List<Category>>> ListAsync()
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
                return new OperationResult<List<Category>>() { Success = true, Result = res };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Category>>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region LND
        public OperationResult<List<Category>> ListNotDeleted()
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

                return new OperationResult<List<Category>>() { Success = true, Result = result };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Category>>() { Success = false, Exception = e };
            }
        }
        public async Task<OperationResult<List<Category>>> ListNotDeletedAsync()
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
                return new OperationResult<List<Category>>() { Success = true, Result = result };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Category>>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Filter
        public OperationResult<List<Category>> Filter(Func<Category, bool> predicate)
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = _dao.List();
                result = result.Where(predicate).ToList();
                transactionScope.Complete();
                return new OperationResult<List<Category>> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Category>>() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult<List<Category>>> FilterAsync(Func<Category, bool> predicate)
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = await _dao.ListAsync();
                result = result.Where(predicate).ToList();
                transactionScope.Complete();
                return new OperationResult<List<Category>> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Category>>() { Success = false, Exception = e };
            }
        }
        #endregion
    }
}
