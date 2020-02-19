namespace MyCourse.Models.ViewModels
{
    public interface IPaginationInfo
    {
        int ResultsPerPage { get; }
        int TotalResults { get;}
        string Search { get; }
        int CurrentPage { get; }
        string OrderBy { get; }
        bool Ascending { get; }
    }
}