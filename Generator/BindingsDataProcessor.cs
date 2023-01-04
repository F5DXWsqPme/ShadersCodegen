namespace Generator;

public class BindingsDataProcessor
{
    public void AddSrvBinding(string type, string name)
    {
        _bindings.Add(new SrvBinding(name, type));
    }
    
    public void AddUavBinding(string type, string name)
    {
        _bindings.Add(new UavBinding(name, type));
    }
    
    public void AddTexBinding(string type, string name)
    {
        _bindings.Add(new TexBinding(name, type));
    }

    private class Counters
    {
        public int UavCounter{ get; set; } = 0;
        public int SrvCounter{ get; set; } = 0;
    }
    
    public string GenerateHlslBindings(string bindingSplit, string baseTab)
    {
        Counters counters = new();
        string result = String.Empty;

        foreach (var bind in _bindings)
        {
            result += baseTab + bind.GetBindingText(counters);
            result += bindingSplit;
        }

        return result;
    }
    
    private abstract class Binding
    {
        public Binding(string name)
        {
            Name = name;
        }

        public abstract string GetBindingText(Counters counters);
        
        public string Name { get; }
    }

    // t – for shader resource views (SRV)
    // s – for samplers
    // u – for unordered access views (UAV)
    // b – for constant buffer views (CBV)
    
    private class SrvBinding : Binding
    {
        public SrvBinding(string name, string type) : base(name)
        {
            Type = type;
        }

        public override string GetBindingText(Counters counters)
        {
            return "Buffer<" + Type + "> " + Name + ": register(t" + counters.SrvCounter++ + ")";
        }

        private string Type { get; }
    }

    private class TexBinding : Binding
    {
        public TexBinding(string name, string type) : base(name)
        {
            Type = type;
        }

        public override string GetBindingText(Counters counters)
        {
            return "Texture2D<" + Type + "> " + Name + ": register(t" + counters.SrvCounter++ + ")";
        }

        private string Type { get; }
    }
    
    private class UavBinding : Binding
    {
        public UavBinding(string name, string type) : base(name)
        {
            Type = type;
        }

        public override string GetBindingText(Counters counters)
        {
            return "RWBuffer<" + Type + "> " + Name + ": register(u" + counters.UavCounter++ + ")";
        }

        private string Type { get; } 
    }

    private List<Binding> _bindings = new();
}