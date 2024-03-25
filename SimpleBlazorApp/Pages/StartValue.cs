using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components.Forms;
using System.Globalization;

namespace SimpleBlazorApp.Pages
{
	public class NumericInput : ComponentBase
	{
		private string _displayValue;

		[Parameter]
		public decimal Value { get; set; }

		[Parameter]
		public EventCallback<decimal> ValueChanged { get; set; }

		protected override void OnParametersSet()
		{
			_displayValue = Value.ToString("N3", CultureInfo.InvariantCulture);
		}

		[Parameter]
		public string Class { get; set; }

		[Parameter]
		public string LabelText { get; set; }

		protected override void BuildRenderTree(RenderTreeBuilder builder)
		{
			builder.OpenElement(0, "div"); // Encapsulate label and input in a div
			builder.AddAttribute(1, "class", "input-group");
			builder.OpenElement(2, "span"); // Container for the label
			builder.AddContent(3, LabelText); // Static text
			builder.CloseElement();
			builder.OpenElement(4, "input");
			builder.AddAttribute(5, "class", Class); // Apply the CSS class
			builder.AddAttribute(6, "value", _displayValue);
			builder.AddAttribute(7, "oninput", EventCallback.Factory.CreateBinder<string>(this, __value => _displayValue = __value, _displayValue));
			builder.AddAttribute(8, "onblur", EventCallback.Factory.Create(this, () =>
			{ 
			  if (decimal.TryParse(_displayValue, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
				{
					Value = result;
					ValueChanged.InvokeAsync(Value);
					_displayValue = Value.ToString("N2", CultureInfo.InvariantCulture);
				}
			}));
			builder.AddAttribute(7, "onfocus", EventCallback.Factory.Create(this, () => _displayValue = ""));
			builder.AddAttribute(8, "min", "0");

			builder.CloseElement(); // Close input
			builder.CloseElement(); // Close flex container
		}
	}
}
