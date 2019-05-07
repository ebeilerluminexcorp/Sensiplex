using Luminex.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Luminex.Models.Implementations
{
    public class PanelData : IPanelData
    {
        public string PanelId { get; set; }

        public IList<BeadData> BeadList { get; set; }

        public bool IsDataValid
        {
            get
            {
                if (string.IsNullOrEmpty(PanelId) || BeadList == null || BeadList.Count <= 0)
                {
                    return false;
                }

                foreach(var bead in BeadList)
                {
                    if(!bead.IsDataValid)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public PanelData()
        {
            BeadList = new List<BeadData>();
        }

        #region Methods

        public IPanelData Clone()
        {
            return new PanelData()
            {
                PanelId = string.Copy(PanelId),
                BeadList = BeadList.Select(bead => (BeadData)(bead as BeadData).Clone()).ToList()
            };
        }

        #endregion

        #region ICloneable

        object ICloneable.Clone()
        {
            return this.Clone();
        }

       
        #endregion ICloneable
    }
}
