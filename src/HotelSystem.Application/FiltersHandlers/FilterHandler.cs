using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Linq.Dynamic.Core;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

namespace HotelSystem.Application.FiltersHandler;
public static class FilterHandler
{
    public static IQueryable<T> GetFilterRequest<T>(DbSet<T> context, JObject jsonRequest) where T : class
    {
        string createExpression = string.Empty;
        string expression = string.Empty;

        var attributeList = jsonRequest.Properties()
        .Where(attr => attr.Any())
        .ToList();

        IQueryable<T> request = context;

        foreach (var attribute in attributeList)
        {
            string attributeName = attribute.Name;

            if (Regex.IsMatch(attribute.Name, @"\bMax\w*"))
            {
                createExpression = "t => t.{0} <= {1}";
                attributeName = "Price";
            }
            else if (Regex.IsMatch(attribute.Name, @"\bMin\w*"))
            {
                createExpression = "t => t.{0} >= {1}";
                attributeName = "Price";
            }
            else if (attribute.Value.Type == JTokenType.Float)
            {
                createExpression = "t => t.{0} == {1}";
            }
            else
                createExpression = "t => t.{0} == \"{1}\"";
            expression = string.Format(createExpression, attributeName, attribute.Value.ToString());

            request = request.Where(expression);
        }

        return request;
    }
}
