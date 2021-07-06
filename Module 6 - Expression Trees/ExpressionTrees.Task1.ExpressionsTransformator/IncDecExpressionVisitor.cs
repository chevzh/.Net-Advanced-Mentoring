using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressionTrees.Task1.ExpressionsTransformer
{
    public class IncDecExpressionVisitor : ExpressionVisitor
    {
        readonly Dictionary<ExpressionType, Func<ParameterExpression, Expression>> operations = new Dictionary<ExpressionType, Func<ParameterExpression, Expression>>() 
        { 
            {ExpressionType.Add,  Expression.Increment},
            {ExpressionType.Subtract,  Expression.Decrement},
        };

        public Expression Convert(Expression exp)
        {
            return Visit(exp);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            if (node.NodeType == ExpressionType.Add || node.NodeType == ExpressionType.Subtract)
            {
                (ParameterExpression parameterExpression, ConstantExpression constantExpression) = DetectNodes(node);

                if (parameterExpression != null && constantExpression != null
                   && constantExpression.Type == typeof(int) && (int)constantExpression.Value == 1)
                {
                    return operations[node.NodeType](parameterExpression);
                }
            }

            return base.VisitBinary(node);
        }

        private (ParameterExpression, ConstantExpression) DetectNodes(BinaryExpression node)
        {
            ParameterExpression parameterExpression = null;
            ConstantExpression constantExpression = null;

            if (node.Left.NodeType == ExpressionType.Parameter)
            {
                parameterExpression = (ParameterExpression)node.Left;
            }
            else if (node.Left.NodeType == ExpressionType.Constant)
            {
                constantExpression = (ConstantExpression)node.Left;
            }

            if (node.Right.NodeType == ExpressionType.Parameter)
            {
                parameterExpression = (ParameterExpression)node.Right;
            }
            else if (node.Right.NodeType == ExpressionType.Constant)
            {
                constantExpression = (ConstantExpression)node.Right;

            }

            return (parameterExpression, constantExpression);
        }
    }
}
