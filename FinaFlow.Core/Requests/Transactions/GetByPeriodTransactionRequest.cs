namespace FinaFlow.Core.Requests.Transactions;

public class GetByPeriodTransactionRequest : Request
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
