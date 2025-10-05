using System;
using System.Collections.Generic;

namespace Real_Estate_Agency.Models;

public partial class Owner
{
    public int OwnerId { get; set; }

    public string OwnerName { get; set; } = null!;

    public string OwnerContacts { get; set; } = null!;

    public virtual ICollection<Property> Properties { get; set; } = new List<Property>();
}
