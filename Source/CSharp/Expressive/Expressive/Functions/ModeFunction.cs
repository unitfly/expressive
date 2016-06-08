﻿using Expressive.Expressions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Expressive.Functions
{
    internal class ModeFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "Mode"; } }

        public override object Evaluate(IExpression[] participants)
        {
            this.ValidateParameterCount(participants, -1, 1);
            
            IList<object> values = new List<object>();

            foreach (var p in participants)
            {
                var value = p.Evaluate(this.Arguments);
                var enumerable = value as IEnumerable;

                if (enumerable != null)
                {
                    foreach (var item in enumerable)
                    {
                        values.Add(item);
                    }
                }
                else
                {
                    values.Add(value);
                }
            }

            var groups = values.GroupBy(v => v);
            int maxCount = groups.Max(g => g.Count());
            return groups.First(g => g.Count() == maxCount).Key;
        }

        #endregion
    }
}
