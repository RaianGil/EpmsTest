namespace Windows.Foundation.Diagnostics
{
    using System;

    internal enum CausalityRelation
    {
        AssignDelegate,
        Join,
        Choice,
        Cancel,
        Error
    }
}

