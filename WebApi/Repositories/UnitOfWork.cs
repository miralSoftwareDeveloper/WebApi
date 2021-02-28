using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using WebApi.Models;
using WebApi.Repositories;
using System.Data.Entity.Validation;

namespace WebApi.Repositories
{
    public class UnitOfWork
    {
        private PaymentDBContext DBContext;
        private PaymentRepository paymentRepository;
        private PaymentStatusRepository paymentStatusRepository;

        public UnitOfWork(PaymentDBContext dBContext)
        {
            this.DBContext = dBContext;
        }

        public PaymentRepository PaymentRepo
        {
            get
            {
                if (paymentRepository == null)
                {

                    paymentRepository = new PaymentRepository(DBContext);
                }
                return paymentRepository;
            }
        }


        public PaymentStatusRepository PaymentStatusRepo
        {
            get
            {
                if (paymentStatusRepository == null)
                {

                    paymentStatusRepository = new PaymentStatusRepository(DBContext);
                }
                return paymentStatusRepository;
            }
        }

        public int Save()
        {
            int isSave = 0;

            try
            {
                isSave = DBContext.SaveChanges();
                return isSave;
            }
            catch (DbEntityValidationException DBContext)
            {
                foreach (var validationErrors in DBContext.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }

                return isSave;
            }
        }


    }
}
