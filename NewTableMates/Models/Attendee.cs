using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NewTableMates.Models
{
    public class Attendee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public string username { get; set; }
        public string email { get; set; }
    }
}