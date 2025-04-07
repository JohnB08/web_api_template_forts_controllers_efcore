using Microsoft.AspNetCore.Mvc;
using web_api_template_forts_controllers_efcore.Interfaces;
using web_api_template_forts_controllers_efcore.Models;

namespace web_api_template_forts_controllers_efcore.Controllers;

/// <summary>
/// Her implementerer vi vår egen controller.
/// Vi automapper igjen url til controlleren lik navnet til controlleren,
/// som vi så var mulig i eksempel templaten vår.
/// dvs at RawUrl /task refererer til denne resursen. 
/// Legg merke til at vi kan ta inn referanser til context og andre ting via primærconstructoren, om vi skulle ønske det.
/// bl.a. hvis vi ønsker logging, via primærconstructoren til Controlleren vår.
/// Da er disse tilgjengelig i metodene nedenfor.
/// 
/// Det kan også være viktig å vite at disse Controllerene eksisterer i et Transient Scope.
/// Det vil si objektet eksisterer i øyeblikket appen vår matcher en request mot en mappet controller-sti
/// og da kan metodene våre konsumere elementer fra primærconstructoren direkte.
/// 
/// Vi kan tenke oss at app caller vår controller slik:
/// 
/// Det kommer en forespørsel mot GET /tasks/Complete:
/// return new TaskController().GetComplete() (ish);
/// 
/// 
/// </summary>
/// <param name="context"></param>
/// <param name="logger"></param>
[ApiController]
[Route("/[Controller]")]
public class TaskController(ITaskContext context, ILogger<TaskController> logger): ControllerBase
{
    /* Legg merke til at metodene våre også markerer hvilken http metode de skal matche, via Attributtene sine. */
    [HttpGet]
    /* Det kan være lurt å matche navnet på metoden i controlleren, med navnet på http metode + subroute. */
    public IActionResult Get([FromQuery] QueryDto queryDto) => Ok(queryDto.BuildQuery(context));

    [HttpGet("/complete")]
    /* Se navngivningen på denne metoden. */
    public IActionResult GetComplete() => Ok(context.GetCompleteTasks());

    [HttpGet("/pending")]
    public IActionResult GetPending() => Ok(context.GetPendingTasks());

    /* Vi kan ta inn elementer parsed fra RawUrl her også, som vi kunne med vår minimal-api struktur. */
    [HttpGet("{id}")]
    /* Legg og merke til metodeoverloadingen her, en controller basert api er en naturlig plass hvor overloading eksisterer.  */
    public IActionResult Get(int id) => Ok(context.GetTaskById(id));

    [HttpPatch("/complete/{id}")]
    public IActionResult CompleteId(int id) => Ok(context.CompleteTask(id));

    [HttpPost]
    public IActionResult Post([FromBody] TaskDto taskDto) => Ok(taskDto.InsertTask(context));

    [HttpDelete("{id}")]
    public IActionResult Delete(int id) => Ok(context.DeleteTask(id)); 

    [HttpGet("Log")]
    public IActionResult GetLog()
    {
        logger.LogInformation("I'm called!");
        return Ok();
    }
}