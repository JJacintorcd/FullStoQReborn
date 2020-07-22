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
    public class EstablishmentBusinessObject : OperationResult
    {
        private readonly EstablishmentDataAccessObject _dao;
        public EstablishmentBusinessObject()
        {
            _dao = new EstablishmentDataAccessObject();
        }

        #region List
        public OperationResult<List<Establishment>> List()
        {
            try
            {
                var transactionOptions = new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TimeSpan.FromSeconds(30)
                };
                using var ts = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);

                var result = _dao.List();
                ts.Complete();
                return new OperationResult<List<Establishment>>() { Success = true, Result = result };

            }
            catch (Exception e)
            {
                return new OperationResult<List<Establishment>>() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult<List<Establishment>>> ListAsync()
        {
            try
            {
                var transactionOptions = new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TimeSpan.FromSeconds(30)
                };
                using var ts = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);

                var result = await _dao.ListAsync();
                ts.Complete();
                return new OperationResult<List<Establishment>>() { Success = true, Result = result };

            }
            catch (Exception e)
            {
                return new OperationResult<List<Establishment>>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Create
        public OperationResult<bool> Create(Establishment establishment)
        {
            try
            {
                if (_dao.List().Any(x => x.Address == establishment.Address))
                    if (_dao.ListAsync().Result.Any(x => x.Address == establishment.Address)) return new OperationResult<bool>() { Success = true, Result = false, Message = "Address already exists" };
                _dao.Create(establishment);
                return new OperationResult<bool>() { Success = true, Result = true };

            }
            catch (Exception e)
            {
                return new OperationResult<bool>() { Success = false, Exception = e };
            }
        }
        public async Task<OperationResult<bool>> CreateAsync(Establishment establishment)
        {
            try
            {
                if (_dao.ListAsync().Result.Any(x => x.Address == establishment.Address)) return new OperationResult<bool>() { Success = true, Result = false, Message = "Address already exists" };
                await _dao.CreateAsync(establishment);
                return new OperationResult<bool>() { Success = true, Result = true };
            }
            catch (Exception e)
            {
                return new OperationResult<bool>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Read
        public OperationResult<Establishment> Read(Guid id)
        {
            try
            {
                var transactionOptions = new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TimeSpan.FromSeconds(30)
                };
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);

                var res = _dao.Read(id);
                transactionScope.Complete();
                return new OperationResult<Establishment>() { Success = true, Result = res };

            }
            catch (Exception e)
            {
                return new OperationResult<Establishment>() { Success = false, Exception = e };
            }
        }
        public async Task<OperationResult<Establishment>> ReadAsync(Guid id)
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
                return new OperationResult<Establishment>() { Success = true, Result = res };
            }
            catch (Exception e)
            {
                return new OperationResult<Establishment>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Update
        public OperationResult<bool> Update(Establishment establishment)
        {
            try
            {
                if (_dao.List().Any(x => x.Address == establishment.Address && x.Id != establishment.Id)) return new OperationResult<bool>() { Success = true, Result = false, Message = "Address already exists" };
                _dao.Update(establishment);
                return new OperationResult<bool>() { Success = true, Result = true };
            }
            catch (Exception e)
            {
                return new OperationResult<bool>() { Success = false, Exception = e };
            }
        }
        public async Task<OperationResult<bool>> UpdateAsync(Establishment establishment)
        {
            try
            {
                if (_dao.ListAsync().Result.Any(x => x.Address == establishment.Address && x.Id != establishment.Id)) return new OperationResult<bool>() { Success = true, Result = false, Message = "Address already exists" };
                await _dao.UpdateAsync(establishment);
                return new OperationResult<bool>() { Success = true, Result = true };
            }
            catch (Exception e)
            {
                return new OperationResult<bool>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Delete
        public OperationResult Delete(Establishment establishment)
        {
            try
            {
                _dao.Delete(establishment);
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = true, Exception = e };
            }
        }
        public async Task<OperationResult> DeleteAsync(Establishment establishment)
        {
            try
            {
                await _dao.DeleteAsync(establishment);
                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = true, Exception = e };
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
                return new OperationResult() { Success = true, Exception = e };
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
                return new OperationResult() { Success = true, Exception = e };
            }
        }
        #endregion

        #region List Not Deleted
        public OperationResult<List<Establishment>> ListNotDeleted()
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

                return new OperationResult<List<Establishment>>() { Success = true, Result = result };

            }
            catch (Exception e)
            {
                return new OperationResult<List<Establishment>>() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult<List<Establishment>>> ListNotDeletedAsync()
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
                return new OperationResult<List<Establishment>>() { Success = true, Result = result };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Establishment>>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Filter
        public OperationResult<List<Establishment>> Filter(Func<Establishment, bool> predicate)
        {
            try
            {
                TransactionOptions transactionOptions = new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TimeSpan.FromSeconds(30)
                }; 
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = _dao.List();
                result = result.Where(predicate).ToList();
                transactionScope.Complete();
                return new OperationResult<List<Establishment>> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Establishment>>() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult<List<Establishment>>> FilterAsync(Func<Establishment, bool> predicate)
        {
            try
            {
                TransactionOptions transactionOptions = new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TimeSpan.FromSeconds(30)
                };
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = await _dao.ListAsync();
                result = result.Where(predicate).ToList();
                transactionScope.Complete();
                return new OperationResult<List<Establishment>> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Establishment>>() { Success = false, Exception = e };
            }
        }
        #endregion
    }
}
