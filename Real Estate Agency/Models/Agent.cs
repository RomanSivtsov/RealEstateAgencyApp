using System;
using System.Collections.Generic;

namespace Real_Estate_Agency.Models;

public partial class Agent
{
    public int AgentId { get; set; }

    public string AgentName { get; set; } = null!;

    public string AgentContacts { get; set; } = null!;

    public string? AgentRequirements { get; set; }

    public virtual ICollection<Deal> Deals { get; set; } = new List<Deal>();

    public virtual ICollection<Showing> Showings { get; set; } = new List<Showing>();
}
