using Greg.Xrm.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Greg.Xrm.Model
{
	/// <summary>
	/// Base interface for MVVM implementation.
	/// Allows declarative configuration of the viewmodel behavior when something changes
	/// </summary>
	public abstract class ViewModel : INotifyPropertyChanged
	{
		private readonly Dictionary<string, object> propertyDict = new Dictionary<string, object>();
		private readonly Dictionary<string, object> defaultValueSetterDict = new Dictionary<string, object>();

		private readonly Dictionary<string, List<PropertyInfo>> dependentPropertyDict = new Dictionary<string, List<PropertyInfo>>();
		private readonly Dictionary<string, List<Action<object>>> dependentActionDict = new Dictionary<string, List<Action<object>>>();


		public event PropertyChangedEventHandler PropertyChanged;



		/// <summary>
		/// Instructs the viewmodel to apply some logic when a given property changes
		/// </summary>
		/// <typeparam name="TProperty">The type of the property that changes</typeparam>
		/// <param name="propertyLambda">An expression representing the property that changes</param>
		/// <returns>The fluent interface</returns>
		protected IChangeManagerFluent WhenChanges<TProperty>(Expression<Func<TProperty>> propertyLambda)
		{
			var property = GetPropertyInfo(propertyLambda);
			return new ChangeManagerFluent(this, property.Name);
		}


		/// <summary>
		/// Concrete implementation of the fluent interface
		/// </summary>
		class ChangeManagerFluent : IChangeManagerFluent
		{
			private readonly ViewModel parent;
			private readonly string sourcePropertyName;


			/// <summary>
			/// Creates a new instance of this class
			/// </summary>
			/// <param name="parent">The parent viewmodel</param>
			/// <param name="sourcePropertyName">The name of the property that triggers the change</param>
			public ChangeManagerFluent(ViewModel parent, string sourcePropertyName)
			{
				this.parent = parent;
				this.sourcePropertyName = sourcePropertyName;
			}



			/// <summary>
			/// Specifies that the change of a property should trigger also the change of another one
			/// </summary>
			/// <typeparam name="TProperty">The type of the "dependent" property</typeparam>
			/// <param name="otherProperty">An expression that represents the property that "depends" from the first one</param>
			/// <returns>The fluent interface</returns>
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
			



			/// <summary>
			/// Instructs the viewmodel to execute a specific method when a property changes
			/// </summary>
			/// <param name="callback">The method to call. The argument of the method is the new value of the property</param>
			/// <returns>The fluent interface</returns>
			public IChangeManagerFluent Execute(Action<object> callback)
			{
				if (!parent.dependentActionDict.ContainsKey(sourcePropertyName))
				{
					parent.dependentActionDict[sourcePropertyName] = new List<Action<object>>();
				}
				parent.dependentActionDict[sourcePropertyName].Add(callback);
				return this;
			}




			/// <summary>
			/// Instructs the viewmodel to send a message when this property changes
			/// </summary>
			/// <param name="messenger">The messenger that will be used to send the message</param>
			public void NotifyOthers(IMessenger messenger)
			{
				void notificationDelegate(object newValue) 
				{
					messenger.Send(new NotifyPropertyChangedMessage(parent, sourcePropertyName, newValue));
				}

				this.Execute(notificationDelegate);
			}
		}



		/// <summary>
		/// Allows to specify a default value for a given property.
		/// </summary>
		/// <typeparam name="T">The type of the property</typeparam>
		/// <param name="propertyLambda">An expression that references the property</param>
		/// <param name="overrideCallback">A function that returns the default value to apply</param>
		protected void OverrideSetDefaultValue<T>(Expression<Func<T>> propertyLambda, Func<T> overrideCallback)
		{
			if (propertyLambda == null)
				throw new ArgumentNullException(nameof(propertyLambda), "The property name cannot be null");
			if (overrideCallback == null)
				throw new ArgumentNullException(nameof(overrideCallback), "The callback cannot be null");

			var property = GetPropertyInfo(propertyLambda);

			this.defaultValueSetterDict[property.Name] = overrideCallback;
		}



		/// <summary>
		/// Given the name of a property, returns it's current value.
		/// </summary>
		/// <typeparam name="T">The type of the property.</typeparam>
		/// <param name="propertyName">The name of the property.</param>
		/// <returns></returns>
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


		/// <summary>
		/// Sets the value of a given property.
		/// If changed, it also triggers the PropertyChanged event and the behaviors 
		/// that have been declaratively specified.
		/// </summary>
		/// <typeparam name="T">The type of the property</typeparam>
		/// <param name="value">The new value of the property</param>
		/// <param name="propertyName">The name of the property</param>
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


		/// <summary>
		/// Given an expression that points to a property of the current object, returns the related PropertyInfo
		/// </summary>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="propertyLambda">The accessor to the property</param>
		/// <returns>The PropertyInfo that described the property in the lambda</returns>
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
