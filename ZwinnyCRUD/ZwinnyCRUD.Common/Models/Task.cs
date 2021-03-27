using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ZwinnyCRUD.Common.Models
{
    public class Task
    {

        public Task()
        {
            IsDone = false;
        }

        public int Id { get; set; }

        [Required]
        [StringLength(31, ErrorMessage = "Tytuł jest zbyt długi.")]
        public string Title { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Opis jest zbyt długi.")]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTimeOffset CreationDate { get; }

        public bool IsDone { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }

    }
}
