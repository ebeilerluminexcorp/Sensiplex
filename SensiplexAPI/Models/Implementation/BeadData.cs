using Luminex.Models.Interfaces;
using System;
using System.ComponentModel;

namespace Luminex.Models.Implementations
{
    public class BeadData : IBeadData, INotifyPropertyChanged
    {
       const short MIN_REGION_ID = 1;
       const short MAX_REGION_ID = 500;

        public event PropertyChangedEventHandler PropertyChanged;

        private bool _isDisabled;
        public int _expectedBeadCount = 100;

        public int RegionId { get; set; }
        public string AnalyteName { get; set; }
        public int ExpectedBeadCount
        {
            get { return _expectedBeadCount; }
            set
            {
                if (_expectedBeadCount != value)
                {
                    _expectedBeadCount = value;
                }
            }
        }
        public int ActualBeadCount { get; set; }
        public double BeadPercentage
        {
            get
            {
                return ((double)(100 * ActualBeadCount) / ExpectedBeadCount);
            }
        }
        public bool IsDisabled
        {
            get
            {
                return _isDisabled;
            }
            set
            {
                if (value != _isDisabled)
                {
                    _isDisabled = value;
                    OnPropertyChanged("IsDisabled");
                }
            }
        }

        public bool IsDataValid
        {
            get
            {
                return (!string.IsNullOrEmpty(AnalyteName) && (RegionId >= MIN_REGION_ID && RegionId <= MAX_REGION_ID));
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Methods

        public IBeadData Clone()
        {
            return new BeadData()
            {
                ActualBeadCount = ActualBeadCount,
                AnalyteName = string.Copy(AnalyteName),
                ExpectedBeadCount = ExpectedBeadCount,
                RegionId = RegionId,
                IsDisabled = IsDisabled,
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
