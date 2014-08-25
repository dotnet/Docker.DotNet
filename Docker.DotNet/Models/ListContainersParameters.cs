using System;

namespace Docker.DotNet.Models
{
	public partial class ListContainersParameters
	{
		public class TimeConstraint {
			public TimeConstraintMode Mode { get; private set; }
			public string ContainerId { get; private set; }

			public TimeConstraint(TimeConstraintMode mode, string containerId){
				if (string.IsNullOrEmpty(containerId)){
					throw new ArgumentNullException("containerId");
				}

				this.Mode = mode;
				this.ContainerId = containerId;
			}
		}

		public enum TimeConstraintMode {
			Before,
			Since
		}
	}

	public partial class ListContainersParameters
	{
		private bool? allBackingField;
		private long? limitBackingField;
		private TimeConstraint timeFilterBackingField;

		private readonly InvalidOperationException AllAndLimitExclusiveException = new InvalidOperationException ("'all' and 'limit' parameters cannot be used mutually");
		private readonly InvalidOperationException AllAndTimeConstraintExclusiveException = new InvalidOperationException ("'all' and 'before'/'since' parameters cannot be used mutually");


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

		public bool? Sizes { get; set; }

		public ListContainersParameters ()
		{
		}
	}
}

