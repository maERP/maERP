#nullable disable

namespace maERP.Server.ApiExceptions
{
	public class NotFoundException : ApplicationException
	{
        public NotFoundException(string name, object key)
            : base($"{name} ({key} was not found!")
        {

        }
	}
}