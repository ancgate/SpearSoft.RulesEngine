using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace RulesEngine.Rules
{
    public class GreaterThanRule<R> : IRule<R>
        where R : IComparable<R>
    {
        private R _greaterThan;
        private int _compareToResult;

        public GreaterThanRule(R greaterThan, bool inclusive)
        {
            _greaterThan = greaterThan;
            if (inclusive)
            {
                _compareToResult = 1;
            }
            else
            {
                _compareToResult = 0;
            }
        }

        public ValidationResult Validate(R value)
        {
            if (_greaterThan.CompareTo(value) < _compareToResult)
                return ValidationResult.Success;
            else
                return ValidationResult.Fail(_greaterThan);
        }

        public string RuleKind
        {
            get
            {
                if (_compareToResult == 0)
                {
                    return RuleKinds.GreaterThanRule;
                }
                else
                {
                    return RuleKinds.GreaterThanOrEqualToRule;
                }
            }
        }

    }

    public class GreaterThanRule<T, R> : IRule<T>
        where R : IComparable<R>
    {
        private Func<T, R> _value1;
        private Func<T, R> _value2;
        private int _compareToResult;

        public GreaterThanRule(Expression<Func<T, R>> value1, Expression<Func<T, R>> value2, bool inclusive)
        {
            _value1 = value1.Compile();
            _value2 = value2.Compile();
            if (inclusive)
            {
                _compareToResult = -1;
            }
            else
            {
                _compareToResult = 0;
            }
        }

        public ValidationResult Validate(T value)
        {
            IComparable<R> v1 = _value1(value);
            R v2 = _value2(value);

            if (v1.CompareTo(v2) > _compareToResult)
                return ValidationResult.Success;
            else
                return ValidationResult.Fail(v2);
        }

        public string RuleKind
        {
            get
            {
                if (_compareToResult == 0)
                {
                    return RuleKinds.GreaterThanRule;
                }
                else
                {
                    return RuleKinds.GreaterThanOrEqualToRule;
                }
            }
        }
    }
}
