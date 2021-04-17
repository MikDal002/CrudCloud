using System;
using ZwinnyCRUD.Common.Models;

namespace ZwinnyCRUD.Common.Dtos
{
    public class FileDto
    {
        public int? Id { get; set; }

        public string FilePath { get; set; }

        public string Name { get; set; }

        public long SizeinBytes { get; set; }

        public DateTimeOffset Uploaded { get; set; }

        public int ProjectId { get; set; }

        public static FileDto FromFile(File file)
        {
            return new FileDto()
            {
                Id = file.Id,
                FilePath = file.FilePath,
                Name = file.Name,
                SizeinBytes = file.SizeinBytes,
                Uploaded = file.Uploaded,
                ProjectId = file.ProjectId
            };
        }
    }
}
