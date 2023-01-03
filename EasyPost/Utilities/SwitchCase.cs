using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EasyPost.Utilities
{
    // Thanks to https://stackoverflow.com/a/34388226/13343799

    internal class SwitchCaseScenario : Enum
    {
        internal static readonly SwitchCaseScenario Default = new(1);

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

        public object Value => Expression.Invoke();

        private Func<object> Expression { get; }

        internal ExpressionCase(Func<object> expression, Action? action)
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
        internal StaticCase(object value, Action? action) : base(() => value, action)
        {
        }
    }

    /// <summary>
    ///     A custom switch-case implementation that can handle non-constants and custom enums.
    /// </summary>
    internal class SwitchCase : IEnumerable<ICase>
    {
        private readonly List<ICase> _list = new();

        private Action? _defaultCaseAction;

        /// <summary>
        ///     Add a case where matching a static value triggers an Action
        /// </summary>
        /// <param name="value">Static value to match.</param>
        /// <param name="action">Action to trigger on match.</param>
        internal void Add(object value, Action? action) => _list.Add(new StaticCase(value, action));

        /// <summary>
        ///     Add a case where matching the result of an function triggers an Action
        /// </summary>
        /// <param name="func">Func whose return value to match.</param>
        /// <param name="action">Action to trigger on match.</param>
        public void Add(Func<object> func, Action? action) => _list.Add(new StaticCase(func.Invoke(), action));

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
        ///     Execute the action of all matching cases. If no matches are found, execute the default case if set.
        /// </summary>
        /// <param name="value">Value to match.</param>
        internal void MatchAll(object value)
        {
            List<ICase> matchingCases = new();

            foreach (ICase @case in _list)
            {
                if (@case.Value.Equals(value))
                {
                    matchingCases.Add(@case);
                }
            }

            ProcessMatchingCases(matchingCases.ToList());
        }

        /// <summary>
        ///     Execute the action of the first matching case. If no match is found, execute the default case if set.
        /// </summary>
        /// <param name="value">Value to match.</param>
        internal void MatchFirst(object value)
        {
            List<ICase> matchingCases = new();

            foreach (ICase @case in _list)
            {
                if (!@case.Value.Equals(value))
                {
                    continue;
                }

                matchingCases.Add(@case);
                break;
            }

            ProcessMatchingCases(matchingCases.ToList());
        }

        /// <summary>
        ///     Execute the action of the first case that evaluates to true. If no match is found, execute the default case if set.
        /// </summary>
        internal void MatchFirstTrue() => MatchFirst(true);

        /// <summary>
        ///     Execute the action of the first case that evaluates to false. If no match is found, execute the default case if set.
        /// </summary>
        internal void MatchFirstFalse() => MatchFirst(false);

        IEnumerator<ICase> IEnumerable<ICase>.GetEnumerator() => _list.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _list.GetEnumerator();

        /// <summary>
        ///     Execute the associated action for each matching case.
        ///     If there are no matching cases, execute the default case if set.
        /// </summary>
        /// <param name="matchingCases">List of matching cases to process.</param>
        private void ProcessMatchingCases(List<ICase> matchingCases)
        {
            if (matchingCases.Count == 0)
            {
                _defaultCaseAction?.Invoke();
            }

            foreach (ICase @case in matchingCases)
            {
                @case.Action?.Invoke();
            }
        }
    }
}
