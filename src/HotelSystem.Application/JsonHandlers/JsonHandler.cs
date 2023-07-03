using Newtonsoft.Json.Linq;

namespace HotelSystem.Application.JsonHandlers;
public static class JsonHandler
{
    public static string ClearDefaultValues(string jsonClass)
    {
        var resultJson = JObject.Parse(jsonClass);
        resultJson.Properties()
    .Where(attr => attr.Value.ToString() == "def")
    .ToList()
    .ForEach(attr => attr.Remove());
        return resultJson.ToString();
    }
}