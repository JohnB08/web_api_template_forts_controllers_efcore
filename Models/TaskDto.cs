using System.Text.Json.Serialization;
using web_api_template_forts_controllers_efcore.Interfaces;

namespace web_api_template_forts_controllers_efcore.Models;

public class TaskDto : ITaskDto
{
    [JsonPropertyName("title")]
    public required string Title { get; set; }
    [JsonPropertyName("description")]
    public required string Description { get; set; }
    [JsonPropertyName("DueDate")]
    public DateTime DueDate { get; set; }

    public IUserTask InsertTask(ITaskContext context)
    {
        return context.AddTask(Title, Description, DueDate);
    }
}
