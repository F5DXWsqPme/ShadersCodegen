namespace Generator;

public class EnumsDataProcessor
{
    public void AddElement(string name)
    {
        if (_currentEnum == null)
        {
            throw new Exception("Current enum is null");
        }

        var structField = new EnumElement(name);
        
        _currentEnum.Elements.Add(structField);
    }
    
    public void StartEnum(string name)
    {
        FinishCurrentEnumProcessing();

        _currentEnum = new EnumType(name);
    }

    private void FinishCurrentEnumProcessing()
    {
        if (_currentEnum != null)
        {
            _enums.Add(_currentEnum);
        }

        _currentEnum = null;
    }

    public string GenerateHlslEnums(string enumSplit, string baseTab, string tab)
    {
        FinishCurrentEnumProcessing();

        string result = String.Empty;

        foreach (var structureData in _enums)
        {
            result += GenerateEnumData(structureData, baseTab, tab);
            result += enumSplit;
        }

        return result;
    }

    private string GenerateEnumData(EnumType enumData, string baseTab, string tab)
    {
        string result = String.Empty;

        result += baseTab + "enum " + enumData.Name + "\n";
        result += baseTab + "{\n";

        foreach (var enumElement in enumData.Elements)
        {
            result += baseTab + tab + enumElement.Name + ",\n";
        }
        
        result += baseTab + "};";
        
        return result;
    }

    private class EnumElement
    {
        public EnumElement(string name)
        {
            Name = name;
        }
        
        public string Name { get; }
    }

    private class EnumType
    {
        public EnumType(string name)
        {
            Name = name;
        }

        public string Name { get; }
        public List<EnumElement> Elements { get; } = new();
    }

    private EnumType? _currentEnum;
    private readonly List<EnumType> _enums = new();
}
