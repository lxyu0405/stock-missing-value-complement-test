using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockMissingValueComplement.Stock.Complement.Repository.Entity;
using StockMissingValueComplement.Stock.Complement.Business.Interface;
using StockMissingValueComplement.Stock.Complement.Business.DTO;
using StockMissingValueComplement.Stock.Complement.Business.Factory;
using StockMissingValueComplement.Stock.Complement.Repository.Interface;
using StockMissingValueComplement.Stock.Complement.Repository.Implement;
using StockMissingValueComplement.Stock.Complement.Tools;

namespace StockMissingValueComplement.Stock.Complement.Business.Implement
{
    public class ValueComplementBusinessImpl : IValueComplementBusiness
    {
        private readonly ICurveDao _iCurveDao;
        private readonly ICurveDataDao _iCurveDataDao;

        public ValueComplementBusinessImpl(ICurveDao iCurveDao
            , ICurveDataDao iCurveDataDao)
        {
            this._iCurveDao = iCurveDao;
            this._iCurveDataDao = iCurveDataDao;
        }


        #region Find missing value and do complementation
        public void DoFitCalculation(string stockName, string fitMethod)
        {
            CurveEntity curveInfo = this._iCurveDao.GetCurveByName(stockName);
            if(curveInfo == null)
            {
                LogHelper.Warn(typeof(ValueComplementBusinessImpl), "Cannot find certain stock information");
                return;
            }
            List<CurveDataEntity> curveDataList = this._iCurveDataDao.GetCurveDataByCurveId(curveInfo.CurveId);
            if(ListHelper.IsNullOrEmpty(curveDataList))
            {
                LogHelper.Warn(typeof(ValueComplementBusinessImpl), "Stock " + curveInfo.CurveName + " doesnot have any data");
                return;
            }
            int missIdx = FindMissingValueIdx(curveDataList);
            CurveDataEntity missingCurveDataEntity = curveDataList[missIdx];
            List<PointDto> modelPoints = ConvertValueItemsToPoints(curveDataList);
            if (missIdx != -1 && !ListHelper.IsNullOrEmpty(modelPoints))
            {
                IFitStrategy fitStrategy = StrategyFactory.GetStrategyByName(fitMethod);
                PointDto fitResultPoint = fitStrategy.FitCalculation(modelPoints, missIdx);
                missingCurveDataEntity.Value = System.Convert.ToDecimal(fitResultPoint.Y);
                //更新至db
                this._iCurveDataDao.Update(missingCurveDataEntity);
            }
        }


        //Dto转换
        private List<PointDto> ConvertValueItemsToPoints(List<CurveDataEntity> curveDataList)
        {
            List<PointDto> results = new List<PointDto>();
            if(ListHelper.IsNullOrEmpty(curveDataList))
            {
                return results;
            }
            CurveDataEntity firstEntity = curveDataList[0];
            DateTime firstDate = DateTime.ParseExact(firstEntity.EffectiveDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            foreach(CurveDataEntity curveData in curveDataList)
            {
                DateTime thisDate = DateTime.ParseExact(curveData.EffectiveDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                PointDto newPoint = new PointDto()
                {
                    X = DaysDiff(firstDate, thisDate),
                    Y = System.Convert.ToDouble(curveData.Value)
                };
                results.Add(newPoint);
            }
            return results;
        }

        private int DaysDiff(DateTime DateTime1, DateTime DateTime2)
        {
            TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            return ts.Days;
        }


        // 寻找空值位置
        private int FindMissingValueIdx(List<CurveDataEntity> curveDataList)
        {
            int missingIdx = -1;
            if (ListHelper.IsNullOrEmpty(curveDataList))
            {
                return missingIdx;
            }
            for (int i = 0; i < curveDataList.Count; i++)
            {
                if (System.Convert.ToDouble(curveDataList[i].Value) == 0.0)
                {
                    missingIdx = i;
                    break;
                }
            }
            return missingIdx;
        }
        #endregion




        #region Get stock names in db
        public string GetStoredStockNames() 
        {
            List<CurveEntity> curves = this._iCurveDao.GetAllCurves();
            if (ListHelper.IsNullOrEmpty(curves))
            {
                return "";
            }
            List<string> curveNames = curves.Select(cur => cur.CurveName).ToList();
            return String.Join(", ", curveNames.ToArray());
        }
        #endregion
    }
}
