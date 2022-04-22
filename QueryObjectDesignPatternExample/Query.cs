using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QueryObjectDesignPatternExample
{
    public class Query<T>
    {
        string baseQuery = "SELECT {0} FROM {1}";

        List<Criteria<T>> criterias;
        public Query(string tableNames) : this("*", tableNames)
        { }

        public Query(List<Criteria<T>> criterias, string tableNames) : this("*", tableNames)
            => this.criterias = criterias;

        public Query(string columnNames, string tableNames)
        {
            this.criterias = new();
            baseQuery = String.Format(baseQuery, columnNames, tableNames);
        }

        public Query(Expression<Func<T, object>> method, string tableNames)
        {
            this.criterias = new();
            NewExpression expression = method.Body as NewExpression;
            List<Expression> arguments = expression.Arguments.ToList();

            StringBuilder columns = new();
            for (int i = 0; i < arguments.Count; i++)
            {
                Expression argument = arguments[i];
                columns.Append(argument.ToString().Remove(0, argument.ToString().IndexOf(".") + 1));
                if (i != arguments.Count - 1)
                    columns.Append(", ");
            }
            baseQuery = String.Format(baseQuery, columns, tableNames);
        }

        public void Add(Criteria<T> criteria)
            => this.criterias.Add(criteria);

        public string GenerateWhereClause()
        {
            StringBuilder whereClause = new();
            whereClause.Append("Where ");
            for (int i = 0; i < criterias.Count; i++)
            {
                Criteria<T> criteria = criterias[i];
                whereClause.Append(criteria.GenerateSql());
                if (i != criterias.Count - 1)
                    whereClause.Append(criteria.QueryOperator switch { QueryLogicalOperator.Or => " OR ", QueryLogicalOperator.And => " AND " });
            }
            return $"{baseQuery} {whereClause.ToString()}";
        }
    }
}
