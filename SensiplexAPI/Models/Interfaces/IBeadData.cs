

using System;

namespace Luminex.Models.Interfaces
{
    public interface IBeadData : ICloneable
    {
        int RegionId { get; set; }
        string AnalyteName { get; set; }
        int ExpectedBeadCount { get; set; }
        int ActualBeadCount { get; set; }
        double BeadPercentage { get; }
        bool IsDisabled { get; set; }
        bool IsDataValid { get; }

        #region ICloneable

        new IBeadData Clone();

        #endregion ICloneable
    }
}
