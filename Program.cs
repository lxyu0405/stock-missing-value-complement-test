using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity;
using System.Threading.Tasks;
using StockMissingValueComplement.Stock.Complement.Tools;
using StockMissingValueComplement.Stock.Complement.Business.Interface;
using StockMissingValueComplement.Stock.Complement.Business.Implement;
using StockMissingValueComplement.Stock.Complement.Repository.Interface;
using StockMissingValueComplement.Stock.Complement.Repository.Implement;

namespace StockMissingValueComplement
{
    class Program
    {

        public static void InitilizeIOCContainer(string dbType, IUnityContainer container)
        {
            container.RegisterType<IValueComplementBusiness, ValueComplementBusinessImpl>();
            if("mysql".Equals(dbType, StringComparison.InvariantCultureIgnoreCase))
            {
                container.RegisterType<ICurveDao, CurveDaoMySQLImpl>();
                container.RegisterType<ICurveDataDao, CurveDataDaoMySQLImpl>();
            }
            else if("sqlserver".Equals(dbType, StringComparison.InvariantCultureIgnoreCase))
            {
                container.RegisterType<ICurveDao, CurveDaoSqlServerImpl>();
                container.RegisterType<ICurveDataDao, CurveDataDaoSqlServerImpl>();
            }

        }

        static void Main(string[] args)
        {
            LogHelper.Info(typeof(Program), "++++++ Program Start ++++++");

            //sql server方法还未实现
            string dbType = "mysql";

            IUnityContainer container = new UnityContainer();
            InitilizeIOCContainer(dbType, container);

            IValueComplementBusiness valueComplement = container.Resolve<IValueComplementBusiness>();
            string accessibleStocks = valueComplement.GetStoredStockNames();
            //simplelinear, regressionlinear
            string fitMethods = "regressionlinear";
            //600543.XSHG, 601007.XSHG
            string stockName = "600543.XSHG";

            LogHelper.Info(typeof(Program), "Fit Method: " + fitMethods + ", Stock Name: " + stockName);
            valueComplement.DoFitCalculation(stockName, fitMethods);

            LogHelper.Info(typeof(Program), "++++++ Program End ++++++");
        }
    }
}
