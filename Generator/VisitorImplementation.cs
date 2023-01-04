using Antlr4.Runtime.Misc;

namespace Generator;

public class VisitorImplementation : BindingsGrammarBaseVisitor<object>
{
    public override object VisitSrvId([NotNull] BindingsGrammarParser.SrvIdContext context)
    {
        BindingsProcessor.AddSrvBinding(context.type().GetText(), context.Id().GetText());
        return VisitChildren(context);
    }

    public override object VisitTexId([NotNull] BindingsGrammarParser.TexIdContext context)
    {
        BindingsProcessor.AddTexBinding(context.type().GetText(), context.Id().GetText());
        return VisitChildren(context);
    }

    public override object VisitUavId([NotNull] BindingsGrammarParser.UavIdContext context)
    {
        BindingsProcessor.AddUavBinding(context.type().GetText(), context.Id().GetText());
        return VisitChildren(context);
    }
    
    public override object VisitStruct([NotNull] BindingsGrammarParser.StructContext context)
    {
        StructuresProcessor.StartStruct(context.type().GetText());
        return VisitChildren(context);
    }
    
    public override object VisitField([NotNull] BindingsGrammarParser.FieldContext context)
    {
        StructuresProcessor.AddField(context.type().GetText(), context.Id().GetText());
        return VisitChildren(context);
    }
    
    public override object VisitEnum([NotNull] BindingsGrammarParser.EnumContext context)
    {
        EnumsProcessor.StartEnum(context.type().GetText());
        return VisitChildren(context);
    }
    
    public override object VisitEnumElement([NotNull] BindingsGrammarParser.EnumElementContext context)
    {
        EnumsProcessor.AddElement(context.Id().GetText());
        return VisitChildren(context);
    }

    public StructuresDataProcessor StructuresProcessor { get; } = new();
    public EnumsDataProcessor EnumsProcessor { get; } = new();
    public BindingsDataProcessor BindingsProcessor { get; } = new();
}