namespace web_api_template_forts_controllers_efcore.Interfaces;


public interface IQueryDto
{
    public string? Title {get;set;}
    public string? Description {get;set;}
    public DateTime? BeforeDate {get;set;}
    public DateTime? AfterDate {get;set;}
    public IQueryable<IUserTask> BuildQuery(ITaskContext context);
}