namespace Rick_and_Morty.Application.Responses
{
    public class PagedResponse<T> : Response<T>
    {
        public int TotalRecords { get; set; }
        public int Pages { get; set; }
        public int NextPage { get; set; }

        public PagedResponse(T data, int totalRecords)
        {
            Data = data;
            TotalRecords = totalRecords;
            Succeeded = true;
            Message = null;
            Error = null;
        }

        public PagedResponse(T data, int totalRecords, int pages, int nextPage)
        {
            Data = data;
            TotalRecords = totalRecords;
            Pages = pages;
            NextPage = nextPage;
            Succeeded = true;
            Message = null;
            Error = null;
        }
    }
}
