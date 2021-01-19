namespace TheatreCMS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;

    public class RentalSurvey : Survey
    { 
        public int RecommendRentingRating { get; set; }
        public int OverallExperienceRating { get; set; }
        //The next line of code is to define the 1-1 relationship between RentalRequest and RentalSurvey//
        [Required]
        public virtual RentalRequest RentalRequest { get; set; }

    }
}