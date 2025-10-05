using System;
using System.Collections.Generic;

namespace Real_Estate_Agency.Models;

public partial class Deal
{
    public long DealId { get; set; }

    public int PropertyId { get; set; }

    public int OwnerId { get; set; }

    public int ClientId { get; set; }

    public int AgentId { get; set; }

    public DateOnly DealDate { get; set; }

    public decimal DealAmount { get; set; }

    public virtual Agent Agent { get; set; } = null!;

    public virtual Client Client { get; set; } = null!;

    public virtual Property Property { get; set; } = null!;
}
