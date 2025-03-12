using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EasyPost.Utilities.Internal
{
    // Thanks to https://stackoverflow.com/a/34388226/13343799
#pragma warning disable SA1649
    /// <summary>
    ///     Pre-made switch case scenarios.
    /// </summary>
    internal sealed class SwitchCaseScenario : Enum
#pragma warning restore SA1649
    {
        /// <summary>
        ///     The default scenario to use in a switch-case if no other cases match.
        /// </summary>
        internal static readonly SwitchCaseScenario Default = new(1);

        /// <summary>
        ///     Initializes a new instance of the <see cref="SwitchCaseScenario" /> class.
        /// </summary>
        /// <param name="id">Internal ID for this enum.</param>
        private SwitchCaseScenario(int id)
            : base(id)
        {
        }
    }

    /// <summary>
    ///     Base interface for all switch-case case types.
    /// </summary>
    internal interface ICase
    {
        /// <summary>
        ///     The action to execute if the <see cref="Value"/> matches.
        /// </summary>
        Action? Action { get; }

        /// <summary>
        ///     The value to match to trigger the <see cref="Action"/>.
        /// </summary>
        object Value { get; }
    }

    /// <summary>
    ///     Represents a case in a <see cref="SwitchCase"/> statement with an expression to evaluate and an action to take.
    /// </summary>
    internal class ExpressionCase : ICase
    {
        /// <inheritdoc cref="ICase.Action"/>
        public Action? Action { get; }

        /// <inheritdoc cref="ICase.Value"/>
        public object Value => Expression.Invoke();

        /// <summary>
        ///  A <see cref="Func{TResult}"/> to execute to evaluate the <see cref="Value"/>.
        /// </summary>
        private Func<object> Expression { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ExpressionCase" /> class.
        /// </summary>
        /// <param name="expression">The <see cref="Func{TResult}"/> to execute when evaluating if the "value" matches.</param>
        /// <param name="action">The <see cref="System.Action"/> to execute if the "value" matches.</param>
        internal ExpressionCase(Func<object> expression, Action? action)
        {
            Expression = expression;
            Action = action;
        }
    }

    /// <summary>
    ///     Represents a case in a <see cref="SwitchCase"/> statement with a value to match and an action to take.
    /// </summary>
    internal sealed class StaticCase : ExpressionCase
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="StaticCase" /> class.
        /// </summary>
        /// <param name="value">The value to match to trigger the action.</param>
        /// <param name="action">The <see cref="System.Action"/> to execute if the value matches.</param>
        internal StaticCase(object value, Action? action)
            : base(() => value, action)
        {
        }
    }

    /// <summary>
    ///     A custom switch-case implementation that can handle non-constants and custom enums.
    /// </summary>
    internal sealed class SwitchCase : IEnumerable<ICase>
    {
        /// <summary>
        ///     The internal list of all <see cref="ICase"/>s to evaluate.
        /// </summary>
        private readonly List<ICase> _list = new();

        /// <summary>
        ///     The optional <see cref="System.Action"/> to perform if no other cases match.
        /// </summary>
        private Action? _defaultCaseAction;

        /// <summary>
        ///     Add a case where matching a static value triggers an <see cref="System.Action"/>.
        /// </summary>
        /// <param name="value">Static value to match.</param>
        /// <param name="action"><see cref="System.Action"/> to trigger on match.</param>
        internal void Add(object value, Action? action) => _list.Add(new StaticCase(value, action));

        /// <summary>
        ///     Add a case where matching the result of a <see cref="Func{TResult}"/> triggers an <see cref="System.Action"/>.
        /// </summary>
        /// <param name="func"><see cref="Func{TResult}"/> whose return value to match.</param>
        /// <param name="action"><see cref="System.Action"/> to trigger on match.</param>
        public void Add(Func<object> func, Action? action) => _list.Add(new StaticCase(func.Invoke(), action));

        /// <summary>
        ///     Add a case to store <see cref="System.Action"/>s in special scenarios. Overrides any previously-set <see cref="System.Action"/>s for the same scenario.
        /// </summary>
        /// <param name="switchCaseScenario"><see cref="SwitchCaseScenario"/> to trigger special storage.</param>
        /// <param name="action"><see cref="System.Action"/> to trigger on match.</param>
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
        ///     Execute the <see cref="System.Action"/> of all matching cases. If no matches are found, execute the <see cref="_defaultCaseAction"/> if set.
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
        ///     Execute the <see cref="System.Action"/> of the first matching case. If no match is found, execute the <see cref="_defaultCaseAction"/> if set.
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
        ///     Execute the <see cref="System.Action"/> of the first case that evaluates to true. If no match is found, execute the <see cref="_defaultCaseAction"/> if set.
        /// </summary>
        internal void MatchFirstTrue() => MatchFirst(true);

        /// <summary>
        ///     Execute the <see cref="System.Action"/> of the first case that evaluates to false. If no match is found, execute the <see cref="_defaultCaseAction"/> if set.
        /// </summary>
        internal void MatchFirstFalse() => MatchFirst(false);

        /// <summary>
        ///     Get an enumerator for all stored <see cref="ICase"/>s.
        /// </summary>
        /// <returns>An enumerator for all stored <see cref="ICase"/>s.</returns>
        IEnumerator<ICase> IEnumerable<ICase>.GetEnumerator() => _list.GetEnumerator();

        /// <summary>
        ///     Get an enumerator for all stored <see cref="ICase"/>s.
        /// </summary>
        /// <returns>An enumerator for all stored <see cref="ICase"/>s.</returns>
        IEnumerator IEnumerable.GetEnumerator() => _list.GetEnumerator();

        /// <summary>
        ///     Execute the associated <see cref="System.Action"/> for each matching case.
        ///     If there are no matching cases, execute the <see cref="_defaultCaseAction"/> if set.
        /// </summary>
        /// <param name="matchingCases">List of matching <see cref="ICase"/>s to process.</param>
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
