namespace Windows.Foundation.Diagnostics
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [ComImport, Guid("410B7711-FF3B-477F-9C9A-D2EFDA302DC3")]
    internal sealed class TracingStatusChangedEventArgs : ITracingStatusChangedEventArgs
    {
        public bool Enabled { [MethodImpl(MethodImplOptions.InternalCall)] get; }

        public CausalityTraceLevel TraceLevel { [MethodImpl(MethodImplOptions.InternalCall)] get; }
    }
}

