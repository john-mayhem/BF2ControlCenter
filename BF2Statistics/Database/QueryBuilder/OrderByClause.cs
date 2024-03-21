﻿namespace BF2Statistics.Database.QueryBuilder
{
    public class OrderByClause
    {
        /// <summary>
        /// The field being ordered by
        /// </summary>
        public string FieldName { get; protected set; }

        /// <summary>
        /// The direction in which the field is being sorted
        /// </summary>
        public Sorting SortOrder;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="field">The field being ordered</param>
        public OrderByClause(string field)
        {
            this.FieldName = field;
            this.SortOrder = Sorting.Ascending;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="field">The field being ordered</param>
        /// <param name="order">The sorting direction of the field</param>
        public OrderByClause(string field, Sorting order)
        {
            this.FieldName = field;
            this.SortOrder = order;
        }
    }
}
