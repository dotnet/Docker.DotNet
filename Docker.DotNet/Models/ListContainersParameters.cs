using System;

namespace Docker.DotNet.Models
{
	public partial class ListContainersParameters
	{
		public class TimeConstraint
		{
			public TimeConstraintMode Mode { get; private set; }

			public string ContainerId { get; private set; }

			public TimeConstraint (TimeConstraintMode mode, string containerId)
			{
				if (string.IsNullOrEmpty (containerId)) {
					throw new ArgumentNullException ("containerId");
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
			public bool CanConvert (Type t)
			{
				return t == typeof(TimeConstraint);
			}

			public string Convert (object o)
			{
				TimeConstraint val = o as TimeConstraint;
				return val.ContainerId;
			}

			public bool ChangesKey ()
			{
				return true;
			}

			public string GetKey (object o)
			{
				TimeConstraint val = o as TimeConstraint;
				switch (val.Mode) {
				case ListContainersParameters.TimeConstraintMode.Before:
					return "before";
				case ListContainersParameters.TimeConstraintMode.Since:
					return "since";
				default:
					throw new InvalidOperationException ("Unhandled time constraint mode");
				}
			}
		}
	}

	public partial class ListContainersParameters
	{
		private bool? allBackingField;
		private long? limitBackingField;
		private TimeConstraint timeFilterBackingField;

		private readonly InvalidOperationException AllAndLimitExclusiveException = new InvalidOperationException ("'all' and 'limit' parameters cannot be used mutually");
		private readonly InvalidOperationException AllAndTimeConstraintExclusiveException = new InvalidOperationException ("'all' and 'before'/'since' parameters cannot be used mutually");

		[QueryStringParameterAttribute ("all", false, typeof(BoolQueryStringConverter))]
		public bool? All { 
			get { 
				return allBackingField;
			}
			set {
				if (value.HasValue) {
					if (this.TimeFilter != null) {
						throw AllAndTimeConstraintExclusiveException;
					}

					if (this.Limit.HasValue) {
						throw AllAndLimitExclusiveException;
					}
				}
				allBackingField = value;
			}
		}

		[QueryStringParameterAttribute ("limit", false, typeof(BoolQueryStringConverter))]
		public long? Limit {
			get {
				return limitBackingField;
			} 
			set {
				if (value.HasValue && this.All.HasValue) {
					throw AllAndLimitExclusiveException;
				}
				limitBackingField = value;
			}
		}

		[QueryStringParameterAttribute ("$ignore", false, typeof(TimeConstraintQueryStringConverter))]
		public TimeConstraint TimeFilter {
			get {
				return timeFilterBackingField;
			} 
			set {
				if (value != null && this.All.HasValue) {
					throw AllAndTimeConstraintExclusiveException;
				}
				timeFilterBackingField = value;
			}
		}

		[QueryStringParameterAttribute ("size", false, typeof(BoolQueryStringConverter))]
		public bool? Sizes { get; set; }

		public ListContainersParameters ()
		{
		}
	}
}

