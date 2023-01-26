using AsseccoBSWeb.Services.Abstraction;

namespace AsseccoBSWeb.Services
{
    public class PageService : IPageService
    {
        private List<string>[] _allValues { get; set; }
        public int numberOfPages { get; set; } = 0;

        private int _itemsPerPage = 20;

        public IEnumerable<string> GetPage(int page)
        {
            if (numberOfPages == 0)
                return null;
            return _allValues[page - 1];
        }

        public void AssignList(List<string> values)
        {
            if (values.Count == 0)
                return;
            numberOfPages = (int)Math.Ceiling((double)values.Count / _itemsPerPage);
            _allValues = new List<string>[numberOfPages];

            foreach (var value in values.Select((text, index) => (text, index)))
            {
                int pageIndex = value.index / _itemsPerPage;
                if (_allValues[pageIndex] == null)
                {
                    _allValues[pageIndex] = new List<string>();
                }
                _allValues[pageIndex].Add(value.text);
            }
        }
    }
}
