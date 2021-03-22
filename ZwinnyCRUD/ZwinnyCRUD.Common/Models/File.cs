using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text;

namespace ZwinnyCRUD.Common.Models
{
    public class File
    {
        public int Id { get; set; }

        public string FilePath { get; set; }

        [Display(Name = "File Name")]
        public string Name { get; set; }

        [Display(Name = "Note")]
        public string Note { get; set; }

        [Display(Name = "Size (bytes)")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public long Size { get; set; }

        [Display(Name = "Uploaded")]
        [DisplayFormat(DataFormatString = "{0:G}")]
        public DateTime UploadDT { get; set; }

        public int ProjectId { get; set; }

        public virtual Project Project { get; set; }
    }
}
