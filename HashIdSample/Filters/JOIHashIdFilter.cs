using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using HashidsNet;
using HashIdSample.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HashIdSample.Filters
{
    /// <summary>
    /// Converte HashId em Long antes ou depois de executar as actions
    /// </summary>
    public class JOIHashIdFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            foreach (var routeDataValue in context.RouteData.Values)
                if (routeDataValue.Key.EndsWith("id", StringComparison.OrdinalIgnoreCase))
                    context.ActionArguments[routeDataValue.Key] = HashIdsUtil.Decode(routeDataValue.Value as string);
            
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            //var viewResult = context.Result as Microsoft.AspNetCore.Mvc.ViewResult;

            //foreach (PropertyInfo prop in new List<PropertyInfo>((viewResult.Model.GetType()).GetProperties()))
            //    if (prop.Name.Equals("id", StringComparison.CurrentCultureIgnoreCase))
            //        prop.SetValue(viewResult.Model, HashIdsUtil.Encode(prop.GetValue(viewResult.Model, null)));

            base.OnActionExecuted(context);


        }
    }
}
