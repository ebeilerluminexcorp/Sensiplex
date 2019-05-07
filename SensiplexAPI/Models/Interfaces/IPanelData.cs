using Luminex.Models.Implementations;
using System;
using System.Collections.Generic;

namespace Luminex.Models.Interfaces
{
    public interface IPanelData : ICloneable
    {
        string PanelId { get; set; }

        IList<BeadData> BeadList { get; set; }

        bool IsDataValid { get; }

        #region ICloneable

        new IPanelData Clone();

        #endregion ICloneable
    }
}
