using Greg.Xrm.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Greg.Xrm.Model
{
	public abstract class ViewModel : INotifyPropertyChanged
	{
		private readonly Dictionary<string, object> propertyDict = new Dictionary<string, object>();
		private readonly Dictionary<string, object> defaultValueSetterDict = new Dictionary<string, object>();

		private readonly Dictionary<string, List<PropertyInfo>> dependentPropertyDict = new Dictionary<string, List<PropertyInfo>>();
		private readonly Dictionary<string, List<Action<object>>> dependentActionDict = new Dictionary<string, List<Action<object>>>();


		public event PropertyChangedEventHandler PropertyChanged;


		protected IChangeManagerFluent WhenChanges<TProperty>(Expression<Func<TProperty>> propertyLambda)
		{
			var property = GetPropertyInfo(propertyLambda);
			return new ChangeManagerFluent(this, property.Name);
		}


		class ChangeManagerFluent : IChangeManagerFluent
		{
			private readonly ViewModel parent;
			private readonly string sourcePropertyName;

			public ChangeManagerFluent(ViewModel parent, string sourcePropertyName)
			{
				this.parent = parent;
				this.sourcePropertyName = sourcePropertyName;
			}

			public IChangeManagerFluent ChangesAlso<TProperty>(Expression<Func<TProperty>> otherProperty)
			{
				var targetPropertyName = parent.GetPropertyInfo(otherProperty);

				if (Equals(sourcePropertyName, targetPropertyName)) return this; // it's a loop, doing nothing

				if (!parent.dependentPropertyDict.ContainsKey(sourcePropertyName))
				{
					parent.dependentPropertyDict[sourcePropertyName] = new List<PropertyInfo>();
				}
				parent.dependentPropertyDict[sourcePropertyName].Add(targetPropertyName);
				return this;
			}

			public void NotifyOthers(IMessenger messenger)
			{
				void notificationDelegate(object newValue) 
				{
					messenger.Send(new NotifyPropertyChangedMessage(parent, sourcePropertyName, newValue));
				}


				if (!parent.dependentActionDict.ContainsKey(sourcePropertyName))
				{
					parent.dependentActionDict[sourcePropertyName] = new List<Action<object>>();
				}
				parent.dependentActionDict[sourcePropertyName].Add(notificationDelegate);
			}
		}



		protected void OverrideSetDefaultValue<T>(Expression<Func<T>> propertyLambda, Func<T> overrideCallback)
		{
			if (propertyLambda == null)
				throw new ArgumentNullException(nameof(propertyLambda), "The property name cannot be null");
			if (overrideCallback == null)
				throw new ArgumentNullException(nameof(overrideCallback), "The callback cannot be null");

			var property = GetPropertyInfo(propertyLambda);

			this.defaultValueSetterDict[property.Name] = overrideCallback;
		}




		protected T Get<T>([CallerMemberName] string propertyName = null)
		{
			if (this.propertyDict.TryGetValue(propertyName, out object value))
			{
				return (T)value;
			}
			if (this.defaultValueSetterDict.TryGetValue(propertyName, out object callback))
			{
				var overrideCallback = (Func<T>)callback;
				return overrideCallback();
			}
			return default;
		}

		protected void Set<T>(T value, [CallerMemberName] string propertyName = null)
		{
			if (value == null)
			{
				if (this.defaultValueSetterDict.TryGetValue(propertyName, out object callback))
				{
					var overrideCallback = (Func<T>)callback;
					value = overrideCallback();
				}
			}


			if (this.propertyDict.TryGetValue(propertyName, out object oldValue))
			{
				if (Equals(oldValue, value)) return;
			}

			this.propertyDict[propertyName] = value;
			OnPropertyChanged(propertyName, value);
		}

		private void OnPropertyChanged(string propertyName, object newValue)
		{
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


			if (this.dependentPropertyDict.TryGetValue(propertyName, out List<PropertyInfo> dependentProperties))
			{
				foreach (var dependentProperty in dependentProperties)
				{
					var dependentPropertyValue = dependentProperty.GetValue(this);
					OnPropertyChanged(dependentProperty.Name, dependentPropertyValue);
				}
			}


			if (this.dependentActionDict.TryGetValue(propertyName, out List<Action<object>> dependentActionList))
			{
				foreach (var dependentAction in dependentActionList)
				{
					dependentAction(newValue);
				}
			}
		}


		public PropertyInfo GetPropertyInfo<TProperty>(Expression<Func<TProperty>> propertyLambda)
		{
			var type = GetType();

			if (!(propertyLambda.Body is MemberExpression member))
				throw new ArgumentException(string.Format(
					"Expression '{0}' refers to a method, not a property.",
					propertyLambda.ToString()));

			var propInfo = member.Member as PropertyInfo;
			if (propInfo == null)
				throw new ArgumentException(string.Format(
					"Expression '{0}' refers to a field, not a property.",
					propertyLambda.ToString()));

			if (type != propInfo.ReflectedType &&
				!type.IsSubclassOf(propInfo.ReflectedType))
				throw new ArgumentException(string.Format(
					"Expression '{0}' refers to a property that is not from type {1}.",
					propertyLambda.ToString(),
					type));

			return propInfo;
		}
	}
}
