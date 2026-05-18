using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace VM_Stack
{
    public class Token
    {
        public enum TokenType
        {
            PUSH,
            POP,

            STACKW,
            STACKR,
            LOAD,
            GOTO,

            STORE,
            PRINT,

            NUMBER,
            IDENTI,
            LABEL,
            REG,

            ADD,
            SUB,
            MUL,
            DIV,

            GT,
            LT,
            CMP,

            EOF,
            EOL,
        }
        public TokenType type;
        public string value;

        public Token(TokenType t, string val)
        {
            type = t;
            value = val;
        }
        

    }

    public class Lexer
    {

        public int index = 0;
        string code;

        public Lexer(string code)
        {
            this.code = code;
        }

        public Token getToken()
        {
            if(index >= code.Length) { return new Token(Token.TokenType.EOF, "EOF"); }

            while (index < code.Length && (code[index] == ' ' || code[index] == '\r'))
            {
                index++;
            }

            if (index >= code.Length) { return new Token(Token.TokenType.EOF, "EOF"); }

            if (code[index] == '\n' )
            {
                index++;
                return new Token(Token.TokenType.EOL, "EOL");
            }

            if (Char.IsLetter(code[index]))
            {
                string str = "";
                while (index < code.Length && Char.IsLetterOrDigit(code[index]))
                {
                    str += code[index];
                    index++;
                }

                if (str == "POP") { return new Token(Token.TokenType.POP, str); }
                else if (str == "PUSH") { return new Token(Token.TokenType.PUSH, str); }
                else if (str == "PRINT") { return new Token(Token.TokenType.PRINT, str); }
                else if(str == "LABEL") { return new Token(Token.TokenType.LABEL, str); }

                else if(str == "STACKW") { return new Token(Token.TokenType.STACKW, str); }
                else if (str == "STACKR") { return new Token(Token.TokenType.STACKR, str); }
                else if(str == "LOAD") { return new Token(Token.TokenType.LOAD, str); }
                else if (str == "STORE") { return new Token(Token.TokenType.STORE, str); }
                else if (str == "REG") { return new Token(Token.TokenType.REG, str); }
                else if(str == "GOTO") { return new Token(Token.TokenType.GOTO, str); }

                else if(str == "CMP") { return new Token(Token.TokenType.CMP, str); }
                else if (str == "GT") { return new Token(Token.TokenType.GT, str); }
                else if (str == "LT") { return new Token(Token.TokenType.LT, str); }

                else if (str == "ADD") { return new Token(Token.TokenType.ADD, str); }
                else if (str == "SUB") { return new Token(Token.TokenType.SUB, str); }
                else if (str == "MUL") { return new Token(Token.TokenType.MUL, str); }
                else if (str == "DIV") { return new Token(Token.TokenType.DIV, str); }
                else { return new Token(Token.TokenType.IDENTI, str); }
            }

            if (Char.IsDigit(code[index]))
            {
                string str = "";
                while (Char.IsDigit(code[index]) && index < code.Length)
                {
                    str += code[index];
                    index++;
                }

                return new Token(Token.TokenType.NUMBER, str);
            }
            
            return null;
        }
    }
}
