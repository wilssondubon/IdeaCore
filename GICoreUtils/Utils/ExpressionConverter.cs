using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GICoreUtils.Utils
{
    /// <summary>
    /// ExpressionVisitor para lambdas, convierte entre predicados de una clase a predicado de otra clase
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TOutput"></typeparam>
    public class ExpressionConverter<TInput, TOutput> : ExpressionVisitor
    {
        /// <summary>
        /// parametro a reemplazar
        /// </summary>
        private ParameterExpression replaceParam;
        /// <summary>
        /// convierte un predicado
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public Expression<Func<TOutput, bool>> Convert(Expression<Func<TInput, bool>> expression)
        {
            return (Expression<Func<TOutput, bool>>)Visit(expression);
        }
        public Expression<Func<IQueryable<TOutput>, IOrderedQueryable<TOutput>>> Convert(Expression<Func<IQueryable<TInput>, IOrderedQueryable<TInput>>> expression)
        {
            return (Expression<Func<IQueryable<TOutput>, IOrderedQueryable<TOutput>>>)Visit(expression);
        }
        /// <summary>
        /// modifica una expresion basada en un tipo
        /// </summary>
        /// <typeparam name="T">clase del objeto</typeparam>
        /// <param name="node">la expresion a convertir</param>
        /// <returns>la expresion modificada</returns>
        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            if (!(typeof(T) == typeof(Func<TInput, bool>)))
                return base.VisitLambda<T>(node);
            replaceParam = Expression.Parameter(typeof(TOutput), "p");
            return Expression.Lambda<Func<TOutput, bool>>(Visit(node.Body), replaceParam);
        }
        /// <summary>
        /// convierte una expresion
        /// </summary>
        /// <param name="node">la expresion a convertir</param>
        /// <returns>la expresion modificada</returns>
        protected override Expression VisitParameter(ParameterExpression node) => node.Type == typeof(TInput) ? replaceParam : base.VisitParameter(node);
        /// <summary>
        /// convierte una expresion de tipo member
        /// </summary>
        /// <param name="node">a expresion a convertir</param>
        /// <returns>la expresion modificada</returns>
        /// <exception cref="InvalidOperationException">devuelve error si el miembro no existe</exception>
        protected override Expression VisitMember(MemberExpression node)
        {
            if (!(node.Member.DeclaringType == typeof(TInput)))
                return base.VisitMember(node);
            MemberInfo? member = ((IEnumerable<MemberInfo>)typeof(TOutput).GetMember(node.Member.Name, BindingFlags.Instance | BindingFlags.Public)).FirstOrDefault();
            if (member == null)
                throw new InvalidOperationException("Cannot identify corresponding member of DataObject");
            return Expression.MakeMemberAccess(this.Visit(node.Expression), member);
        }
    }
}
