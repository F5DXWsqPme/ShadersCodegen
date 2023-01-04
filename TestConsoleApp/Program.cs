using Generator;

using var fileStream = File.OpenRead("test.txt");
var parsedFile = new ParsedStream(fileStream);
Console.Write(parsedFile.GenerateHlslBindings());
Console.Write(parsedFile.GenerateHlslEnums());
Console.Write(parsedFile.GenerateHlslStructures());