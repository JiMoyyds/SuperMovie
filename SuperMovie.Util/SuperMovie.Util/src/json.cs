namespace SuperMovie.Util;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public class JsonHelper
{
    public static string Stringify(object o)
    {
        var converter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
        return JsonConvert.SerializeObject(o, converter);
    }

    public static T? Parse<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }
}