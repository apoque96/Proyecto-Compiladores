public class Variable<T>
{
    private string name;
    private T value;
    private int scope;

    public Variable(string name, T value, int scope)
    {
        setName(name);
        setValue(value);
        setScope(scope);
    }

    public string getName()
    {
        return name;
    }
    public void setName(string name)
    {
        this.name = name;
    }
    public T getValue()
    {
        return value;
    }
    public void setValue(T value)
    {
        this.value = value;
    }
    public int getScope()
    {
        return scope;
    }
    public void setScope(int scope)
    {
        this.scope = scope;
    }
}