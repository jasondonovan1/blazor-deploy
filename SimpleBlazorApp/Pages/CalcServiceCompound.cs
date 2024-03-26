using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SimpleBlazorApp.Pages
{

	
	public class CalcServiceCompund
	{
		public (decimal Amount, decimal Profit) CalculateSomething(decimal value, decimal periodic, int apr, int year)
		{
			decimal amount = value; // Start with the initial principal
			decimal aprPer = apr / 100m;
		    
					
			for (int i = 0; i < year; i++)
			{
				// Compound the current amount by the growth rate
				amount *= (1 + aprPer);

				// Then add the periodic payment, which will compound in subsequent years
				// No need to adjust periodic here; if it's <= 0, it simply won't add to the amount
				amount += periodic;
			}
			decimal profit = amount - value - (periodic*year);
			
			return (amount, profit); // Return both amount and profit as a tuple
		}

	}
}
