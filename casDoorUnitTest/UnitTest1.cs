using System;
using System.Collections.Generic;
using Casdoor.Client;
using Casdoor.Client.Utils;
using Xunit;

namespace casDoorUnitTest
{
    public class UnitTest1
    {
        public static IEnumerable<object[]> test1Plate()
        {
            return new List<object[]>{
                new object[]
                {
                    new List<KeyValuePair<string,object>>
                    {
                        new KeyValuePair<string,object>("name","sans"),
                        new KeyValuePair<string, object>("owner", "komi"),
                        new KeyValuePair<string, object>("Location", null!)
                    },
                }
            };
        }

        [Theory]
        [MemberData(nameof(test1Plate))]
        public void Test1(List<KeyValuePair<string, object>> types)
        {
            try
            {
                Assert.True(CastUtil.classify(types, new CasdoorUser()) is CasdoorUser);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
