using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressionTrees.Task1.ExpressionsTransformer
{
    public class IncDecExpressionVisitor : ExpressionVisitor
    {
        private Dictionary<string, int> operators;

        readonly Dictionary<ExpressionType, Func<Expression, Expression>> operations = new Dictionary<ExpressionType, Func<Expression, Expression>>()
        {
            {ExpressionType.Add,  Expression.Increment},
            {ExpressionType.Subtract,  Expression.Decrement},
        };

        public Expression Convert(Expression exp)
        {
            return Visit(exp);
        }

        public Expression Convert(Expression exp, Dictionary<string, int> operators)
        {
            this.operators = operators;

            return Visit(exp);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            (Expression parameterExpression, ConstantExpression constantExpression) = DetectNodes(node);

            if (parameterExpression != null && constantExpression != null
                   && constantExpression.Type == typeof(int))
            {

                if (operators?.Count > 0)
                {
                    parameterExpression = operators.TryGetValue(((ParameterExpression)parameterExpression).Name, out var param) ? Expression.Constant(param) : parameterExpression;
                }

                if (node.NodeType == ExpressionType.Add || node.NodeType == ExpressionType.Subtract)
                {
                    if ((int)constantExpression.Value == 1)
                    {
                        return operations[node.NodeType](parameterExpression);
                    }
                }
            }

            return base.VisitBinary(node);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (operators?.Count > 0)
            {
                return operators.TryGetValue(node.Name, out var param) ? (Expression)Expression.Constant(param) : node;
            }

            return base.VisitParameter(node);
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
            => Expression.Lambda(Visit(node.Body), node.Parameters);

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
