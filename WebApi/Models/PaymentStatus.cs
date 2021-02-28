using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class PaymentStatus
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [ForeignKey("Payment")]
        public int PaymentId { get; set; }

        public Status StatusName { get; set; }


        public virtual Payment Payment { get; set; }

    }

    public enum Status
    {
        Processed,
        Failed,
        Pending
    }
}
