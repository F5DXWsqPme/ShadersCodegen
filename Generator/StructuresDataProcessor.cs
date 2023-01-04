namespace Generator;

public class StructuresDataProcessor
{
    public void AddField(string type, string name)
    {
        if (_currentStructure == null)
        {
            throw new Exception("Current struct is null");
        }

        var structField = new StructureField(type, name);
        
        _currentStructure.Fields.Add(structField);
    }
    
    public void StartStruct(string name)
    {
        FinishCurrentStructureProcessing();

        _currentStructure = new StructureType(name);
    }

    private void FinishCurrentStructureProcessing()
    {
        if (_currentStructure != null)
        {
            _structures.Add(_currentStructure);
        }

        _currentStructure = null;
    }

    public string GenerateHlslStructures(string structureSplit, string baseTab, string tab)
    {
        FinishCurrentStructureProcessing();

        string result = String.Empty;
        
        foreach (var structureData in _structures)
        {
            result += GenerateStructureData(structureData, baseTab, tab);
            result += structureSplit;
        }

        return result;
    }

    private string GenerateStructureData(StructureType structureData, string baseTab, string tab)
    {
        string result = String.Empty;

        result += baseTab + "struct " + structureData.Name + "\n";
        result += baseTab + "{\n";

        foreach (var field in structureData.Fields)
        {
            result += baseTab + tab + field.Type + " " + field.Name + ";\n";
        }
        
        result += baseTab + "};";
        
        return result;
    }

    private class StructureField
    {
        public StructureField(string type, string name)
        {
            Type = type;
            Name = name;
        }

        public string Type { get; }
        
        public string Name { get; }
    }

    private class StructureType
    {
        public StructureType(string name)
        {
            Name = name;
        }

        public string Name { get; }
        public List<StructureField> Fields { get; } = new();
    }

    private StructureType? _currentStructure;
    private readonly List<StructureType> _structures = new();
}
