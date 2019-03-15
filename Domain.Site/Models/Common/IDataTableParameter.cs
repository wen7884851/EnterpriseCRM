namespace Domain.Site.Models
{
    public interface IDataTableParameter
    {
        int PageIndex { get; set; }

        int PageSize { get; set; }

        string OrderBy { get; set; }

        bool Asc { get; set; }
    }
}
