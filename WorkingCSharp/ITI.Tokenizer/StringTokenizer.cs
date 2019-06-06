using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ITI.Tokenizer
{
    public class StringTokenizer
    {
        string _toParse;
        int _pos;
        int _maxPos;
        double _doubleValue;
        TokenType _curToken;
        public StringTokenizer(string s)
            : this(s, 0, s.Length)
        {
        }

        public StringTokenizer(string s, int startIndex)
            : this(s, startIndex, s.Length - startIndex)
        {
        }

        public StringTokenizer(string s, int startIndex, int count)
        {
            _curToken = TokenType.None;
            _toParse = s;
            _pos = startIndex;
            _maxPos = startIndex + count;
        }

        public static IEnumerable<TokenType> Parse(string toParse)
        {
            var t = new StringTokenizer(toParse);
            while (t.GetNextToken() != TokenType.EndOfInput) yield return t.CurrentToken;
        }

        char Peek()
        {
            Debug.Assert(!IsEnd);
            return _toParse[_pos];
        }

        void Forward()
        {
            Debug.Assert(!IsEnd);
            ++_pos;
        }

        char Read()
        {
            Debug.Assert(!IsEnd);
            return _toParse[_pos++];
        }

        public TokenType CurrentToken
        {
            get { return _curToken; }
        }

        bool IsEnd
        {
            get { return _pos >= _maxPos; }
        }

        public TokenType GetNextToken()
        {
            if (IsEnd) return _curToken = TokenType.EndOfInput;

            char c = Read();

            while (Char.IsWhiteSpace(c))
            {
                if (IsEnd) return _curToken = TokenType.EndOfInput;
                c = Read();
            }

            switch (c)
            {
                case '+': _curToken = TokenType.Plus; break;
                case '-': _curToken = TokenType.Minus; break;
                case '*': _curToken = TokenType.Mult; break;
                case '/':_curToken = TokenType.Div;break;
                case '(': _curToken = TokenType.OpenPar; break;
                case ')': _curToken = TokenType.ClosePar; break;
                case '[': _curToken = TokenType.OpenSquare; break;
                case ']': _curToken = TokenType.CloseSquare; break;
                case '{': _curToken = TokenType.OpenBracket; break;
                case '}': _curToken = TokenType.CloseBracket; break;
                case ',': _curToken = TokenType.Comma; break;
                case '.': _curToken = TokenType.Dot; break;
                case ';': _curToken = TokenType.SemiColon; break;
                case ':':
                    // Check if '::'
                    if(!IsEnd && Peek() == ':')
                    {
                        _curToken = TokenType.DoubleColon;
                        Forward();
                    }
                    else { _curToken = TokenType.Colon; }
                    break;
                default:
                    if (char.IsDigit(c))
                    {
                        _curToken = TokenType.Number;
                        double val = (int)(c - '0');
                        while(!IsEnd && char.IsDigit(c = Peek()))
                        {
                            // Append number letter. Enx: 125 = 1 -> 12 -> 125
                            val = val * 10 + (int)(c - '0');
                            Forward();
                        }
                        _doubleValue = val;
                    }
                    else { _curToken = TokenType.Error; }
                    break;
            }
            return _curToken;
        }
    }
}
