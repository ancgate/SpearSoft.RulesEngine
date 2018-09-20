﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace SpearSoft.RulesEngine.Rules
{
    public class NotOneOfRule<R>
        : IRule<R>
    {
        IEnumerable<R> _values;
        IEqualityComparer<R> _comparer;

        public NotOneOfRule(IEnumerable<R> values)
            : this (values, EqualityComparer<R>.Default)
        {
        }

        public NotOneOfRule(IEnumerable<R> values, IEqualityComparer<R> comparer)
        {
            if (comparer == null) throw new System.ArgumentNullException("comparer");
            if (values == null) throw new System.ArgumentNullException("values");
            _values = values;
            _comparer = comparer;
        }

        public ValidationResult Validate(R value)
        {
            if (!_values.Contains(value, _comparer))
                return ValidationResult.Success;
            else
                return ValidationResult.Fail(new object[] { _values });
        }

        public string RuleKind
        {
            get { return RuleKinds.NotOneOfRule; }
        }
    }
}
