using System;
using System.Collections.Generic;

namespace KolokwiumCF.Models​;

public partial class Payment
{
    public int IdPayment { get; set; }

    public DateTime Date { get; set; }

    public int IdClient { get; set; }

    public int IdSubscription { get; set; }

    public decimal Value { get; set; }

    public virtual Subscription IdSubscriptionNavigation { get; set; } = null!;
}
