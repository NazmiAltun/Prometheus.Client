using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Prometheus.Client.Abstractions;
using Prometheus.Client.MetricsWriter;
using Prometheus.Client.MetricsWriter.Abstractions;

namespace Prometheus.Client
{
    public sealed class CounterInt64 : MetricBase<MetricConfiguration>, ICounter<long>
    {
        private ThreadSafeLong _value  = default;

        public CounterInt64(MetricConfiguration configuration, IReadOnlyList<string> labels)
            : base(configuration, labels)
        {
        }

        public void Inc()
        {
            IncInternal(1, null);
        }

        public void Inc(long increment)
        {
            Inc(increment, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Inc(long increment, long? timestamp)
        {
            if (increment < 0)
                throw new ArgumentOutOfRangeException(nameof(increment), "Counter cannot go down");

            IncInternal(increment, timestamp);
        }

        public long Value => _value.Value;

        protected internal override void Collect(IMetricsWriter writer)
        {
            writer.WriteSample(Value, string.Empty, Configuration.LabelNames, LabelValues, Timestamp);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void IncInternal(long increment, long? timestamp)
        {
            _value.Add(increment);
            TrackObservation(timestamp);
        }
    }
}
