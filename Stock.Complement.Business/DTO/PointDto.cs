using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMissingValueComplement.Stock.Complement.Business.DTO
{
    public class PointDto
    {
        public int X { get; set; }
        public double Y { get; set; }

        public PointDto(int x = 0, double y = 0.0)
        {
            X = x;
            Y = y;
        }
    }
}
