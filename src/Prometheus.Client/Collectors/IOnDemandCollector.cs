﻿namespace Prometheus.Client.Collectors
{
    /// <summary>
    ///     interface for On Demand Collector
    /// </summary>
    public interface IOnDemandCollector
    {
        /// <summary>
        ///     Register metrics
        /// </summary>
        void RegisterMetrics();

        /// <summary>
        ///     Update metrics
        /// </summary>
        void UpdateMetrics();
    }
}