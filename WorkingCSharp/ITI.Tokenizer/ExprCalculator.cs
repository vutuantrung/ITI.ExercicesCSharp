﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.Tokenizer
{
    public static class ExprCalculator
    {
        public static double Compute(string expression)
        {
            var t = new StringTokenizer(expression);
            // With this tokenizer, head must be forwarded at first.
            return t.GetNextToken() == TokenType.EndOfInput ? 0.0 : ComputeCondExpression(t);
        }

        /// <summary>
        /// condExpression → expression [? condExpression : condExpression]
        /// </summary>
        static double ComputeCondExpression(StringTokenizer t)
        {
            var expr = ComputeExpression(t);
            if (t.Match(TokenType.QuestionMark))
            {
                var then = ComputeCondExpression(t);
                if (!t.Match(TokenType.Colon))
                {
                    throw new Exception("Expected : of ternary operator.");
                }
                var @else = ComputeCondExpression(t);
                expr = expr > 0 ? then : @else;
            }
            return expr;
        }

        /// <summary>
        /// expression → terme  [opérateur-additif  terme]*
        /// </summary>
        static double ComputeExpression(StringTokenizer t)
        {
            var expr = ComputeTerm(t);
            while (t.CurrentToken == TokenType.Plus
                    || t.CurrentToken == TokenType.Minus)
            {
                if (t.Match(TokenType.Plus)) expr += ComputeTerm(t);
                if (t.Match(TokenType.Minus)) expr -= ComputeTerm(t);
            }
            return expr;

            //
            // expression → terme | terme opérateur-additif terme
            // ==> only ONE primary/root addition/substraction is handled here!
            //
            //var expr = ComputeTerm( t );
            //if( t.Match( TokenType.Plus ) ) expr += ComputeTerm( t );
            //else if( t.Match( TokenType.Minus ) ) expr -= ComputeTerm( t );
            //return expr;
        }

        /// <summary>
        /// terme → facteur [opérateur-multiplicatif  facteur]*
        /// </summary>
        static double ComputeTerm(StringTokenizer t)
        {
            var fact = ComputeFactor(t);
            while (t.CurrentToken == TokenType.Mult
                    || t.CurrentToken == TokenType.Div)
            {
                if (t.Match(TokenType.Mult)) fact *= ComputeFactor(t);
                if (t.Match(TokenType.Div)) fact /= ComputeFactor(t);
            }
            return fact;
        }

        /// <summary>
        /// facteur ==> PositiveFacteur | '-' PositiveFacteur
        /// </summary>
        static double ComputeFactor(StringTokenizer t)
        {
            if (t.Match(TokenType.Minus))
            {
                return -ComputePositiveFactor(t);
            }
            return ComputePositiveFactor(t);
        }


        /// <summary>
        /// positiveFacteur → nombre  |  ‘(’  expression  ‘)’ 
        /// </summary>
        static double ComputePositiveFactor(StringTokenizer t)
        {
            if (!t.MatchDouble(out var f))
            {
                if (!t.Match(TokenType.OpenPar)) throw new Exception("Expected number or (.");
                f = ComputeExpression(t);
                if (!t.Match(TokenType.ClosePar)) throw new Exception("Expected ).");
            }
            return f;
        }
    }
}
