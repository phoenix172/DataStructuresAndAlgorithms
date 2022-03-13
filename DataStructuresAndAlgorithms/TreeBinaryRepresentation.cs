namespace ConsoleApp1;

public class TreeBinaryRepresentation
{
    static string BinaryRepresentation(int n, int? k = null)
    {
        int m = (int)Math.Floor(Math.Log(n, 2)) + 1;
        k ??= m;
        string str = Convert.ToString(n, 2);
        int diff = k.Value - m;

        if (diff > 0) return str.PadLeft(k.Value, '0');
        else if (diff == 0) return str;
        else return str.Substring(-diff);
    }

    public void Run()
    {
        Console.Write("n:");
        int n = 32; //int.Parse(Console.ReadLine());

        Console.WriteLine("h:");
        int h = 4; //int.Parse(Console.ReadLine());


        for (int i = (int)Math.Pow(2, h); i < n; i++)
        {
            Console.WriteLine(BinaryRepresentation(i, h + 1) + " = " + i);
        }
    }
}