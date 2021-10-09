using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicielNettoyagePC.Entities
{
    internal class History
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Action { get; set; } = null!;
        public string Remark { get; set; } = null!;
    }
}
