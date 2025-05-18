

public class VariableSegment
{
    private Dictionary<string, Variable<bool>> booleanVariables = new Dictionary<string, Variable<bool>>();
    private Dictionary<string, Variable<int>> integerVariables = new Dictionary<string, Variable<int>>();
    private Dictionary<string, Variable<float>> floatVariables = new Dictionary<string, Variable<float>>();
    private Dictionary<string, Variable<string>> stringVariables = new Dictionary<string, Variable<string>>();

    public Dictionary<string, Variable<bool>> getBoolVariables()
    {
        return this.booleanVariables;
    }

    public Dictionary<string, Variable<int>> getIntVariables()
    {
        return this.integerVariables;
    }

    public Dictionary<string, Variable<float>> getFloatVariables()
    {
        return this.floatVariables;
    }

    public Dictionary<string, Variable<string>> getStringVariables()
    {
        return this.stringVariables;
    }
}