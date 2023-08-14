using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MobileAppKyleGilstrap.DataHolders
{
    public class Term
    {
        [PrimaryKey, AutoIncrement]
        public int TermId { get; set; }
        public string TermName { get; set; }
        public DateTime TermStart { get; set; }
        public DateTime TermEnd { get; set; }
    }
}
