using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QueryObjectDesignPatternExample
{
    public class Criteria<TModel>
    {
        string @operator;
        string field;
        object value;
        QueryLogicalOperator queryOperator;

        public QueryLogicalOperator QueryOperator => queryOperator;

        public Criteria(string @operator, string field, object value, QueryLogicalOperator queryOperator = QueryLogicalOperator.None)
        {
            this.@operator = @operator;
            this.field = field;
            this.value = value;
            this.queryOperator = queryOperator;
        }

        static string DebugField<TKey>(Expression<Func<TModel, TKey>> method)
        {
            string field = method.Body.ToString();
            field = field.Remove(0, field.IndexOf(".") + 1);
            return field;
        }

        public static Criteria<TModel> GreaterThan(string field, object value, QueryLogicalOperator queryOperator = QueryLogicalOperator.None)
            => new(">", field, value, queryOperator);
        public static Criteria<TModel> GreaterThan<TKey>(Expression<Func<TModel, TKey>> method, object value, QueryLogicalOperator queryOperator = QueryLogicalOperator.None)
        {
            string field = DebugField(method);
            return new(">", field, value, queryOperator);
        }

        public static Criteria<TModel> GreaterThanOrEqual(string field, object value, QueryLogicalOperator queryOperator = QueryLogicalOperator.None)
            => new(">=", field, value, queryOperator);
        public static Criteria<TModel> GreaterThanOrEqual<TKey>(Expression<Func<TModel, TKey>> method, object value, QueryLogicalOperator queryOperator = QueryLogicalOperator.None)
        {
            string field = DebugField(method);
            return new(">=", field, value, queryOperator);
        }

        public static Criteria<TModel> LessThan(string field, object value, QueryLogicalOperator queryOperator = QueryLogicalOperator.None)
            => new("<", field, value, queryOperator);
        public static Criteria<TModel> LessThan<TKey>(Expression<Func<TModel, TKey>> method, object value, QueryLogicalOperator queryOperator = QueryLogicalOperator.None)
        {
            string field = DebugField(method);
            return new("<", field, value, queryOperator);
        }

        public static Criteria<TModel> LessThanOrEqual(string field, object value, QueryLogicalOperator queryOperator = QueryLogicalOperator.None)
            => new("<=", field, value, queryOperator);
        public static Criteria<TModel> LessThanOrEqual<TKey>(Expression<Func<TModel, TKey>> method, object value, QueryLogicalOperator queryOperator = QueryLogicalOperator.None)
        {
            string field = DebugField(method);
            return new("<=", field, value, queryOperator);
        }

        public static Criteria<TModel> Equal(string field, object value, QueryLogicalOperator queryOperator = QueryLogicalOperator.None)
            => new("=", field, value, queryOperator);
        public static Criteria<TModel> Equal<TKey>(Expression<Func<TModel, TKey>> method, object value, QueryLogicalOperator queryOperator = QueryLogicalOperator.None)
        {
            string field = DebugField(method);
            return new("=", field, value, queryOperator);
        }

        public static Criteria<TModel> Contains(string field, object value, QueryLogicalOperator queryOperator = QueryLogicalOperator.None)
            => new("Like", field, $"%{value}%", queryOperator);
        public static Criteria<TModel> Contains<TKey>(Expression<Func<TModel, TKey>> method, object value, QueryLogicalOperator queryOperator = QueryLogicalOperator.None)
        {
            string field = DebugField(method);
            return new("Like", field, $"%{value}%", queryOperator);
        }

        public static Criteria<TModel> StartsWith(string field, object value, QueryLogicalOperator queryOperator = QueryLogicalOperator.None)
            => new("Like", field, $"{value}%", queryOperator);
        public static Criteria<TModel> StartsWith<TKey>(Expression<Func<TModel, TKey>> method, object value, QueryLogicalOperator queryOperator = QueryLogicalOperator.None)
        {
            string field = DebugField(method);
            return new("Like", field, $"{value}%", queryOperator);
        }

        public static Criteria<TModel> EndsWith(string field, object value, QueryLogicalOperator queryOperator = QueryLogicalOperator.None)
            => new("Like", field, $"%{value}", queryOperator);
        public static Criteria<TModel> EndsWith<TKey>(Expression<Func<TModel, TKey>> method, object value, QueryLogicalOperator queryOperator = QueryLogicalOperator.None)
        {
            string field = method.Body.ToString();
            return new("Like", field, $"%{value}", queryOperator);
        }

        public string GenerateSql()
            => $"{field} {@operator} {(value is int or long or float or decimal ? value : $"'{value}'")}";
    }
}
