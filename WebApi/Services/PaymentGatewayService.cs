using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using WebApi.Repositories;
using WebApi.Services;
using WebApi.Models;
using WebApi.DTO;
using AutoMapper;
using System.Net.Http;
using System.Net;

namespace WebApi.Services
{
    public class PaymentGatewayService : IPaymentGatewayService
    {
        private readonly UnitOfWork unitOfWork;
        private readonly PaymentDBContext paymentDBContext;
        private readonly Payment payment;
        private readonly  PaymentStatus paymentStatus;

        public PaymentGatewayService()
        {
            this.paymentDBContext = new PaymentDBContext();
            this.unitOfWork = new UnitOfWork(paymentDBContext);
            payment = new Payment();
            paymentStatus = new PaymentStatus();
        }


        public bool PaymentGateway(PaymentDTO paymentDTO)
        {
            bool insertSuccessfull = false;

            if (paymentDTO == null)
                return insertSuccessfull;

            
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<PaymentDTO, Payment>();
            });


            IMapper iMapper = config.CreateMapper();
            iMapper.Map(paymentDTO, payment);


            unitOfWork.PaymentRepo.InsertPayment(payment);
            paymentStatus.StatusName = Helper.ConvertStringToEnum(paymentDTO.Status);
            unitOfWork.PaymentStatusRepo.InsertPaymentStatus(paymentStatus);
        
            if (payment.Amount <= 20)
            {

                insertSuccessfull = CheapPaymentService(unitOfWork);
            }
            else if (payment.Amount > 20 && payment.Amount < 500)
            {
                insertSuccessfull = PremiumPaymentService(unitOfWork);
            }
            else
            {
                insertSuccessfull = PremiumPaymentService(unitOfWork);
            }

            return insertSuccessfull;
        }

        public bool CheapPaymentService(UnitOfWork unitOfWork)
        {
            return unitOfWork.Save() > 0;

        }
        public  bool PremiumPaymentService(UnitOfWork unitOfWork)
        {
            return unitOfWork.Save() > 0;


        }
        public bool ExpensivePaymentService(UnitOfWork unitOfWork)
        {
            return unitOfWork.Save() > 0;


        }


        public bool UpdatePaymentGateway(int uid,PaymentDTO paymentDTO)
        {
            bool isUpdated = false;


            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<PaymentDTO, Payment>();
            });


            IMapper iMapper = config.CreateMapper();
            iMapper.Map(paymentDTO, payment);

            payment.ID = uid;
            unitOfWork.PaymentRepo.UpdatePayment(payment);
            paymentStatus.PaymentId = uid;
            paymentStatus.StatusName = Helper.ConvertStringToEnum(paymentDTO.Status);
            unitOfWork.PaymentStatusRepo.UpdatePaymentStatus(payment.ID,paymentStatus.StatusName);
            if (unitOfWork.Save() > 0)
            {
                isUpdated = true;
            }

            return isUpdated;

        }
    }
}
