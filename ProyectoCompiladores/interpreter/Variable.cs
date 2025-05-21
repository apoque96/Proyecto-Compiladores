public class Variable
{
    public string name {get; set; }
    public Type type { get; set; }

    public Variable(string name, Type type)
    {
        this.name = name;
        this.type = type;
    }
}