using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM_Stack
{
    public class Interpreter
    {
        Lexer lexer;
        Token token;
        Machine machine;
        public Interpreter(string code)
        {
            lexer = new Lexer(code);
            machine = new Machine(lexer);

            //Schonmal labels setzen
            Token token = lexer.getToken();
            while (token.type != Token.TokenType.EOF)
            {
                if (token.type == Token.TokenType.LABEL)
                {
                    token = lexer.getToken();
                    if (token.type == Token.TokenType.NUMBER)
                    {
                        machine.LABEL(Int32.Parse(token.value));
                    }
                }
                token = lexer.getToken();
            }

            
            lexer = new Lexer(code);
            machine.lexer = lexer;
        }

        public void Run()
        {
            Token token = lexer.getToken();
            while (token.type != Token.TokenType.EOF)
            {
                //Console.WriteLine(token.value);

                if(token.type == Token.TokenType.PUSH)
                {
                    machine.PUSH();
                }
                else if(token.type == Token.TokenType.POP)
                {
                    machine.POP();
                }
                //Kann das hier eigentlich rausnehmen
                else if(token.type == Token.TokenType.LABEL)
                {
                    token = lexer.getToken();
                    if(token.type == Token.TokenType.NUMBER)
                    {
                        machine.LABEL(Int32.Parse(token.value));
                    }
                }
                else if(token.type == Token.TokenType.STACKW)
                {
                    token = lexer.getToken();
                    machine.STACKW(token);
                }
                else if(token.type == Token.TokenType.STACKR)
                {
                    token = lexer.getToken();
                    machine.STACKR(token);
                }
                else if(token.type == Token.TokenType.LOAD)
                {
                    token = lexer.getToken();
                    machine.LOAD(token);
                }
                else if(token.type == Token.TokenType.GOTO)
                {
                    //token = lexer.getToken();
                    machine.GOTO(token);
                }
                else if(token.type == Token.TokenType.STORE)
                {
                    token = lexer.getToken();
                    machine.STORE(token);
                }
                else if (token.type == Token.TokenType.PRINT)
                {
                    token = lexer.getToken();
                    machine.PRINT(token);
                }
                else if(token.type == Token.TokenType.ADD)
                {
                    token = lexer.getToken();
                    machine.ADD(token);
                }
                else if(token.type == Token.TokenType.SUB)
                {
                    token = lexer.getToken();
                    machine.SUB(token);
                }
                else if (token.type == Token.TokenType.MUL)
                {
                    token = lexer.getToken();
                    machine.MUL(token);
                }
                else if (token.type == Token.TokenType.DIV)
                {
                    token = lexer.getToken();
                    machine.DIV(token);
                }
                else if(token.type == Token.TokenType.CMP)
                {
                    token = lexer.getToken();
                    machine.CMP(token);
                }
                else if (token.type == Token.TokenType.GT)
                {
                    token = lexer.getToken();
                    machine.GT(token);
                }
                else if (token.type == Token.TokenType.LT)
                {
                    token = lexer.getToken();
                    machine.LT(token);
                }

                token = lexer.getToken();
            }
            //Console.WriteLine(token.value);
        }
    }
}
