using System;
using System.Collections.Generic;

namespace KolokwiumCF.Models​;

public partial class Sale
{
    public int IdSale { get; set; }

    public int IdClient { get; set; }

    public int IdSubscription { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Subscription IdSubscriptionNavigation { get; set; } = null!;
}
