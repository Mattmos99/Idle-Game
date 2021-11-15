using System.Collections.Generic;
using System.Linq;
using BreakInfinity;

public static class Methods 

{
    public static int Notation;
    public static string Notate(this BigDouble number )
    {
        switch(Notation)
        {
            case 0: //Standard
                return "Lol";
            case 1: //Sci
                return "science";
        }
        return "";
    }
    public static List<T> CreateList<T>(int capacity) => Enumerable.Repeat(default(T), capacity).ToList();

    public static void UpgradeCheck<T>(List<T> list, int length) where T : new()
    {
        try
        {

            if (list.Count == 0) list = new T[length].ToList();
            while (list.Count < length) list.Add(new T());

        }

        catch
        {

            list = new T[length].ToList();

        }
    }
}
