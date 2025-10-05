using System;
using System.Collections.Generic;

namespace Real_Estate_Agency.Models;

public partial class Client
{
    public int ClientId { get; set; }

    public string ClientName { get; set; } = null!;

    public string ClientContacts { get; set; } = null!;

    public string ClientRequirements { get; set; } = null!;

    public virtual ICollection<Deal> Deals { get; set; } = new List<Deal>();

    public virtual ICollection<Showing> Showings { get; set; } = new List<Showing>();
}
