using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SimpleBlazorApp.Pages
{


	public class CalcServiceWithdrawal
	{
		public (decimal EndBalance, string YearsOrNever, decimal TotalWithdrawn, decimal TotalManagementCharges) CalculateWithdrawalPeriod(decimal startingBalance, decimal monthlyWithdrawal, int apr, int years, decimal annualManagementFeePercentage)
		{
			decimal monthlyRate = apr / 100m / 12;
			decimal balance = startingBalance;
			decimal totalWithdrawn = 0; // Initialize total withdrawn
			decimal totalManagementCharges = 0; // Initialize total management charges
			int totalMonths = years * 12;
			int monthWhenOutOfFunds = 0; // Keep track of the month when funds run out

			for (int month = 1; month <= totalMonths; month++)
			{
				balance -= monthlyWithdrawal;
				totalWithdrawn += monthlyWithdrawal; // Increase total withdrawn

				balance += balance * monthlyRate; // Apply growth

				// Deduct management fee at the end of each year, after growth
				if (month % 12 == 0 && annualManagementFeePercentage > 0)
				{
					decimal annualFee = balance * (annualManagementFeePercentage / 100);
					balance -= annualFee;
					totalManagementCharges += annualFee; // Accumulate total management charges
				}

				if (balance < 0)
				{
					monthWhenOutOfFunds = month;
					balance = 0; // if the end balance is Negative then set to Zero - cant take out what you dont have
					break;
				}
			}

			string yearsOrNever = monthWhenOutOfFunds == 0 ? "never" :
				((decimal)monthWhenOutOfFunds / 12).ToString("N2") + " years";

			return (balance, yearsOrNever, totalWithdrawn, totalManagementCharges);
		}
	}

}

