grammar BindingsGrammar;

text : statement*;

statement : (binding | struct | enum);

binding : (srvId | uavId | texId) ';';
srvId : 'SRV<' type '>' Id;
texId : 'TEX<' type '>' Id;
uavId : 'UAV<' type '>' Id;

enum : 'enum' type '{' enumElements '}' ';';
enumElements : enumElement*;
enumElement : (Id ',' | Id '=' enumValue ',');
enumValue : IntNumber;

struct : 'struct' type '{' fields '}' ';';
fields : field*;
field : type Id ';';

type : Id;
FloatNumber : ('+' | '-')? Digit+ ('.' Digit* 'f'?)?;
IntNumber : ('+'? | '-'?) Digit+;
Id : Nondigit (Digit | Nondigit)*;
WhiteSpace : [ \t\r\n]+ -> skip;
fragment Nondigit : [a-zA-Z_];
fragment Digit : [0-9];
