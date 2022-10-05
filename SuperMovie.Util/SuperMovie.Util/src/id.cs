namespace SuperMovie.Util;

using static pilipala.util.id.palaflake;

public enum ActionType
{
    Cinema = 0,
    Film = 1,
    Order = 2,
    Schedule = 3,
}

public class IdGenerator
{
    private readonly Generator _gen;

    public IdGenerator(ActionType type)
    {
        _gen = new Generator((byte)type, 2022);
    }

    public long Next() => _gen.Next();
}

/*How to use:
 
using SuperMovie.Util;
var gen = new IdGenerator(ActionType.Schedule);
Console.WriteLine(gen.Next());
*/