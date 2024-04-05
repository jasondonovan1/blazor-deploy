using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Generic;

namespace SimpleBlazorApp.Pages
{


    public class CalcServiceCompound
    {
        public (decimal FinalAmount, decimal Profit, decimal TotalManagementCharges, List<decimal> YearProfit) CalculateSomething(decimal value, decimal periodic, int apr, int years, decimal managementChargeRate)
        {
            decimal amount = value; // Start with the initial principal
            decimal aprPer = apr / 100m;
            decimal totalManagementCharges = 0; // Initialize total management charges
            List<decimal> yearAmountList = new List<decimal>();  // declare a list to store the profit values for each year
           
            yearAmountList.Clear(); //clear the list on Run
			yearAmountList.Insert(0, 0);// place a 0 into position 0, all other values start from year 1...year n

			for (int i = 0; i < years; i++)
            {
                
                // Compound the current amount by the growth rate
                amount *= (1 + aprPer);

                // Then add the periodic payment, which will compound in subsequent years
                amount += periodic;
                              

                // Calculate and deduct the management charge at the end of each year
                decimal annualManagementCharge = amount * managementChargeRate/100;
                amount -= annualManagementCharge;
                totalManagementCharges += annualManagementCharge; // Accumulate total management charges

				decimal yearProfit = amount - value - (periodic * years);
				// add the profit value to a list and return it
				yearAmountList.Insert(i+1, yearProfit);
			}

            decimal profit = amount - value - (periodic * years);
			// Adjust profit calculation if necessary, considering totalManagementCharges
			// For example, if management charges should not reduce profit directly,
			// adjust the profit formula accordingly.
                      	

			return (amount, profit, totalManagementCharges, yearAmountList);
        }
    }

}
