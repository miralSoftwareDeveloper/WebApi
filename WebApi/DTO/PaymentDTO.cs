using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebApi.DTO
{
    public class PaymentDTO
    {
        [Required(ErrorMessage = "Credit Card Number is mandatory")]

        [CreditCardValidation]
        public string CreditCardNumber  { get; set; }

        [Required(ErrorMessage = "Card Holder is mandatory")]
        public string CardHolder { get; set; }

        [Required(ErrorMessage = "Expiration Date is mandatory")]
        
        [DateGreaterThanOrEqualToToday]
        public  DateTime ExpirationDate { get; set; }

        [StringLength(3)]
        public string   SecurityCode { get; set; }

        [Required(ErrorMessage = "Amount is mandatory")]
        [DataType(DataType.Currency)]
        public decimal  Amount { get; set; }

        [Required(ErrorMessage = "Status is mandatory")]
        public string Status { get; set; }
    }

    public class DateGreaterThanOrEqualToToday : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return "Date value should be a future date";
        }

        protected override ValidationResult IsValid(object objValue,
                                                       ValidationContext validationContext)
        {
            var dateValue = objValue as DateTime? ?? new DateTime();

            if (dateValue.Date < DateTime.Now.Date)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            return ValidationResult.Success;
        }
    }


    public class CreditCardValidation : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return "Invaild Credit Card Number";
        }

        protected override ValidationResult IsValid(object objValue,
                                                       ValidationContext validationContext)
        {
            var stringCreditCard = objValue as string;

            Regex expression = new Regex(@"^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$");

            if (expression.IsMatch(stringCreditCard))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }



        }


        public bool ValidateCreditCard(string creditCardNumber)
        {
            //Build your Regular Expression
            Regex expression = new Regex(@"^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$");

            //Return if it was a match or not
            return expression.IsMatch(creditCardNumber);
        }

    }

}
