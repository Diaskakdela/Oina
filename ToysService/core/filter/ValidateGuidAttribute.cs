using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ToysService.core.filter;

public class ValidateGuidAttribute(string parameterName) : ActionFilterAttribute
{
    public string ParameterName { get; } = parameterName;

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ActionArguments.ContainsKey(ParameterName) &&
            context.ActionArguments[ParameterName] is string guidString &&
            !Guid.TryParse(guidString, out _))
        {
            context.Result = new BadRequestObjectResult($"The parameter '{ParameterName}' is not a valid GUID.");
            return;
        }

        base.OnActionExecuting(context);
    }
}