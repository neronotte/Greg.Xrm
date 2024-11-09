using Greg.Xrm.Model;
using Greg.Xrm.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace Greg.Xrm
{


	public static class BindingExtensions
	{
		private static void RemoveBinding(IBindableComponent component, string propertyName)
		{
			for (var i = 0; i < component.DataBindings.Count; i++)
			{
				var binding = component.DataBindings[i];
				if (binding.PropertyName == propertyName)
				{
					component.DataBindings.RemoveAt(i);
					break;
				}
			}
		}


		public static Binding Bind<TComponent, TViewModel, TProperty>(
			this TComponent component,
			Expression<Func<TComponent, TProperty>> componentProperty,
			TViewModel viewModel,
			Expression<Func<TViewModel, TProperty>> viewModelProperty)
			where TComponent : class, IBindableComponent
			where TViewModel : ViewModel
		{
			if (component == null) throw new ArgumentNullException(nameof(component));
			if (componentProperty == null) throw new ArgumentNullException(nameof(componentProperty));
			if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));
			if (viewModelProperty == null) throw new ArgumentNullException(nameof(viewModelProperty));

			var componentMemberName = componentProperty.GetMemberName();
			RemoveBinding(component, componentMemberName);

			return component.DataBindings.Add(
				componentMemberName,
				viewModel,
				viewModelProperty.GetMemberName());
		}

		public static void BindCommand<TComponent>(
			this TComponent component,
			Func<ICommand> commandAccessor,
			Func<object> argumentAccessor = null,
			CommandExecuteBehavior behavior = CommandExecuteBehavior.Enabled)
			where TComponent : class, IBindableComponent
		{
			if (component == null) throw new ArgumentNullException(nameof(component));
			if (commandAccessor == null) throw new ArgumentNullException(nameof(commandAccessor));
			if (argumentAccessor == null)
			{
				argumentAccessor = () => null;
			}


			var command = commandAccessor();

			if (behavior == CommandExecuteBehavior.Enabled || behavior == CommandExecuteBehavior.EnabledAndVisible)
			{
				RemoveBinding(component, "Enabled");
				component.DataBindings.Add(
					"Enabled",
					command,
					nameof(command.CanExecute));
			}
			if (behavior == CommandExecuteBehavior.Visible || behavior == CommandExecuteBehavior.EnabledAndVisible)
			{
				RemoveBinding(component, "Visible");
				component.DataBindings.Add(
					"Visible",
					command,
					nameof(command.CanExecute));
			}



			if (component is ToolStripItem toolStripItem)
			{
				toolStripItem.Click += (s, e) => command.Execute(argumentAccessor());
			}
			if (component is Button button)
			{
				button.Click += (s, e) => command.Execute(argumentAccessor());
			}
		}



		public static void BindCollection<TItem>(this ListView listView, ObservableCollection<TItem> collection, Func<TItem, ListViewItem> itemFactory)
		{
			if (listView == null) throw new ArgumentNullException(nameof(listView));
			if (collection == null) throw new ArgumentNullException(nameof(collection));
			if (itemFactory == null) throw new ArgumentNullException(nameof(itemFactory));

			Action updateSource = () =>
			{
				listView.BeginUpdate();

				listView.Items.Clear();
				foreach (var element in collection)
				{
					var listViewItem = itemFactory(element);
					listView.Items.Add(listViewItem);
				}

				listView.EndUpdate();
			};

			collection.CollectionChanged += (s, e) =>
			{
				if (listView.InvokeRequired)
				{
					listView.BeginInvoke(updateSource);
					return;
				}

				updateSource();
			};
		}
	}
}
