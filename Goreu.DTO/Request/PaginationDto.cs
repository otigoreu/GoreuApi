namespace Goreu.Dto.Request
{
    public class PaginationDto
    {
        private readonly int maxRecordsPerPage = 50;
        public int Page { get; set; } = 0;

        private int recordsPerPage = 50;
        public int RecordsPerPage
        {
            get { return recordsPerPage; }
            set { recordsPerPage = value > maxRecordsPerPage ? maxRecordsPerPage : value; }
        }
    }
}
