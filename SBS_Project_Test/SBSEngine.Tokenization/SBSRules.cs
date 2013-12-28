﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SBSEngine.Tokenization.SBSRules
{
    public enum LexiconType
    {
        Undefined,

        LInteger,
        LFloat,
        LName,
        LBlank,
        LLineBreak,
        LString,
        LSymbol
    }

    class NumberRule : IRule
    {
        const int INT = 0;
        const int DOT = 1;
        const int FLOAT = 2;
        int status = INT;

        ScannerResult IRule.Scan(int character)
        {
            switch (status)
            {
                case INT:
                    if (char.IsDigit((char)character))
                    {
                        return ScannerResult.Continued;
                    }
                    else if ((char)character == '.')
                    {
                        status = DOT;
                        return ScannerResult.Continued;
                    }
                    break;
                case DOT:
                    if (char.IsDigit((char)character))
                    {
                        status = FLOAT;
                        return ScannerResult.Continued;
                    }
                    break;
                case FLOAT:
                    if (char.IsDigit((char)character))
                    {
                        return ScannerResult.Continued;
                    }
                    break;
            }

            if (status != DOT)
                return ScannerResult.PreviousFinished;
            else
                return ScannerResult.Unmatch;
        }

        Token IRule.Pack(StringBuilder buffer)
        {
            if (status == INT)
            {
                return new Token
                {
                    Type = (int)LexiconType.LInteger,
                    Value = buffer.ToString()
                };
            }
            else if (status == FLOAT)
            {
                return new Token
                {
                    Type = (int)LexiconType.LFloat,
                    Value = buffer.ToString()
                };
            }

            return new Token();
        }

        void IRule.Reset()
        {
            status = INT;
        }

    }

    class BlankRule : IRule
    {
        const int SPACE = 0;
        const int LINEBREAK = 1;
        int status = SPACE;

        ScannerResult IRule.Scan(int character)
        {
            if (char.IsWhiteSpace((char)character))
            {
                if (character == (char)'\n') status = LINEBREAK;
                return ScannerResult.Continued;
            }

            return ScannerResult.PreviousFinished;
        }

        Token IRule.Pack(StringBuilder buffer)
        {
            if (status == SPACE)
                return new Token
                {
                    Type = (int)LexiconType.LBlank,
                    Value = null
                };
            else if (status == LINEBREAK)
                return new Token
                {
                    Type = (int)LexiconType.LLineBreak,
                    Value = null
                };

            return new Token();
        }

        void IRule.Reset();
    }

}
