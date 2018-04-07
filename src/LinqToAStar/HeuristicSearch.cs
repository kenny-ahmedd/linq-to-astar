﻿using System;
using System.Collections.Generic;

namespace LinqToAStar
{
    using Core;

    public static class HeuristicSearch
    {
        public static HeuristicSearchBase<TStep, TStep> AStar<TStep>(TStep from, TStep to, Func<TStep, int, IEnumerable<TStep>> expander)
        {
            return new HeuristicSearchInitial<TStep>(nameof(AStar<TStep, TStep>), from, to, null, expander);
        }

        public static HeuristicSearchBase<TStep, TStep> AStar<TStep>(TStep from, TStep to, Func<TStep, int, IEnumerable<TStep>> expander, IEqualityComparer<TStep> comparer)
        {
            return new HeuristicSearchInitial<TStep>(nameof(AStar<TStep, TStep>), from, to, comparer, expander);
        }

        public static HeuristicSearchBase<TStep, TStep> BestFirst<TStep>(TStep from, TStep to, Func<TStep, int, IEnumerable<TStep>> expander)
        {
            throw new NotImplementedException();
        }

        public static HeuristicSearchBase<TStep, TStep> BestFirst<TStep>(TStep from, TStep to, Func<TStep, int, IEnumerable<TStep>> expander, IEqualityComparer<TStep> comparer)
        {
            throw new NotImplementedException();
        }

        public static HeuristicSearchBase<TStep, TStep> RecursiveBestFirstSearch<TStep>(TStep from, TStep to, Func<TStep, int, IEnumerable<TStep>> expander)
        {
            throw new NotImplementedException();
        }

        public static HeuristicSearchBase<TStep, TStep> RecursiveBestFirstSearch<TStep>(TStep from, TStep to, Func<TStep, int, IEnumerable<TStep>> expander, IEqualityComparer<TStep> comparer)
        {
            throw new NotImplementedException();
        }

        public static HeuristicSearchBase<TStep, TStep> IterativeDeepeningAStar<TStep>(TStep from, TStep to, Func<TStep, int, IEnumerable<TStep>> expander)
        {
            throw new NotImplementedException();
        }

        public static HeuristicSearchBase<TStep, TStep> IterativeDeepeningAStar<TStep>(TStep from, TStep to, Func<TStep, int, IEnumerable<TStep>> expander, IEqualityComparer<TStep> comparer)
        {
            throw new NotImplementedException();
        }
    }
}