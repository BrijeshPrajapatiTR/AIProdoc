using AIProdoc.Web.Models;

namespace AIProdoc.Web.Services
{
    public interface IBusinessRules
    {
        // Placeholder methods mapped from Clarion procedures during migration
        decimal CalculateTotalWithTax(decimal amount, decimal taxRatePercent);
        bool ValidateRecord(Record r, out string? error);
    }

    public class BusinessRules : IBusinessRules
    {
        public decimal CalculateTotalWithTax(decimal amount, decimal taxRatePercent)
        {
            var total = amount + (amount * (taxRatePercent/100m));
            return Math.Round(total, 2, MidpointRounding.AwayFromZero);
        }

        public bool ValidateRecord(Record r, out string? error)
        {
            if (string.IsNullOrWhiteSpace(r.Name)) { error = "Name is required"; return false; }
            if (r.Amount < 0) { error = "Amount cannot be negative"; return false; }
            error = null; return true;
        }
    }
}
