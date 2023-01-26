namespace AsseccoBSWeb.Services.Abstraction
{
    public interface IPageService
    {
        int numberOfPages { get; set; }

        void AssignList(List<string> values);
        IEnumerable<string> GetPage(int page);
    }
}