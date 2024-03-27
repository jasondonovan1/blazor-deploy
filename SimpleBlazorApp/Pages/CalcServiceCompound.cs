using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SimpleBlazorApp.Pages
{


    public class CalcServiceCompound
    {
        public (decimal FinalAmount, decimal Profit, decimal TotalManagementCharges) CalculateSomething(decimal value, decimal periodic, int apr, int years, decimal managementChargeRate)
        {
            decimal amount = value; // Start with the initial principal
            decimal aprPer = apr / 100m;
            decimal totalManagementCharges = 0; // Initialize total management charges

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
            }

            decimal profit = amount - value - (periodic * years);
            // Adjust profit calculation if necessary, considering totalManagementCharges
            // For example, if management charges should not reduce profit directly,
            // adjust the profit formula accordingly.

            return (amount, profit, totalManagementCharges);
        }
    }

}
