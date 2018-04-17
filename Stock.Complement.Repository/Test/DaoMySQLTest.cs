using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Threading.Tasks;
using StockMissingValueComplement.Stock.Complement.Tools;
using StockMissingValueComplement.Stock.Complement.Repository.Implement;
using StockMissingValueComplement.Stock.Complement.Repository.Interface;

namespace StockMissingValueComplement.Stock.Complement.Repository.Test
{

    [TestFixture]
    public class DaoMySQLTest
    {
        [TestCase]
        public void GetAllCurvesTest()
        {
            ICurveDao curveDao = new CurveDaoMySQLImpl();
            Assert.IsFalse(ListHelper.IsNullOrEmpty(curveDao.GetAllCurves()));
        }

        [TestCase]
        public void GetCurveByNameTest()
        {
            ICurveDao curveDao = new CurveDaoMySQLImpl();
            Assert.IsNotNull(curveDao.GetCurveByName("600543.XSHG"));
        }

        [TestCase]
        public void GetCurveDataByCurveIdTest()
        {
            ICurveDataDao curveDao = new CurveDataDaoMySQLImpl();
            Assert.IsFalse(ListHelper.IsNullOrEmpty(curveDao.GetCurveDataByCurveId(1)));
        }
    }
}
