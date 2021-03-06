﻿using ITCO.SboAddon.Framework.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ITCO.SboAddon.Framework.Tests
{
    [TestClass]
    public class HanaTranslatorTests
    {
        [TestMethod]
        public void ConvertSqlToHana_Test()
        {
            var hanaQuery = SimpleHanaTranslator.ConvertSqlToHana(
                "SELECT TOP(123) ISNULL([ABC], '') + '-' + [DEF], " +
                "[IntVal] + 5 AS [IntValPlus], " +
                "DATEDIFF(DAY, '2017-01-01', GETDATE()) AS [DateTime] FROM [T0], " +
                "DATEDIFF(SECOND, '2017-01-01', GETDATE()) AS [DateTime] FROM [T0] " +
                "FROM (SELECT 'X' AS [DUMMY]) [T] " +
                "JOIN [@UDT] " +
                "WHERE [%0] = '123' AND T.[Variable]=@Variable");

            Assert.AreEqual(
                "SELECT IFNULL(\"ABC\", '') || '-' || \"DEF\", " +
                "\"IntVal\" + 5 AS \"IntValPlus\", " +
                "DAYS_BETWEEN( '2017-01-01', NOW()) AS \"DateTime\" FROM \"T0\", " +
                "SECONDS_BETWEEN( '2017-01-01', NOW()) AS \"DateTime\" FROM \"T0\" " + 
                "FROM \"DUMMY\" " +
                "JOIN \"@UDT\" " +
                "WHERE [%0] = '123' AND T.\"Variable\"=:Variable" + 
                " LIMIT 123",
                hanaQuery);
        }
    }
}
