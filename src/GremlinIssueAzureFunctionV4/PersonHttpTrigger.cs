using System.Threading.Tasks;
using GremlinIssueAzureFunction.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace GremlinIssueAzureFunctionV4;

internal  class PersonHttpTrigger
{
    private readonly IPersonService _personService;

    public PersonHttpTrigger(IPersonService personService)
    {
        _personService = personService;
    }
     
    [FunctionName(nameof(AddPerson))]
    public  async Task<IActionResult> AddPerson(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
    {
        await _personService.Add();
        return new OkResult();
    }
}
