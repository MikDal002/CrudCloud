using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ZwinnyCRUD.Common.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        [StringLength(31, ErrorMessage = "Tytuł jest zbyt długi.")] 
        public string Title { get; set; }
        
        [Required]
        [StringLength(255, ErrorMessage = "Opis jest zbyt długi.")] 
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTimeOffset CreationDate {get; set;}
    }
}