using web_api_template_forts_controllers_efcore.Interfaces;

namespace web_api_template_forts_controllers_efcore.Models;

public class QueryDto : IQueryDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime? BeforeDate { get; set; }
    public DateTime? AfterDate { get; set; }

    public IQueryable<IUserTask> BuildQuery(ITaskContext context)
    {
        var query = context.GetAllTasks().AsQueryable();

        if (!string.IsNullOrWhiteSpace(Title)) query = query.Where(task => task.Title.Contains(Title, StringComparison.InvariantCultureIgnoreCase));

        if (!string.IsNullOrWhiteSpace(Description)) query = query.Where(task => task.Description.Contains(Description, StringComparison.InvariantCultureIgnoreCase));

        if (BeforeDate.HasValue) query = query.Where(task => task.DueDate < BeforeDate);

        if (AfterDate.HasValue) query = query.Where(task => task.DueDate > AfterDate);

        return query;
    }
}
