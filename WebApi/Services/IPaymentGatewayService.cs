using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Repositories;
using WebApi.DTO;

namespace WebApi.Services
{
    

    public interface IPaymentGatewayService
    {
        bool PaymentGateway(PaymentDTO paymentDTO);
        bool UpdatePaymentGateway(int id, PaymentDTO paymentDTO);

    }

    //public interface IExpensivePaymentGateway
    //{
    //    void AddPaymentBet21To500(UnitOfWork unitOfWork);
    //}

    //public interface IPremiumPaymentGateway
    //{
    //    void AddPaymentGreaterThan500(UnitOfWork unitOfWork);
    //}

    

}
