using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Threading.Tasks;
using Unity;
using StockMissingValueComplement.Stock.Complement.Business.Interface;
using StockMissingValueComplement.Stock.Complement.Business.Implement;
using StockMissingValueComplement.Stock.Complement.Tools;
using StockMissingValueComplement.Stock.Complement.Repository.Implement;
using StockMissingValueComplement.Stock.Complement.Repository.Interface;

namespace StockMissingValueComplement.Stock.Complement.Business.Test
{
    [TestFixture]
    public class ValueComplementBussinessTest
    {
        private IUnityContainer unityContainer;

        [TestCase]
        public void GetAllCurvesTest()
        {
            unityContainer = new UnityContainer();
            unityContainer.RegisterType<IValueComplementBusiness, ValueComplementBusinessImpl>();
            unityContainer.RegisterType<ICurveDao, CurveDaoMySQLImpl>();
            unityContainer.RegisterType<ICurveDataDao, CurveDataDaoMySQLImpl>();
            IValueComplementBusiness valueComplement = unityContainer.Resolve<IValueComplementBusiness>();
            Assert.DoesNotThrow(() => valueComplement.DoFitCalculation("600543.XSHG", "simplelinear"));
        }

    }
}
