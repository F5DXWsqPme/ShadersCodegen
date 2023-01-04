using Antlr4.Runtime;
namespace Generator;

public class ParsedStream
{
    public ParsedStream(Stream inputStream)
    {
        AntlrInputStream inputAntlrStream = new AntlrInputStream(inputStream);
        BindingsGrammarLexer lexer = new BindingsGrammarLexer(inputAntlrStream);
        CommonTokenStream commonTokenStream = new CommonTokenStream(lexer);
        BindingsGrammarParser parser = new BindingsGrammarParser(commonTokenStream);
        var textContext = parser.text();
        _visitor.Visit(textContext);
    }

    public string GenerateHlslStructures(string structureSplit = "\n", string baseTab = "", string tab = "  ")
    {
        return _visitor.StructuresProcessor.GenerateHlslStructures(structureSplit, baseTab, tab);
    }
    
    public string GenerateHlslEnums(string enumSplit = "\n", string baseTab = "", string tab = "  ")
    {
        return _visitor.EnumsProcessor.GenerateHlslEnums(enumSplit, baseTab, tab);
    }
    
    public string GenerateHlslBindings(string bindingSplit = "\n", string baseTab = "")
    {
        return _visitor.BindingsProcessor.GenerateHlslBindings(bindingSplit, baseTab);
    }
    
    private readonly VisitorImplementation _visitor = new();
}