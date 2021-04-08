using System;
using System.ComponentModel.DataAnnotations;
using ZwinnyCRUD.Common.Models;

namespace ZwinnyCRUD.Common.Dtos
{
    public class ProjectDto
    {
        public int? Id { get; set; }

        public DateTimeOffset? CreationDate { get; set; }

        [Required]
        [StringLength(31, ErrorMessage = "Tytuł jest zbyt długi.")]
        public string Title { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Opis jest zbyt długi.")]
        public string Description { get; set; }

        public static ProjectDto FromProject(Project proj)
        {
            return new ProjectDto()
            {
                Id = proj.Id,
                Title = proj.Title,
                Description = proj.Description,
                CreationDate = proj.CreationDate
            };
        }
    }
}
