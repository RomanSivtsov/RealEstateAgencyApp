using System;
using System.Collections.Generic;

namespace Real_Estate_Agency;

public partial class ViewAllDeal
{
    public long DealId { get; set; }

    public DateOnly DealDate { get; set; }

    public decimal DealAmount { get; set; }

    public int PropertyId { get; set; }

    public string PropertyAddress { get; set; } = null!;

    public string PropertyType { get; set; } = null!;

    public float PropertyArea { get; set; }

    public int PropertyRooms { get; set; }

    public decimal PropertyPrice { get; set; }

    public string PropertyStatus { get; set; } = null!;

    public int OwnerId { get; set; }

    public string OwnerName { get; set; } = null!;

    public string OwnerContacts { get; set; } = null!;

    public int ClientId { get; set; }

    public string ClientName { get; set; } = null!;

    public string ClientContacts { get; set; } = null!;

    public int AgentId { get; set; }

    public string AgentName { get; set; } = null!;

    public string AgentContacts { get; set; } = null!;
}
