namespace SearchService.Helpers.Requests;

public class SearchParameters
{
    public string SearchText { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 4;
    public string NftSeller { get; set; }
    public string NftArtist { get; set; }
    public string NftCollection { get; set; }
    public string NftOrderBy { get; set; }
    public string NftFilterBy { get; set; }
}
