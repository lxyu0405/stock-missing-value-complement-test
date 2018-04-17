using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMissingValueComplement.Stock.Complement.Repository.Entity
{
    public class CurveEntity
    {
        public long CurveId { get; set; }

        public string CurveName { get; set; }

        public DateTime DataChange_CreateTime { get; set; }

        public DateTime DataChange_LastTime { get; set; }
    }
}
