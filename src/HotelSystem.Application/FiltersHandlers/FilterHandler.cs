using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Linq.Dynamic.Core;

namespace HotelSystem.Application.FiltersHandler;
public static class FilterHandler
{
    public static IQueryable<T> GetFilterRequest<T>(DbSet<T> context, JObject jsonRequest) where T : class
    {
        var temp = jsonRequest.Properties()
        .Where(attr => attr.Any())
        .ToList();

        IQueryable<T> request = context;

        foreach (var attribute in temp)
        {
            string createExpression = "t => t.{0} == \"{1}\"";
            string expression = string.Format(createExpression, attribute.Name, attribute.Value.ToString());

            request = request.Where(expression);
        }

        return request;
    }
}
