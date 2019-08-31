
using Microsoft.EntityFrameworkCore;
using SCA.Common.Result;
using SCA.DataAccess.Context;
using SCA.Model;
using SCA.Repository.Repo;
using System;
using System.Linq;

namespace SCA.Repository.UoW
{
    public sealed class UnitofWork : IUnitofWork
    {
        private readonly PostgreDbContext _context;
        public UnitofWork(PostgreDbContext context)
        {
            _context = context ?? throw new ArgumentException("context is null");
        }

        public IGenericRepository<T> GetRepository<T>() where T : class, new()
        {
            return new GenericRepository<T>(_context);
        }
        public ServiceResult SaveChanges()
        {
            var transaction = _context.Database.BeginTransaction();
            try
            {
                foreach (var dbEntityEntry in _context.ChangeTracker.Entries<BaseEntities>().Where(x => x.State == EntityState.Modified).ToList())
                {
                    try
                    {
                        dbEntityEntry.Property<DateTime>("CreatedDate").IsModified = false;
                        dbEntityEntry.Property<int>("CreatedUserId").IsModified = false;
                    }
                    catch (Exception ex)
                    {
                    }
                }

                var affectedRow = _context.SaveChanges();
                transaction.Commit();
                return Result.ReturnAsSuccess(null, null, affectedRow);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
                return Result.ReturnAsFail(ex.ToString(),null);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return Result.ReturnAsFail(ex.ToString(), null);
            }
        }

    }
}
