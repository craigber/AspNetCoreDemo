using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Cartoonalogue.Models
{
    public class Cartoon
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }


        [Display(Name="Studio")]
        public int StudioId { get; set; }

        [Display(Name="Network")]
        public int NetworkId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name="Date Debuted")]
        public DateTime WhenDebuted { get; set; }

        [Range(0,20)]
        public int Seasons { get; set; }

        public Studio Studio { get; set; }

        public Network Network { get; set; }

        public string Trivia { get; set; }
    }
}