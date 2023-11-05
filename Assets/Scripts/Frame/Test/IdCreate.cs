using System.Collections;
using System.Collections.Generic;

public static class IdCreate
{
    public static int count = 0;

    public static int Get()
    {
        return ++count;
    }
}
