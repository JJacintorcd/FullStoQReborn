using Recodme.RD.FullStoQReborn.BusinessLayer.OperationResults;
using Recodme.RD.FullStoQReborn.DataAccessLayer.Person;
using Recodme.RD.FullStoQReborn.DataLayer.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Recodme.RD.FullStoQReborn.BusinessLayer.Person
{
    public class ProfileBusinessObject
    {
        private readonly ProfileDataAccessObject _dao;

        public ProfileBusinessObject()
        {
            _dao = new ProfileDataAccessObject();
        }

        #region C
        public OperationResult<bool> Create(Profile item)
        {
            try
            {
                if (_dao.List().Any(x => (x.VatNumber == item.VatNumber || x.PhoneNumber == item.PhoneNumber) && item.Id != x.Id))
                {
                    if (_dao.List().Any(x => x.VatNumber == item.VatNumber && item.Id != x.Id)) return new OperationResult<bool>() { Success = true, Result = false, Message = "Vat number already exists" };
                    if (_dao.List().Any(x => x.PhoneNumber == item.PhoneNumber && item.Id != x.Id)) return new OperationResult<bool>() { Success = true, Result = false, Message = "Phone number already exists" };
                }
                _dao.Create(item);
                return new OperationResult<bool>() { Success = true, Result = true };
            }
            catch (Exception e)
            {
                return new OperationResult<bool>() { Success = false, Exception = e };
            }
        }
        public async Task<OperationResult<bool>> CreateAsync(Profile item)
        {
            try
            {
                if (_dao.List().Any(x => (x.VatNumber == item.VatNumber || x.PhoneNumber == item.PhoneNumber) && item.Id != x.Id))
                {
                    if (_dao.ListAsync().Result.Any(x => x.VatNumber == item.VatNumber && item.Id != x.Id)) return new OperationResult<bool>() { Success = true, Result = false, Message = "Vat number already exists" };
                    if (_dao.ListAsync().Result.Any(x => x.PhoneNumber == item.PhoneNumber && item.Id != x.Id)) return new OperationResult<bool>() { Success = true, Result = false, Message = "Phone number already exists" };
                }
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
        public OperationResult<Profile> Read(Guid id)
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
                return new OperationResult<Profile>() { Success = true, Result = result };
            }
            catch (Exception e)
            {
                return new OperationResult<Profile>() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult<Profile>> ReadAsync(Guid id)
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
                return new OperationResult<Profile>() { Success = true, Result = res };
            }
            catch (Exception e)
            {
                return new OperationResult<Profile>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region U
        public OperationResult<bool> Update(Profile item)
        {
            try
            {
                if (_dao.List().Any(x => (x.VatNumber == item.VatNumber || x.PhoneNumber == item.PhoneNumber) && item.Id != x.Id))
                {
                    if (_dao.List().Any(x => x.VatNumber == item.VatNumber && item.Id != x.Id)) return new OperationResult<bool>() { Success = true, Result = false, Message = "Vat number already exists" };
                    if (_dao.List().Any(x => x.PhoneNumber == item.PhoneNumber && item.Id != x.Id)) return new OperationResult<bool>() { Success = true, Result = false, Message = "Phone number already exists" };
                }
                _dao.Update(item);
                return new OperationResult<bool>() { Success = true, Result = true };
            }
            catch (Exception e)
            {
                return new OperationResult<bool>() { Success = false, Exception = e };
            }
        }
        public async Task<OperationResult<bool>> UpdateAsync(Profile item)
        {
            try
            {
                if (_dao.List().Any(x => (x.VatNumber == item.VatNumber || x.PhoneNumber == item.PhoneNumber) && item.Id != x.Id))
                {
                    if (_dao.ListAsync().Result.Any(x => x.VatNumber == item.VatNumber && item.Id != x.Id)) return new OperationResult<bool>() { Success = true, Result = false, Message = "Vat number already exists" };
                    if (_dao.ListAsync().Result.Any(x => x.PhoneNumber == item.PhoneNumber && item.Id != x.Id)) return new OperationResult<bool>() { Success = true, Result = false, Message = "Phone number already exists" };
                }
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
        public OperationResult Delete(Profile item)
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
        public async Task<OperationResult> DeleteAsync(Profile item)
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
        public OperationResult<List<Profile>> List()
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

                return new OperationResult<List<Profile>>() { Success = true, Result = result };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Profile>>() { Success = false, Exception = e };
            }
        }
        public async Task<OperationResult<List<Profile>>> ListAsync()
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
                return new OperationResult<List<Profile>>() { Success = true, Result = res };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Profile>>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region LND
        public OperationResult<List<Profile>> ListNotDeleted()
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

                return new OperationResult<List<Profile>>() { Success = true, Result = result };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Profile>>() { Success = false, Exception = e };
            }
        }
        public async Task<OperationResult<List<Profile>>> ListNotDeletedAsync()
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
                return new OperationResult<List<Profile>>() { Success = true, Result = result };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Profile>>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Filter
        public OperationResult<List<Profile>> Filter(Func<Profile, bool> predicate)
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
                result = result.Where(predicate).ToList();
                transactionScope.Complete();
                return new OperationResult<List<Profile>> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Profile>>() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult<List<Profile>>> FilterAsync(Func<Profile, bool> predicate)
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
                result = result.Where(predicate).ToList();
                transactionScope.Complete();
                return new OperationResult<List<Profile>> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Profile>>() { Success = false, Exception = e };
            }
        }
        #endregion
    }
}
