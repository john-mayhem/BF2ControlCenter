﻿using System.Text.RegularExpressions;

namespace BF2Statistics.MedalData
{
    public enum TokenType
    {
        OpenParen,
        CloseParen,
        ConditionFunction,
        StatFunction,
        Literal
    }

    public class Token : ISourcePart
    {
        public int Position { get; set; }

        public Token[] AsTokens()
        {
            return new[] { this };
        }

        public TokenType Kind { get; set; }

        /// <summary>
        /// Gets or sets the value of the token
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Creates a new Token
        /// </summary>
        /// <param name="kind">The kind (name) of the token</param>
        /// <param name="match">The Match the token is to be generated from</param>
        /// <param name="index">The offset from the beginning of the file the index of the match is relative to</param>
        /// <returns>The newly created token</returns>
        public static Token Create(TokenType kind, Match match, int index)
        {
            return new Token
            {
                Position = match.Index + index,
                Kind = kind,
                Value = match.Value
            };
        }

        /// <summary>
        /// Creates a new Token
        /// </summary>
        /// <param name="kind">The kind (name) of the token</param>
        /// <param name="value">The value to assign to the token</param>
        /// <param name="position">The absolute position in the source file the value is located at</param>
        /// <returns>The newly created token</returns>
        public static Token Create(TokenType kind, string value, int position)
        {
            return new Token
            {
                Kind = kind,
                Value = value,
                Position = position
            };
        }

        public static string TypeToString(TokenType Kind)
        {
            switch (Kind)
            {
                case TokenType.StatFunction:
                    return "StatFunctionStart";
                case TokenType.ConditionFunction:
                    return "ConditionFunctionStart";
                case TokenType.OpenParen:
                    return "OpenParen";
                case TokenType.CloseParen:
                    return "CloseParen";
                case TokenType.Literal:
                    return "Literal";
            }

            return null;
        }
    }
}
