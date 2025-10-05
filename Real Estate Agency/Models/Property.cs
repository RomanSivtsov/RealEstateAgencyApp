using System;
using System.Collections.Generic;

namespace Real_Estate_Agency.Models;

public partial class Property
{
    public int PropertyId { get; set; }

    public string PropertyAddress { get; set; } = null!;

    public string PropertyType { get; set; } = null!;

    public float PropertyArea { get; set; }

    public int PropertyRooms { get; set; }

    public decimal PropertyPrice { get; set; }

    public string? PropertyDescription { get; set; }

    public string PropertyPhoto { get; set; } = null!;

    public string PropertyStatus { get; set; } = null!;

    public int OwnerId { get; set; }

    public DateOnly PropertyCreatedAt { get; set; }

    public virtual ICollection<Deal> Deals { get; set; } = new List<Deal>();

    public virtual Owner Owner { get; set; } = null!;

    public virtual ICollection<Showing> Showings { get; set; } = new List<Showing>();
}
