grammar Gramatica;

prog:    pg EOF ;

pg: fd 'BeginProgram' sl 
;

sl: s sl
|
;

s: d 
| ce 
| io 
| fc 
;

//ese 'int' contara como Keyword? - COMENTARIO// 
d : 'int' ID '=' e ';' 
| 'float' ID '=' e ';' 
| 'string' ID '=' STRING ';'
| 'bool' ID '=' ds ';'
;

e: t ep
;

ep: '+' t ep
| '-' t ep
| // empty
;

t: f tp
;

tp: '*' f tp
| '/' f tp
| // empty
;

f: '(' e ')'
| INT
| FLOAT
| ID
;

ds: BOOL
| e dsp e
| STRING '==' STRING
| STRING '!=' STRING
;

dsp: '>'
| '<'
| '>='
| '<='
| '=='
| '!='
;

io: 'Read' '(' ID ')' ';'
| 'Write' '(' ID ')' ';'
;

ce: ifd
| 'while' '(' ds ')' '{' sl '}'
;

ifd: 'if' '(' ds ')' '{' sl '}' ed
;

ed: 'else' edp
| // empty
;

edp: '{' sl '}'
| ifd
;

fd: 'fn' ID '(' pl ')' rt fd
| // empty
;

rt: '->' ty '{' sl 'return' typ ';' '}'
| '{' sl '}'
;

pl: ty ID plp
|
;

plp: ',' ty ID plp
|
;

ty: 'int'
| 'float'
| 'string'
| 'bool'
;

typ: INT
| FLOAT
| STRING
| BOOL
| ID
;

fc: ID '(' p ')' ';'
;

p: typ pp
| // empty
;

pp: ',' typ pp
| // empty
;

BOOL        
: 'true'
| 'false' 
;

ID          : [a-zA-Z][a-zA-Z1-9_]* ;
INT         : [0-9]+ ;
FLOAT       : 'f'[0-9]+'.'[0-9]+ ;
STRING : '"' (~["\\] | '\\' .)* '"' ;

WS          : [ \t\r\n]+ -> skip ;