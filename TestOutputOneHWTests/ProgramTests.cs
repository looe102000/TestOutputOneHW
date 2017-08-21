using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestOutputOneHW.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        public void 測試_3組_Cost總和()
        {
            Model data = new Model();

            GetValue<IList> value = new GetValue<IList>(input:data.GetData("Cost"), count:3, expect: "6,15,24,21");

            var expect = value._expect;
            var actual = value.GetExpect();


            Assert.AreEqual(expect, actual);
        }
        [TestMethod()]
        public void 測試_4組_Revenue總和()
        {
            Model data = new Model();

            GetValue<IList> value = new GetValue<IList>(input: data.GetData("Revenue"), count: 4, expect: "50,66,60");

            var expect = value._expect;
            var actual = value.GetExpect();


            Assert.AreEqual(expect, actual);
        }
    }

    /// <summary>
    /// 初始值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GetValue<T>
    {
        private T _intput { get; set; }
        private int _count { get; set; }
        public string _expect { get; set; }

        public GetValue(T input, int count, string expect)
        {
            if(count < 0)
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
        public string GetExpect()
        {
            try
            {
                if(_count == 0)
                    return "0";

                IList list = _intput as IList;

                List<double> output = new List<double>();

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
            catch(Exception e)
            {
                return e.ToString();
            }

        }
    }

    /// <summary>
    /// 資料源
    /// </summary>
    public class Model
    {
        Dictionary<string, IList> Data = new Dictionary<string, IList>();
        public Model()
        {
            Data.Add("Id", new int[] { 1,2,3,4,5,6,7,8,9,10,11});
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
                throw new ArgumentException();
            }
            
        }
    }
}