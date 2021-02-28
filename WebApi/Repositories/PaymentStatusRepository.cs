using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using WebApi.Models;


namespace WebApi.Repositories
{
    public class PaymentStatusRepository
    {
        
        private readonly PaymentDBContext Context;

        public PaymentStatusRepository(PaymentDBContext paymentDBContext)
        {
            this.Context = paymentDBContext;
        }

        public IEnumerable<PaymentStatus> GetPaymentStatus()
        {
            return Context.PaymentStatus.ToList();
        }


        public PaymentStatus GetPaymentStatusById(int id)
        {
            return Context.PaymentStatus.Find(id);
        }

        public void InsertPaymentStatus(PaymentStatus paymentStatus)
        {
            Context.PaymentStatus.Add(paymentStatus);
        }

        public void DeletePaymentStatus(int paymentStatusID)
        {
            PaymentStatus paymentStatus = Context.PaymentStatus.Find(paymentStatusID);
            Context.PaymentStatus.Remove(paymentStatus);
        }

        public void UpdatePaymentStatus(int paymentId, Status status)
        {
            PaymentStatus getPaymentStatusObj = Context.PaymentStatus.Where(c => c.PaymentId == paymentId).SingleOrDefault();
            getPaymentStatusObj.StatusName = status;
            Context.Entry(getPaymentStatusObj).State = EntityState.Modified;
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
