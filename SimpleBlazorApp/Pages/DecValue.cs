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

		protected override void OnParametersSet() // Sets the initial values to display
		{
			_displayValue = Value.ToString("N2", CultureInfo.InvariantCulture);
		}

		[Parameter]
		public string Class { get; set; }

		[Parameter]
		public string LabelText { get; set; }

		private string errorMessage;

		protected override void BuildRenderTree(RenderTreeBuilder builder)
		{
			builder.OpenElement(0, "div"); // Encapsulate label and input in a div
			builder.AddAttribute(1, "class", "input-group");
			builder.OpenElement(2, "span"); // Container for the label
			builder.AddContent(3, LabelText); // Static text
			builder.CloseElement();
			builder.OpenElement(4, "input");
			builder.AddAttribute(5, "type", "text"); // Ensure input type is number
			builder.AddAttribute(6, "class", Class); // Apply the CSS class
			builder.AddAttribute(7, "value", _displayValue); // Bind the displayed value
			builder.AddAttribute(8, "step", "any"); // Allow decimal input to two decimal places
			builder.AddAttribute(9, "oninput", EventCallback.Factory.CreateBinder<string>(this, __value => _displayValue = __value, _displayValue));
			
			
			//builder.AddAttribute(10, "onfocus", EventCallback.Factory.Create(this, () => _displayValue = "")); // clear the entry
			builder.AddAttribute(11, "onblur", EventCallback.Factory.Create(this, OnBlur));
			builder.AddAttribute(12, "min", "1"); //min value
			builder.CloseElement(); // Close input


			// Conditionally display an error message
			if (!string.IsNullOrEmpty(errorMessage))
			{
				builder.OpenElement(13, "span");
				builder.AddAttribute(14, "class", "error-message"); // A CSS class for styling
				builder.AddContent(15, errorMessage);
				builder.CloseElement();
			}

			builder.CloseElement(); // Close flex container
		}

		private void OnBlur()
		{
			

			if (decimal.TryParse(_displayValue, NumberStyles.Any, CultureInfo.InvariantCulture, out var result) && result >= 0)
			{
				Value = result;
				_displayValue = Value.ToString("N2", CultureInfo.InvariantCulture);
				errorMessage = "";
				ValueChanged.InvokeAsync(Value);
			}
			else
			{
				// Set error message for non-numeric or negative values
				errorMessage = "Please enter a valid numeric value.";
				// Handle the case where _displayValue is not a valid decimal
				_displayValue = "0.00"; // Example fallback, adjust as necessary
			}
			 // It might be moved inside the if block, depending on whether you want to notify about invalid inputs
		}
	}
}
