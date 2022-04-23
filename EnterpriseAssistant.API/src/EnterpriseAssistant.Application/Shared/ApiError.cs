namespace EnterpriseAssistant.Application.Shared;

public class ApiError
{
    public string Title { get; set; }
    public int Status { get; set; }
    public IEnumerable<object> Errors { get; set; }
}