using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace JsonConditionalSerialization.Code
{
    public class SerializedPropertyCollection<T> : ISerializedPropertyNameCollection
    {
        private readonly HashSet<string> _shouldSerializePropertyNames;

        public SerializedPropertyCollection(params Expression<Func<T, object>>[] propertySelectors)
        {
            if (propertySelectors == null) throw new ArgumentNullException("propertySelectors");

            var propertyNames = propertySelectors.Select(GetMemberName);
            _shouldSerializePropertyNames = new HashSet<string>(propertyNames, StringComparer.Ordinal);
        }

        public bool Contains(string propertyName)
        {
            return _shouldSerializePropertyNames.Contains(propertyName);
        }

        /// <summary>
        /// From: http://stackoverflow.com/a/20533751/423988
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        private static string GetMemberName(Expression<Func<T, object>> expr)
        {
            if (expr == null) throw new ArgumentNullException("expr");
            var currentExpression = expr.Body;
            while (true)
            {
                switch (currentExpression.NodeType)
                {
                    case ExpressionType.Parameter:
                        return ((ParameterExpression)currentExpression).Name;
                    case ExpressionType.MemberAccess:
                        return ((MemberExpression)currentExpression).Member.Name;
                    case ExpressionType.Call:
                        return ((MethodCallExpression)currentExpression).Method.Name;
                    case ExpressionType.Convert:
                    case ExpressionType.ConvertChecked:
                        currentExpression = ((UnaryExpression)currentExpression).Operand;
                        break;
                    case ExpressionType.Invoke:
                        currentExpression = ((InvocationExpression)currentExpression).Expression;
                        break;
                    case ExpressionType.ArrayLength:
                        return "Length";
                    default:
                        throw new Exception("not a proper member selector");
                }
            }
        }
    }
}