public class VariableSegment
{
    /// variables[tipo][nombre]
    public Dictionary<string, Dictionary<string, Variable>> variables { get; set; }

    public VariableSegment()
    {
        variables = new Dictionary<string, Dictionary<string, Variable>>();
        variables["int"] = new Dictionary<string, Variable>();
        variables["float"] = new Dictionary<string, Variable>();
        variables["string"] = new Dictionary<string, Variable>();
        variables["bool"] = new Dictionary<string, Variable>();
        variables["function"] = new Dictionary<string, Variable>();
    }
}
