using System;

namespace Core.Tool.Entity
{
    public interface IModificationAudited
    {
        //string Id { get; set; }
        int? ModifyId { get; set; }
        string ModifyBy { get; set; }
        DateTime? ModifyTime { get; set; }
    }
}