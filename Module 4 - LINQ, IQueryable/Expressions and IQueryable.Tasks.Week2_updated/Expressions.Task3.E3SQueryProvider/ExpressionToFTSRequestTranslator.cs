﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Expressions.Task3.E3SQueryProvider
{
    public class ExpressionToFtsRequestTranslator : ExpressionVisitor
    {
        readonly StringBuilder _resultStringBuilder;

        public ExpressionToFtsRequestTranslator()
        {
            _resultStringBuilder = new StringBuilder();
        }

        public string Translate(Expression exp)
        {
            Visit(exp);

            return _resultStringBuilder.ToString();
        }

        #region protected methods

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.DeclaringType == typeof(Queryable)
                && node.Method.Name == "Where")
            {
                var predicate = node.Arguments[1];
                Visit(predicate);

                return node;
            }

            if(node.Method.DeclaringType == typeof(string))
            {
                MemberExpression left = node.Object as MemberExpression;           

                var right = Expression.Lambda<Func<object>>(node.Arguments[0]).Compile().Invoke();

                switch (node.Method.Name) 
                {
                    case "Equals":
                        break;
                    case "StartsWith":
                        right += "*";
                        break;
                    case "EndsWith":
                        right = "*" + right;
                        break;
                    case "Contains":
                        right = "*" + right + "*";
                        break;
                }                        

                var binaryExpression = Expression.MakeBinary(ExpressionType.Equal, left, Expression.Constant(right));

                Visit(binaryExpression);

                return node;
            }

            return base.VisitMethodCall(node);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.Equal:
                    if(node.Left.NodeType == ExpressionType.Constant && node.Right.NodeType == ExpressionType.MemberAccess)
                    {
                        Visit(node.Right);
                        _resultStringBuilder.Append("(");
                        Visit(node.Left);
                        _resultStringBuilder.Append(")");
                    }
                    else
                    {
                        Visit(node.Left);
                        _resultStringBuilder.Append("(");
                        Visit(node.Right);
                        _resultStringBuilder.Append(")");
                    }
                    break;

                case ExpressionType.AndAlso:
                    _resultStringBuilder.Append("{'statements':[{'query':'");
                    Visit(node.Left);
                    _resultStringBuilder.Append("'},{'query':'");
                    Visit(node.Right);
                    _resultStringBuilder.Append("'}]}");
                    break;

                default:
                    throw new NotSupportedException($"Operation '{node.NodeType}' is not supported");
            };

            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            _resultStringBuilder.Append(node.Member.Name).Append(":");

            return base.VisitMember(node);
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            _resultStringBuilder.Append(node.Value);

            return node;
        }

        #endregion
    }
}
