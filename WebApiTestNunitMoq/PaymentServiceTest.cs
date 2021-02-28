using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Web.Http;
using Moq;
using WebApi.Repositories;
using WebApi.Controllers;
using WebApi.Models;
using WebApi.Services;
using WebApi.DTO;
using System.Net;

namespace WebApiTestNunitMoq
{
    [TestFixture]
    public class PaymentServiceTest
    {
       
        private PaymentGatewayService paymentGatewayService;
        private PaymentDBContext paymentDB;
        private PaymentRepository paymentRepository;
       

        [SetUp]
        public void SetUp()
        {
            paymentDB = new PaymentDBContext();
            paymentRepository = new PaymentRepository(paymentDB);
            paymentGatewayService = new PaymentGatewayService();
            
        }

        //[Test]
        public void AddPaymentTest()
        {
            // Arrange

            PaymentDTO paymentDTO = new PaymentDTO
            {
                CreditCardNumber = "4024007103939509",
                CardHolder = "Miral",
                Amount = 90,
                SecurityCode = "123",
                Status = "Processed",
                ExpirationDate = new DateTime(2021, 2, 27)

            };

            

            // Act  
            bool responseTrue = paymentGatewayService.PaymentGateway(paymentDTO);
            // Assert  
            Assert.IsTrue(responseTrue);
        }

        //[Test]
        public void AddPaymentForNullTest()
        {

            // Arrange
            PaymentDTO paymentDTO = null;
            // Act  
            bool responseFalse = paymentGatewayService.PaymentGateway(paymentDTO);
            // Assert  
            Assert.IsFalse(responseFalse);
        }
        
        [Test]
        public void AddPaymentWithMoq()
        {

            // Arrange

            PaymentDTO paymentDTO = new PaymentDTO
            {
                CreditCardNumber = "4024007103939509",
                CardHolder = "Miral",
                Amount = 90,
                SecurityCode = "123",
                Status = "Processed",
                ExpirationDate = DateTime.Now.AddDays(1),

            };

            

            Mock<IPaymentGatewayService> mockPaymentGatewayService = new Mock<IPaymentGatewayService>();

            mockPaymentGatewayService.Setup(t => t.PaymentGateway(It.IsAny<PaymentDTO>())).Returns(true);

            //Act

            var controller = new ValuesController(mockPaymentGatewayService.Object);

            var response = controller.post(paymentDTO);

            
            //Assert
            Assert.AreEqual(200, ((Microsoft.AspNetCore.Mvc.StatusCodeResult)response).StatusCode);
        }
    }
}
