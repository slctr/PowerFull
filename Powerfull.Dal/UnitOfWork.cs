using System;
using System.Threading.Tasks;
using NHibernate;

namespace Powerfull.Dal
{
    public class UnitOfWork : IUnitOfWork
    {
        public ITransaction Context { get; private set; }


        public UnitOfWork(ITransaction context)
        {
            this.Context = context;
        }


        public void Commit()
        {
            try
            {
                this.Context.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception("Error. Database doesn't save the data.", ex);
            }
        }

        public async Task CommitAsync()
        {
            try
            {
                await this.Context.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error. Database doesn't save the data.", ex);
            }
        }

        public void Dispose()
        {
            if (this.Context == null)
            {
                throw new NullReferenceException(nameof(this.Context));
            }

            this.Context.Dispose();
            this.Context = null;
        }
    }
}
