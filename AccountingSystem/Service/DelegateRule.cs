﻿using System;

namespace AccountingSystem.Service
{
    public sealed class DelegateRule<T> : Rule<T>
    {
        private Func<T, bool> _rule;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateRule<T>"/> class.
        /// </summary>
        /// <param name="propertyName">>The name of the property the rules applies to.</param>
        /// <param name="error">The error if the rules fails.</param>
        /// <param name="rule">The rule to execute.</param>
        public DelegateRule(string propertyName, object error, Func<T, bool> rule)
            : base(propertyName, error)
        {
            if (rule == null)
            {
                throw new ArgumentNullException(nameof(rule));
            }

            _rule = rule;
        }

        #endregion

        #region Rule<T> Members

        /// <summary>
        /// Applies the rule to the specified object.
        /// </summary>
        /// <param name="obj">The object to apply the rule to.</param>
        /// <returns>
        /// <c>true</c> if the object satisfies the rule, otherwise <c>false</c>.
        /// </returns>
        public override bool Apply(T obj)
        {
            return _rule(obj);
        }

        #endregion
    }
}
