using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace StockMissingValueComplement.Stock.Complement.Business.Enums
{
    public enum DBTypeEnum
    {
        [Description("mysql database")]
        MySql = 1,

        [Description("sql server database")]
        SQLSERVER = 2
    }

    public static class DBTypeOperation 
    {
        public static int GetValue(this DBTypeEnum dbType)
        {
            return (int)dbType;
        }
    }
}
