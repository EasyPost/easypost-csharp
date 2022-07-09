using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EasyPost.Utilities
{
    internal class SwitchCase : IEnumerable<SwitchCase.Case>
    {
        private readonly List<Case> _list = new List<Case>();

        public void Add(object value, Action action)
        {
            _list.Add(new Case(value, action));
        }

        public void Execute(object value)
        {
            this
                .Where(c => c.Value == value)
                .Select(c => c.Action)
                .FirstOrDefault()
                ?.Invoke();
        }

        IEnumerator<Case> IEnumerable<Case>.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        public sealed class Case
        {
            public Action Action { get; }
            public object Value { get; }

            public Case(object value, Action action)
            {
                Value = value;
                Action = action;
            }
        }
    }
}
