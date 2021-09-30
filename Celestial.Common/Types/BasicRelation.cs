using System;
using System.Collections.Generic;
using System.Text;

namespace Celestial.Common.Types
{
    public class BasicRelation<TOwner, TItem>
    {
        public TOwner Owner { get; set; }
        public TItem Item { get; set; }
    }

    public class HistoricRelation<TOwner, TItem> : BasicRelation<TOwner, TItem>
    {
        public int Ordinal { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ThruDate { get; set; }
    }
}
