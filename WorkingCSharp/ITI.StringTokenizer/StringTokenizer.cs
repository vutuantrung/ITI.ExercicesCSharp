using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ITI.StringTokenizer
{
    public class StringTokenizer : ITokenizer
    {
        string _numString = "0123456789";
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

        // Lit le caractere sous la tet
        char Peek()
        {
            Debug.Assert(!IsEnd);
            return _toParse[_pos];
        }

        // Lit le caractere et avance la tete
        char Read()
        {
            Debug.Assert(!IsEnd);
            return _toParse[_pos++];
        }

        void Forward()
        {
            Debug.Assert(!IsEnd);
            ++_pos;
        }

        public TokenType GetNextToken()
        {
            if (IsEnd) return _curToken = TokenType.EndOfInput;

            char c = Read();
            while (char.IsWhiteSpace(c))
            {
                if (IsEnd) return _curToken = TokenType.EndOfInput;
                c = Read();
            }

            switch (c)
            {
                case '+': _curToken = TokenType.Plus; break;
                case '-': _curToken = TokenType.Minus; break;
                case '*': _curToken = TokenType.Mult; break;
                case '/': _curToken = TokenType.Div; break;
                case '(': _curToken = TokenType.OpenPar; break;
                case ')': _curToken = TokenType.ClosePar; break;
                case ';': _curToken = TokenType.SemiColon; break;
                case ',': _curToken = TokenType.Colon; break;
                case '.': _curToken = TokenType.Dot; break;
                case '[': _curToken = TokenType.OpenSquare; break;
                case ']': _curToken = TokenType.CloseSquare; break;
                case '{': _curToken = TokenType.OpenBracket; break;
                case '}': _curToken = TokenType.CloseBracket; break;
                case ':':

                    break;
                default:
                    if (char.IsDigit(c))
                    {
                        _curToken = TokenType.Number;
                        double val = (int)(c - '0');
                        while (!IsEnd && char.IsDigit(c))
                        {
                            val = val * 10 + (int)(c - '0');
                            Forward();
                        }
                        _doubleValue = val;
                    }
                    else _curToken = TokenType.Error;
                    break;
            }

            return _curToken;
        }

        public bool Match(TokenType t)
        {
            if (_curToken == t)
            {
                GetNextToken();
                return true;
            }
            return false;
        }

        public bool MatchInteger(int expectedValue)
        {
            if (_curToken == TokenType.Number
                && _doubleValue < Int32.MaxValue
                && (int)_doubleValue == expectedValue)
            {
                GetNextToken();
                return true;
            }
            return false;
        }

        public bool MatchInteger(out int value)
        {
            if (_curToken == TokenType.Number && _doubleValue < Int32.MaxValue)
            {
                value = (int)_doubleValue;
                GetNextToken();
                return true;
            }
            value = 0;
            return false;
        }

        public bool MatchDouble(out double value)
        {
            value = _doubleValue;
            if (_curToken == TokenType.Number)
            {
                GetNextToken();
                return true;
            }
            return false;
        }

        // Teste si la fin est atteinte
        bool IsEnd
        {
            get { return _pos >= _maxPos; }
        }

        public TokenType CurrentToken
        {
            get { return _curToken; }
        }
    }

    [Flags]
    public enum TokenType
    {
        None = 0,
        IsAddOperator = 1,
        Plus = IsAddOperator,
        Minus = IsAddOperator + 2,
        IsMultOperator = 4,
        Mult = IsMultOperator,
        Div = IsAddOperator + 2,
        Number = 8,
        OpenPar = 16,
        ClosePar = 32,
        EndOfInput = 64,
        SemiColon,
        Colon,
        DoubleColon,
        Comma,
        Dot,
        OpenSquare,
        CloseSquare,
        OpenBracket,
        CloseBracket,
        Error = 128
    }
}
