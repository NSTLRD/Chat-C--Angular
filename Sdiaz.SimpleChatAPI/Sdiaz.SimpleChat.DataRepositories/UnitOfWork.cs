﻿using Sdiaz.SimpleChat.Core.Repository_Interfaces;
using Sdiaz.SimpleChat.DataRepositories.Context;
using Sdiaz.SimpleChat.DataRepositories.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sdiaz.SimpleChat.DataRepositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SimpleChatDbContext simpleChatDbContext;



        public UnitOfWork(SimpleChatDbContext simpleChatDbContext)
        {
            this.simpleChatDbContext = simpleChatDbContext;
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            return new RepositoryBase<TEntity>(this.simpleChatDbContext);
        }

        public void SaveChanges()
        {
            //this.RunBeforeSave(this.dbContextScope);
            this.simpleChatDbContext.SaveChanges();
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            //this.RunBeforeSave(this.dbContextScope);
            await this.simpleChatDbContext.SaveChangesAsync(cancellationToken);
        }

        //protected virtual void RunBeforeSave(IDbContextScope currentDbContextScope)
        //{
        //}

        private bool disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.simpleChatDbContext.Dispose();
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            this.Dispose(true);
        }
    }
}
