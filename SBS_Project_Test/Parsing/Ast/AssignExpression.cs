﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MSAst = System.Linq.Expressions;
using SBSEngine.Runtime;
using SBSEngine.Parsing;

namespace SBSEngine.Parsing.Ast
{
    class AssignExpression : MSAst.Expression
    {
        private VariableAccess _variable;
        private MSAst.Expression _value;
        private SBSOperator _assignOp;

        public AssignExpression(VariableAccess variable, MSAst.Expression value, SBSOperator assignOp)
        {
            _variable = variable;
            _value = value;
            _assignOp = assignOp;
        }

        public override MSAst.Expression Reduce()
        {
            return _variable.Assign(_value, _assignOp);
        }

    }
}
