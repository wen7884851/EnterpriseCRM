using System;

namespace Core.Tool.Entity
{
    public interface ICreationAudited
    {
        //int Id { get; set; }
        int? CreateId { get; set; }
        string CreateBy { get; set; }
        DateTime? CreateTime { get; set; }
    }
}