using System;

public class f3_p3
{
public static void suma(int a, int b, int c)
{

int sum = (int)(a + b + c);
Console.WriteLine(sum);
}
public static void Main(string[] args)
{
int a = (int)(1);
int b = (int)(2);
int c = (int)(3);
int d = (int)(5);
while(a < d)
{
suma (a, b, c);
a = (int)(a + 1);
}
}
}