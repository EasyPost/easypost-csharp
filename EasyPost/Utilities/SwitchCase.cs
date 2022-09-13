using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EasyPost.Utilities
{
    // Thanks to https://stackoverflow.com/a/34388226/13343799

    internal class SwitchCaseScenario : Enum
    {
        internal static readonly SwitchCaseScenario Default = new SwitchCaseScenario(1);

        private SwitchCaseScenario(int id) : base(id)
        {
        }
    }

    /// <summary>
    ///     Base interface for all switch-case case types.
    /// </summary>
    internal interface ICase
    {
        public Action? Action { get; }

        public object Value { get; }
    }

    /// <summary>
    ///     Represents a case in a switch statement with an expression to evaluate and an action to take.
    /// </summary>
    internal class ExpressionCase : ICase
    {
        public Action? Action { get; }

        public object Value
        {
            get { return Expression.Invoke(); }
        }

        private Func<object> Expression { get; }

        protected ExpressionCase(Func<object> expression, Action? action)
        {
            Expression = expression;
            Action = action;
        }
    }

    /// <summary>
    ///     Represents a case in a switch statement with a value to match and an action to take.
    /// </summary>
    internal sealed class StaticCase : ExpressionCase
    {
        public StaticCase(object value, Action? action) : base(() => value, action)
        {
        }
    }

    /// <summary>
    ///     A custom switch-case implementation that can handle non-constants and custom enums.
    /// </summary>
    internal class SwitchCase : IEnumerable<ICase>
    {
        private readonly List<ICase> _list = new List<ICase>();

        private Action? _defaultCaseAction;

        /// <summary>
        ///     Add a case where matching a static value triggers an Action
        /// </summary>
        /// <param name="value">Static value to match.</param>
        /// <param name="action">Action to trigger on match.</param>
        internal void Add(object value, Action? action)
        {
            _list.Add(new StaticCase(value, action));
        }

        /// <summary>
        ///     Add a case to store Actions in special scenarios. Overrides any previously-set actions for the same scenario.
        /// </summary>
        /// <param name="switchCaseScenario">CaseEnum to trigger special storage.</param>
        /// <param name="action">Action to trigger on match.</param>
        internal void Add(SwitchCaseScenario switchCaseScenario, Action? action)
        {
            // ironically, we can't use our custom switch-case here, because it would be recursive.
            // instead, back to good ol' if-else statements.
            if (switchCaseScenario == SwitchCaseScenario.Default)
            {
                // set the default action to trigger if no match(es) found
                _defaultCaseAction = action;
            }
        }

        /// <summary>
        ///     Execute the action of the first matching case. If no match is found, execute the default case if set.
        /// </summary>
        /// <param name="value">Value to match.</param>
        internal void Match(object value)
        {
            ICase? matchingCase = _list.FirstOrDefault(c => c.Value == value);
            if (matchingCase == null)
            {
                _defaultCaseAction?.Invoke();
            }
            else
            {
                matchingCase.Action?.Invoke();
            }
        }

        IEnumerator<ICase> IEnumerable<ICase>.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }
}
