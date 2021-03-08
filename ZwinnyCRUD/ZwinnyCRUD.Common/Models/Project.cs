using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ZwinnyCRUD.Common.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        
        [DataType(DataType.Date)]
        public DateTimeOffset CreationDate {get; set;}
    }
}
