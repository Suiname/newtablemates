using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NewTableMates.Models
{
    public class Appointment
    {
        public int ID { get; set; }
        public float Lat { get; set; }
        public float Lng { get; set; }
        [Required]
        public int MinAttendees { get; set; }
        public int? MaxAttendees { get; set; }
        [Required]
        public string AppointmentName { get; set; }
        public string RestaurantName { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime AppointmentDate { get; set; }

        public virtual Attendee[] Attendees { get; set; }
    }
}