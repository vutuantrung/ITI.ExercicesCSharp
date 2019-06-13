using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.StringTokenizer
{
    public interface ITokenizer
    {
        TokenType CurrentToken { get; }

        TokenType GetNextToken();

        bool Match(TokenType t);

        bool MatchInteger(out int value);

        bool MatchDouble(out double value);
    }
}
