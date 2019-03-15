using System;
using System.Linq.Expressions;

namespace Core.Service
{
    public class DynamicLambda<T>
    {

        private ParameterExpression parameter;

        private ParameterExpressionRewriter paraRewriter;
        public DynamicLambda()
        {
            parameter = Expression.Parameter(typeof(T), "parameter");
            paraRewriter = new ParameterExpressionRewriter(parameter);
        }

        /// <summary>
        /// Combine two lambda epression by AndAlso operation
        /// </summary>
        /// <param name="leftExpression">Left Expression, and it can be null.</param>
        /// <param name="rightExpression">Right Expression, and it can be null.</param>
        /// <returns>Combined Expression. But may be null.</returns>
        /// <remarks>
        /// If leftParameter have value, and rightParameter is null, then return rightParamter.
        /// If rightParameter have value, and leftParameter is null, then return leftParamter.
        /// If both parameters is null, then return null.
        /// </remarks>
        public Expression<Func<T, bool>> BuildQueryAnd(Expression<Func<T, bool>> leftExpression, Expression<Func<T, bool>> rightExpression)
        {
            if (leftExpression == null)
            {
                if (rightExpression == null)
                {
                    return null;
                }
                else
                {
                    return rightExpression;
                }
            }
            else
            {
                if (rightExpression == null)
                {
                    return leftExpression;
                }
            }
            leftExpression = paraRewriter.VisitAndConvert(leftExpression, "BuildQueryAnd");
            rightExpression = paraRewriter.VisitAndConvert(rightExpression, "BuildQueryAnd");
            var result = Expression.AndAlso(leftExpression.Body, rightExpression.Body);
            return Expression.Lambda<Func<T, bool>>(result, parameter);

        }

        /// <summary>
        /// Combine two lambda epression by OrElse operation
        /// </summary>
        /// <param name="leftExpression">Left Expression, and it can be null.</param>
        /// <param name="rightExpression">Right Expression, and it can be null.</param>
        /// <returns>Combined Expression. But may be null.</returns>
        /// <remarks>
        /// If leftParameter have value, and rightParameter is null, then return rightParamter.
        /// If rightParameter have value, and leftParameter is null, then return leftParamter.
        /// If both parameters is null, then return null.
        /// </remarks>
        public Expression<Func<T, bool>> BuildQueryOr(Expression<Func<T, bool>> leftExpression, Expression<Func<T, bool>> rightExpression)
        {

            if (leftExpression == null)
            {
                if (rightExpression == null)
                {
                    return null;
                }
                else
                {
                    return rightExpression;
                }
            }
            else
            {
                if (rightExpression == null)
                {
                    return leftExpression;
                }
            }
            leftExpression = paraRewriter.VisitAndConvert(leftExpression, "BuildQueryOr");
            rightExpression = paraRewriter.VisitAndConvert(rightExpression, "BuildQueryOr");
            var result = Expression.OrElse(leftExpression.Body, rightExpression.Body);
            return System.Linq.Expressions.Expression.Lambda<Func<T, bool>>(result, parameter);

        }

    }

    /// <summary>
    /// This Class is inherit from ExpressionVisitor.
    /// It is use to rebuild the Expression Tree and change the Expression's Parameter
    /// </summary>
    /// <remarks></remarks>
    public class ParameterExpressionRewriter : ExpressionVisitor
    {
        private ParameterExpression _param;
        /// <summary>
        /// Create an instance, and set the parameter to replace
        /// </summary>
        /// <param name="param"></param>
        /// <remarks></remarks>
        public ParameterExpressionRewriter(ParameterExpression param)
        {
            _param = param;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if ((node != null) && !object.ReferenceEquals(node, _param))
            {
                return _param;
            }
            return node;
        }

    }
}
