using System;
using System.Collections.Generic;

namespace Real_Estate_Agency;

public partial class ViewAgentSalesReport
{
    public int AgentId { get; set; }

    public string AgentName { get; set; } = null!;

    public int? TotalDeals { get; set; }

    public decimal? TotalSalesAmount { get; set; }

    public decimal? AverageDealAmount { get; set; }
}
