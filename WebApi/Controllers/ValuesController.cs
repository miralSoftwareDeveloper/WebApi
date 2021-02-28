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

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private readonly IPaymentGatewayService paymentGatewayService = null;
        
        public ValuesController(IPaymentGatewayService paymentGatewaySer)
        {
            this.paymentGatewayService = paymentGatewaySer;
        }
        
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values/
        [HttpPost()]
        public ActionResult post([FromBody]PaymentDTO payment)
        {
        
            if (ModelState.IsValid)
            {

                if (paymentGatewayService.PaymentGateway(payment))
                {
                    return StatusCode(200);
                }
                else
                {
                    //return StatusCode(400);
                     return BadRequest(ModelState);
                }
                
            }
            else
            {

                return StatusCode(400);
            }

        }

       


            // PUT api/values/5
            [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] PaymentDTO payment)
        {

            if (ModelState.IsValid)
            {

                if (paymentGatewayService.UpdatePaymentGateway(id,payment))
                {
                    return StatusCode(200);
                }
                else
                {
                    //return StatusCode(400);
                    return BadRequest(ModelState);
                }

            }
            else
            {

                return StatusCode(400);
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
