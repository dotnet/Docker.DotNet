using System;

namespace Docker.DotNet.Models
{
    public partial class ListContainersParameters
    {
        public class TimeConstraint
        {
            public TimeConstraintMode Mode { get; private set; }

            public string ContainerId { get; private set; }

            public TimeConstraint(TimeConstraintMode mode, string containerId)
            {
                if (string.IsNullOrEmpty(containerId))
                {
                    throw new ArgumentNullException("containerId");
                }

                this.Mode = mode;
                this.ContainerId = containerId;
            }
        }

        public enum TimeConstraintMode
        {
            Before,
            Since
        }

        internal class TimeConstraintQueryStringConverter : IQueryStringConverter
        {
            public bool CanConvert(Type t)
            {
                return t == typeof (TimeConstraint);
            }

            public string Convert(object o)
            {
                TimeConstraint val = o as TimeConstraint;
                return val.ContainerId;
            }

            public bool ChangesKey()
            {
                return true;
            }

            public string GetKey(object o)
            {
                TimeConstraint val = o as TimeConstraint; // guaranteed to be not null (CanConvert)
                switch (val.Mode)
                {
                    case TimeConstraintMode.Before:
                        return "before";
                    case TimeConstraintMode.Since:
                        return "since";
                    default:
                        throw new InvalidOperationException("Unhandled time constraint mode");
                }
            }
        }
    }

    public partial class ListContainersParameters
    {
        private bool? _allBackingField;
        private long? _limitBackingField;
        private TimeConstraint _timeFilterBackingField;

        private readonly InvalidOperationException _allAndLimitExclusiveException = new InvalidOperationException("'all' and 'limit' parameters cannot be used mutually");
        private readonly InvalidOperationException _allAndTimeConstraintExclusiveException = new InvalidOperationException("'all' and 'before'/'since' parameters cannot be used mutually");

        [QueryStringParameter("all", false, typeof (BoolQueryStringConverter))]
        public bool? All
        {
            get { return _allBackingField; }
            set
            {
                if (value.HasValue)
                {
                    if (this.TimeFilter != null)
                    {
                        throw _allAndTimeConstraintExclusiveException;
                    }

                    if (this.Limit.HasValue)
                    {
                        throw _allAndLimitExclusiveException;
                    }
                }
                _allBackingField = value;
            }
        }
		
        public long? Limit
        {
            get { return _limitBackingField; }
            set
            {
                if (value.HasValue && this.All.HasValue)
                {
                    throw _allAndLimitExclusiveException;
                }
                _limitBackingField = value;
            }
        }

        [QueryStringParameter("$ignore", false, typeof (TimeConstraintQueryStringConverter))]
        public TimeConstraint TimeFilter
        {
            get { return _timeFilterBackingField; }
            set
            {
                if (value != null && this.All.HasValue)
                {
                    throw _allAndTimeConstraintExclusiveException;
                }
                _timeFilterBackingField = value;
            }
        }

        [QueryStringParameter("size", false, typeof (BoolQueryStringConverter))]
        public bool? Sizes { get; set; }

        public ListContainersParameters()
        {
        }
    }
}