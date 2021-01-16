using System;
using System.Runtime.Serialization;

namespace Greg.Xrm.EnvironmentComparer.Engine
{
	[Serializable]
	internal class EntityNotFoundException : Exception
	{
		public EntityNotFoundException()
		{
		}

		public EntityNotFoundException(string entityName) : base(entityName + " not found!")
		{
			this.EntityName = entityName;
		}

		public EntityNotFoundException(string entityName, Exception innerException) : base(entityName + " not found!", innerException)
		{
			this.EntityName = entityName;
		}

		protected EntityNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		public string EntityName { get; private set; }
	}
}