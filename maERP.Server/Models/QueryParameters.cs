#nullable disable

namespace maERP.Server.Models
{
    public class QueryParameters
	{
		private int _pageSize = 10;
		public int StartIndex { get; set; }

		public int PageNumber
        {
            get
            {
				return _pageSize;
            }
			set
			{
				_pageSize = value;
			}
        }
	}
}