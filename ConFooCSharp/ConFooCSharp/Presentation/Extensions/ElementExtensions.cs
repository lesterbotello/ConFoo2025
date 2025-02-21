using System.Runtime.CompilerServices;
using ConFooCSharp.Behaviors;

namespace ConFooCSharp.Presentation.Extensions;

public static class ElementExtensions
{
	public static TextBox TextBoxCommand(this TextBox element, Func<IAsyncCommand> propertyBinding, [CallerArgumentExpression("propertyBinding")] string? propertyBindingExpression = null)
    {
		Func<IAsyncCommand> propertyBinding2 = propertyBinding;
		string propertyBindingExpression2 = propertyBindingExpression;

		return TextBoxCommand(element, delegate (IDependencyPropertyBuilder<ICommand> _)
		{
			_.Binding(propertyBinding2, propertyBindingExpression2);
		});
	}

	public static TextBox TextBoxCommand(this TextBox element, Action<IDependencyPropertyBuilder<ICommand>> configureProperty)
	{
		DependencyPropertyBuilder<ICommand> instance = DependencyPropertyBuilder<ICommand>.Instance;
		configureProperty(instance);
		instance.SetBinding(element, CommandOnKeyPressBehavior.TextBoxCommandProperty, "TextBoxCommand");
		return element;
	}
}
