namespace web_api_template_forts_controllers_efcore.Interfaces;


public interface ITaskDto
{
    public string Title {get;set;}
    public string Description {get;set;}
    public DateTime DueDate {get;set;}
    public IUserTask InsertTask(ITaskContext context);
}