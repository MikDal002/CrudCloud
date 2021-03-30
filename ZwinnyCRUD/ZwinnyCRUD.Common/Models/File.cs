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

        [Display(Name = "Nazwa Pliku")]
        public string Name { get; set; }

        [Display(Name = "Rozmiar (w bajtach)")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public long SizeinBytes { get; set; }

        [Display(Name = "Przesłane")]
        [DisplayFormat(DataFormatString = "{0:G}")]
        public DateTimeOffset Uploaded { get; set; }

        public int ProjectId { get; set; }

        public Project Project { get; set; }
    }
}
