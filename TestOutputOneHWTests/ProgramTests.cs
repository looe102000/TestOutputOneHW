using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TestOutputOneHW.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        /// <summary>
        /// 該11筆資料，如果要3筆成一組，取得Cost的總和的話，預期結果是 6,15, 24, 21
        /// </summary>
        [TestMethod()]
        public void 測試_3組_Cost總和()
        {
            Model data = new Model();
            ITestOutputOneHW value = new TestValue<IList>(input: data.GetData("Cost"), count: 3, expect: "6,15,24,21");

            //預期的值
            var expect = value.Expect;
            //測試結果
            var actual = value.GetActual();

            Assert.AreEqual(expect, actual);
        }

        /// <summary>
        /// 該11筆資料，如果是4筆一組，取得 Revenue 總和的話，預期結果會是 50,66,60
        /// </summary>
        [TestMethod()]
        public void 測試_4組_Revenue總和()
        {
            Model data = new Model();
            ITestOutputOneHW value = new TestValue<IList>(input: data.GetData("Revenue"), count: 4, expect: "50,66,60");

            //預期的值
            var expect = value.Expect;
            //測試結果
            var actual = value.GetActual();

            Assert.AreEqual(expect, actual);
        }
    }

    /// <summary>
    /// 初始值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TestValue<T> : ITestOutputOneHW
    {
        private T _intput { get; set; }
        private int _count { get; set; }
        private string _expect { get; set; }
        public string Expect => _expect;

        public TestValue(T input, int count, string expect)
        {
            //筆數輸入負數，預期會拋 ArgumentException
            if (count < 0)
            {
                throw new ArgumentException();
            }

            _intput = input;
            _count = count;
            _expect = expect;
        }

        /// <summary>
        /// 取得結果
        /// </summary>
        /// <returns></returns>
        public string GetActual()
        {
            try
            {
                //筆數若輸入為0, 則傳回0
                if (_count == 0)
                    return "0";

                IList list = _intput as IList;
                IList<double> output = new List<double>();
                int Index = 1;
                double Sum = 0;

                foreach (var item in list)
                {
                    Sum += Convert.ToDouble(item);

                    if ((Index % _count) == 0 || (Index == list.Count))
                    {
                        output.Add(Sum);
                        Sum = 0;
                    }

                    Index++;
                }

                return string.Join(",", output);
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
    }

    public interface ITestOutputOneHW
    {
        string Expect { get; }
        string GetActual();
    }

    /// <summary>
    /// 資料源
    /// </summary>
    public class Model
    {
        private Dictionary<string, IList> Data = new Dictionary<string, IList>();

        //未來可能會新增其他欄位
        public Model()
        {
            Data.Add("Id", new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 });
            Data.Add("Cost", new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 });
            Data.Add("Revenue", new int[] { 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21 });
            Data.Add("SellPrice", new int[] { 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32 });
        }

        public IList GetData(string key)
        {
            if (Data.Keys.Contains(key))
            {
                return Data[key];
            }
            else
            {
                //尋找的欄位若不存在，預期會拋 ArgumentException
                throw new ArgumentException();
            }
        }
    }
}