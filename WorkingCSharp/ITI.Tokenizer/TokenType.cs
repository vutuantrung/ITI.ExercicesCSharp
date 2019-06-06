using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.Tokenizer
{
    public enum TokenType
    {
        None,
        Plus,
        Minus,
        Mult,
        Div,
        Number,
        OpenPar,
        ClosePar,
        OpenSquare,
        CloseSquare,
        OpenBracket,
        CloseBracket,

        Comma,
        Dot,
        SemiColon,
        Colon,
        DoubleColon,

        EndOfInput,
        Error
    }
}
