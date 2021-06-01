namespace Windows.Foundation.Diagnostics
{
    using System;

    internal enum AsyncCausalityStatus
    {
        Started,
        Completed,
        Canceled,
        Error
    }
}

