using System;
using System.Collections.Generic;
using System.Linq;

namespace ClosureBefore5
{
    class Program
    {
        static void Main()
        {
            var actions = new List<Action>();

            using (var enumerator = Enumerable.Range(1, 3).GetEnumerator())
            {
                var closure = new Closure();
                while (enumerator.MoveNext())
                {
                    closure.i = enumerator.Current;
                    var action = new Action(()=>Console.WriteLine(closure.i));
                    actions.Add(action);
                }
            }
            foreach (var action in actions) action();
        }
    }

    class Closure
    {
        public int i;
        public void Action()
        {
            Console.WriteLine(i);
        }
    }
}
