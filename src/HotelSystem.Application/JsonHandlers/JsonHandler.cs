using HotelSystem.Domain.Entities.Dtos.RequestDtos;
using Newtonsoft.Json.Linq;

namespace HotelSystem.Application.JsonHandlers;
public static class JsonHandler
{
    public static JObject ClearDefaultValues<T>(T request) where T : class, IRequest
    {
        JObject resultJson = (JObject)JToken.FromObject(request);

        resultJson.Properties()
        .Where(attr => attr.Value.ToString() == "def")
        .ToList()
        .ForEach(attr => attr.Remove());

        return resultJson;
    }
}