using Microsoft.Xrm.Sdk;

namespace Greg.Xrm.EnvironmentComparer.Actions
{
	public class ActionCopyEntity : IAction
	{
		public string EntityName { get; set; }
		public string EntityKey { get; set; }
		public string EnvironmentName { get; set; }
		public Entity Entity { get; set; }

		public bool Equals(IAction other)
		{
			if (other == null) return false;
			if (ReferenceEquals(this, other)) return true;
			if (!(other is ActionCopyEntity o1)) return false;

			return this.EntityName.Equals(o1.EntityName) &&
				this.EntityKey.Equals(o1.EntityKey) &&
				this.EnvironmentName.Equals(o1.EnvironmentName);
		}

		public override bool Equals(object obj)
		{
			if (obj == null) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (!(obj is IAction other)) return false;
			return Equals(other);
		}

		public override int GetHashCode()
		{
			return this.CalculateHashCode(
				() => EntityName,
				() => EntityKey,
				() => EnvironmentName
			);
		}

		public override string ToString()
		{
			return $"Copy the <{EntityName}> record with key <{EntityKey}> on <{EnvironmentName}>";
		}
	}
}
