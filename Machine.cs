using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM_Stack
{
    public class Machine
    {
        public List<int> stack;
        public Dictionary<int, int> labelIndexMap = new Dictionary<int, int>();
        public Dictionary<int, int> registerMap = new Dictionary<int, int>();
        public Lexer lexer;
        public Machine(Lexer lexer)
        {
            stack = new List<int>();
            this.lexer = lexer;
        }

        public void PUSH()
        {
            stack.Add(0);
        }

        public void POP()
        {
            stack.RemoveAt(stack.Count - 1);
        }

        public void STACKW(Token token)
        {
            if(token.type == Token.TokenType.NUMBER)
            {
                stack[(stack.Count - 1) - Int32.Parse(token.value)] = stack[stack.Count - 1];
            }
        }

        public void STACKR(Token token)
        {
            if(token.type == Token.TokenType.NUMBER)
            {
                stack.Add(stack[(stack.Count - 1) - Int32.Parse(token.value)]);
            }
        }

        public void LOAD(Token token)
        {
            if(token.type == Token.TokenType.NUMBER)
            {
                stack.Add(Int32.Parse(token.value));
            }
            else if(token.type == Token.TokenType.REG)
            {
                var tok = lexer.getToken();
                if(tok.type == Token.TokenType.NUMBER)
                {
                    stack.Add(registerMap[Int32.Parse(tok.value)]);
                }
                else
                {
                    //Error
                }
            }
        }

        public void STORE(Token token)
        {
            Console.WriteLine(token.type);
            if(token.type == Token.TokenType.REG)
            {
                var tok = lexer.getToken();
                if (tok.type == Token.TokenType.NUMBER)
                {
                    if (!registerMap.ContainsKey(Int32.Parse(tok.value)))
                    {
                        registerMap.Add(Int32.Parse(tok.value), stack[stack.Count - 1]);
                    }
                    else
                    {
                        registerMap[Int32.Parse(tok.value)] = stack[stack.Count - 1];
                    }
                        
                    stack.RemoveAt(stack.Count - 1);
                }
            }
            else
            {
                //ERROR
            }
        }

        //Compare
        public void CMP(Token token)
        {
            if(token.type == Token.TokenType.NUMBER)
            {
                if(Int32.Parse(token.value) == stack[stack.Count - 1])
                {
                    //token = lexer.getToken();
                    GOTO(token);
                }
            }
            else if(token.type == Token.TokenType.REG)
            {
                token = lexer.getToken();

                if (token.type == Token.TokenType.NUMBER)
                {
                    if (registerMap[Int32.Parse(token.value)] == stack[stack.Count - 1])
                    {
                        //token = lexer.getToken();
                        GOTO(token);
                    }
                }
            }
        }

        //Less than
        public void LT(Token token)
        {
            if (token.type == Token.TokenType.NUMBER)
            {
                if (Int32.Parse(token.value) < stack[stack.Count - 1])
                {
                    //token = lexer.getToken();
                    GOTO(token);
                }
            }
            else if (token.type == Token.TokenType.REG)
            {
                token = lexer.getToken();

                if (token.type == Token.TokenType.NUMBER)
                {
                    if (registerMap[Int32.Parse(token.value)] < stack[stack.Count - 1])
                    {
                        //token = lexer.getToken();
                        GOTO(token);
                    }
                }
            }
        }

        //Greather than
        public void GT(Token token)
        {
            if (token.type == Token.TokenType.NUMBER)
            {
                if (Int32.Parse(token.value) > stack[stack.Count - 1])
                {
                    //token = lexer.getToken();
                    GOTO(token);
                }
            }
            else if (token.type == Token.TokenType.REG)
            {
                token = lexer.getToken();

                if (token.type == Token.TokenType.NUMBER)
                {
                    if (registerMap[Int32.Parse(token.value)] > stack[stack.Count - 1])
                    {
                        //token = lexer.getToken();
                        GOTO(token);
                    }
                }
            }
        }

        public void PRINT(Token token)
        {
            
            if(token.type == Token.TokenType.NUMBER)
            {
                Console.WriteLine(token.value);
            }
            else if(token.type == Token.TokenType.REG)
            {
                var tok = lexer.getToken();
                if(tok.type == Token.TokenType.NUMBER)
                {
                    Console.WriteLine(registerMap[Int32.Parse(tok.value)]);
                }
            }
        }

        public void GOTO(Token token)
        {
            var tok = lexer.getToken();
            if (tok.type == Token.TokenType.NUMBER)
            {
                lexer.index = labelIndexMap[Int32.Parse(tok.value)];
            }     
        }

        //OPERATIONS ADD SUB MUL DIV

        //Setze label für goto später
        public void LABEL(int num)
        {
            if (!labelIndexMap.ContainsKey(num))
            {
                labelIndexMap.Add(num, lexer.index - 1); //Ich denke -1
            }
            else
            {
                labelIndexMap[num] = lexer.index - 1;
            }
            
        }

        public void ADD(Token token)
        {
            if(token.type == Token.TokenType.NUMBER)
            {
                stack[stack.Count - 1] += Int32.Parse(token.value);
            }
            else if(token.type == Token.TokenType.REG)
            {
                token = lexer.getToken();
                if(token.type == Token.TokenType.NUMBER)
                {
                    stack[stack.Count - 1] += registerMap[Int32.Parse(token.value)];
                }
                
            }
        }

        public void SUB(Token token)
        {
            if (token.type == Token.TokenType.NUMBER)
            {
                stack[stack.Count - 1] += Int32.Parse(token.value);
            }
            else if (token.type == Token.TokenType.REG)
            {
                token = lexer.getToken();
                if (token.type == Token.TokenType.NUMBER)
                {
                    stack[stack.Count - 1] += registerMap[Int32.Parse(token.value)];
                }

            }
        }

        public void MUL(Token token)
        {
            if (token.type == Token.TokenType.NUMBER)
            {
                stack[stack.Count - 1] *= Int32.Parse(token.value);
            }
            else if (token.type == Token.TokenType.REG)
            {
                token = lexer.getToken();
                if (token.type == Token.TokenType.NUMBER)
                {
                    stack[stack.Count - 1] *= registerMap[Int32.Parse(token.value)];
                }

            }
        }

        public void DIV(Token token)
        {
            if (token.type == Token.TokenType.NUMBER)
            {
                stack[stack.Count - 1] /= Int32.Parse(token.value);
            }
            else if (token.type == Token.TokenType.REG)
            {
                token = lexer.getToken();
                if (token.type == Token.TokenType.NUMBER)
                {
                    stack[stack.Count - 1] /= registerMap[Int32.Parse(token.value)];
                }

            }
        }
    }
}
