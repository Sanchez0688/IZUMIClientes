using System.Linq.Expressions;

namespace Back.Helpers
{
	public class SwapVisitor : ExpressionVisitor
	{
		private readonly ParameterExpression _source;
		private readonly ParameterExpression _target;

		public SwapVisitor(ParameterExpression source, ParameterExpression target)
		{
			_source = source;
			_target = target;
		}

		protected override Expression VisitParameter(ParameterExpression node)
		{
			return node == _source ? _target : base.VisitParameter(node);
		}

		public static Expression<Func<T, bool>> CombineExpressions<T>(Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
		{
			var parameter = Expression.Parameter(typeof(T), "entity");

			var firstBody = new SwapVisitor(first.Parameters[0], parameter).Visit(first.Body);
			var secondBody = new SwapVisitor(second.Parameters[0], parameter).Visit(second.Body);

			var body = Expression.AndAlso(firstBody, secondBody);

			return Expression.Lambda<Func<T, bool>>(body, parameter);
		}
	}
}
