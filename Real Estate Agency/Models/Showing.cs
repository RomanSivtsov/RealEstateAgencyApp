using System;
using System.Collections.Generic;

namespace Real_Estate_Agency.Models;

public partial class Showing
{
    public long ShowingId { get; set; }

    public int PropertyId { get; set; }

    public int ClientId { get; set; }

    public int AgentId { get; set; }

    public DateTime ShowingDateTime { get; set; }

    public string ShowingResult { get; set; } = null!;

    public virtual Agent Agent { get; set; } = null!;

    public virtual Client Client { get; set; } = null!;

    public virtual Property Property { get; set; } = null!;
}
