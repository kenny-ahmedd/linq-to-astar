﻿using System;

namespace Heuristic.Linq
{
    using Algorithms;

    /// <summary>
    /// Reports the progress of an algorithm from an abstract view. 
    /// </summary>
    /// <typeparam name="TStep">The type of step of the problem.</typeparam>
    public abstract class AlgorithmProgress<TStep> : IProgress<IAlgorithmState<TStep>>
    {
        /// <summary>
        /// Invoked when the progress of an algorithm has changed.
        /// </summary>
        public EventHandler<IAlgorithmState<TStep>> ProgressChanged;

        void IProgress<IAlgorithmState<TStep>>.Report(IAlgorithmState<TStep> value)
        {
            OnReport(value);
        }

        /// <summary>
        /// Invokes the <see cref="ProgressChanged"/> event.
        /// </summary>
        /// <param name="state">The state of an algorithm from an abstract view.</param>
        protected virtual void OnReport(IAlgorithmState<TStep> state)
        {
            ProgressChanged?.Invoke(this, state);
        }
    }

    class AlgorithmProgress<TFactor, TStep> : AlgorithmProgress<TStep>, IProgress<AlgorithmState<TFactor, TStep>>
    {
        internal AlgorithmProgress() { }

        void IProgress<AlgorithmState<TFactor, TStep>>.Report(AlgorithmState<TFactor, TStep> value)
        {
            OnReport(value);
        }
    }
}