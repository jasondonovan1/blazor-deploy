using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components.Forms;
using System.Globalization;

namespace SimpleBlazorApp.Pages
{
	public class NumericInputInt : ComponentBase
	{
		private string _displayValue;

		[Parameter]
		public int Value { get; set; }

		[Parameter]
		public EventCallback<int> ValueChanged { get; set; }

		protected override void OnParametersSet() // Sets the initial values to display
		{
			_displayValue = Value.ToString("N0", CultureInfo.InvariantCulture);
		}

		[Parameter]
		public string Class { get; set; }

		[Parameter]
		public string LabelText { get; set; }

		private string NumberYearsFormatted => Value.ToString("N0");

		protected override void BuildRenderTree(RenderTreeBuilder builder)
		{
			builder.OpenElement(0, "div"); // Encapsulate label and input in a div
			builder.AddAttribute(1, "class", "input-group");
			builder.OpenElement(2, "span"); // Container for the label
			builder.AddContent(3, LabelText); // Static text
			builder.CloseElement();
			builder.OpenElement(4, "input");
			builder.AddAttribute(5, "type", "number"); // Ensure input type is number
			builder.AddAttribute(6, "class", Class); // Apply the CSS class
			builder.AddAttribute(7, "value", _displayValue);
			builder.AddAttribute(8, "oninput", EventCallback.Factory.CreateBinder<string>(this, __value => _displayValue = __value, _displayValue));
			builder.AddAttribute(9, "onblur", EventCallback.Factory.Create(this, OnBlur));
			
			builder.AddAttribute(10, "onfocus", EventCallback.Factory.Create(this, () => _displayValue = "1"));
			builder.AddAttribute(11, "min", "1");
			builder.CloseElement(); // Close input

		    builder.CloseElement(); // Close flex container
		}

		private void OnBlur()
		{
			if (int.TryParse(_displayValue, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
			{
				if ((result <= 0) || (result >99)) result = 1; // Adjust negative or large number values to 1
				Value = result; // Update the numeric value
				_displayValue = Value.ToString("N0", CultureInfo.InvariantCulture); // Format for display
				ValueChanged.InvokeAsync(Value); // Notify of value change
			}
		}
	}
}
