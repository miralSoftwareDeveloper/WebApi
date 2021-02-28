using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class PaymentRepository
    {
        private  PaymentDBContext Context;

        public PaymentRepository(PaymentDBContext paymentDBContext)
        {
            this.Context = paymentDBContext;
        }

        public IEnumerable<Payment> GetPayments()
        {
            return Context.Payment.ToList();
        }


        public Payment GetPaymentById(int id)
        {
            return Context.Payment.Find(id);
        }

        public void InsertPayment(Payment payment)
        {
            Context.Payment.Add(payment);
        }

        public void DeletePayment(int paymentID)
        {
            Payment payment = Context.Payment.Find(paymentID);
            Context.Payment.Remove(payment);
        }

        public void UpdatePayment(Payment payment)
        {
            Context.Entry(payment).State = EntityState.Modified;
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
