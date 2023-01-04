using System.Text;
using Generator;

namespace Generator_Tests;

public class Tests
{
    private ParsedStream CreateParsedStream(string input)
    {
        var inputStream = new MemoryStream(Encoding.ASCII.GetBytes(input));
        return new ParsedStream(inputStream);
    }

    [Test]
    public void ParsedStreamShouldGenerateHlslBindings()
    {
        var parsedStream = CreateParsedStream(
            "TEX<float4> noiseTexture1;\n" +
            "UAV<int> dstBuffer;\n" +
            "SRV<uint2> b1;\n");
        var output = parsedStream.GenerateHlslBindings("", "");
        
        Assert.AreEqual(
            "Texture2D<float4> noiseTexture1: register(t0)" +
            "RWBuffer<int> dstBuffer: register(u0)" +
            "Buffer<uint2> b1: register(t1)",
            output);
    }
    
    [Test]
    public void ParsedStreamShouldGenerateHlslStructures()
    {
        var parsedStream = CreateParsedStream(
            "struct X {\n" +
            "int f;\n" +
            "};\n");
        var output = parsedStream.GenerateHlslStructures("", "", "");
        
        Assert.AreEqual(
            "struct X\n" +
            "{\n" +
            "int f;\n" +
            "};",
            output);
    }
    
    [Test]
    public void ParsedStreamShouldGenerateHlslEnums()
    {
        var parsedStream = CreateParsedStream(
            "enum E {\n" +
            "e1,\n" +
            "};\n");
        var output = parsedStream.GenerateHlslEnums("", "", "");
        
        Assert.AreEqual(
            "enum E\n" +
            "{\n" +
            "e1,\n" +
            "};",
            output);
    }
}