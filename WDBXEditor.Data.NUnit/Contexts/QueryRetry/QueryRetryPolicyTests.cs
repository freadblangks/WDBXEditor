using Acmil.Data.Constants;
using Acmil.Data.Contexts.QueryRetry;
using NUnit.Framework;
using System;
using System.Linq;
using WDBXEditor.Data.NUnit.TestHelpers;

namespace WDBXEditor.Data.NUnit.Contexts.QueryRetry
{
	[TestFixture]
	public class QueryRetryPolicyTests
	{
		[Test]
		public void QueryRetryPolicy_RetryWithoutQueryHints_CorrectlyRemovesMultipleHintsFromSingleLine()
		{
			// ARRANGE //
			string sql = "SELECT COUNT(*) FROM t1 JOIN t2 JOIN t3 WHERE t1.f1 IN (SELECT /*+ QB_NAME(subq1) */ f1 FROM t4) AND t2.f1 IN (SELECT /*+ QB_NAME(subq2) */ f1 FROM t5);";
			string expectedUpdatedSqlPattern = "SELECT COUNT\\(\\*\\) FROM t1 JOIN t2 JOIN t3 WHERE t1\\.f1 IN \\(SELECT[\\s]+f1 FROM t4\\) AND t2\\.f1 IN \\(SELECT[\\s]+f1 FROM t5\\);";

			string actualUpdatedSql = "";
			Func<string, int> retryQuery = updatedText =>
			{
				actualUpdatedSql = updatedText;
				return 0;
			};

			var sut = new QueryRetryPolicy<int>(() => { throw GetQueryHintException(); }, new QueryRetryConfiguration());
			sut.RetryWithoutQueryHints(sql, retryQuery);

			// ACT //
			sut.RunQuery();

			// ASSERT //
			Assert.False(string.IsNullOrWhiteSpace(actualUpdatedSql), "Expected retried query text to be populated.");
			StringAssert.IsMatch(expectedUpdatedSqlPattern, actualUpdatedSql, "Expected retried query to be the same as the original minus query hints.");
		}

		[Test]
		public void QueryRetryPolicy_RetryWithoutQueryHints_CorrectlyRemovesMultipleHintsFromMultipleLines()
		{
			// ARRANGE //
			string sql = @"SELECT
			/*+ JOIN_PREFIX(t2, t5@subq2, t4@subq1)
				JOIN_ORDER(t4@subq1, t3)
				JOIN_SUFFIX(t1) */
			COUNT(*) FROM t1 JOIN t2 JOIN t3
				WHERE t1.f1 IN (SELECT /*+ QB_NAME(subq1) */ f1 FROM t4)
					AND t2.f1 IN (SELECT /*+ QB_NAME(subq2) */ f1 FROM t5);";

			string expectedUpdatedSqlPattern = "(?m)SELECT[\\s\\n\\r]*?COUNT\\(\\*\\) FROM t1 JOIN t2 JOIN t3[\\s\\n\\r]*?WHERE t1\\.f1 IN \\(SELECT[\\s]+f1 FROM t4\\)[\\s\\n\\r]*?AND t2.f1 IN \\(SELECT[\\s]+f1 FROM t5\\);";

			string actualUpdatedSql = "";
			Func<string, int> retryQuery = updatedText =>
			{
				actualUpdatedSql = updatedText;
				return 0;
			};

			var sut = new QueryRetryPolicy<int>(() => { throw GetQueryHintException(); }, new QueryRetryConfiguration());
			sut.RetryWithoutQueryHints(sql, retryQuery);

			// ACT //
			sut.RunQuery();

			// ASSERT //
			Assert.False(string.IsNullOrWhiteSpace(actualUpdatedSql), "Expected retried query text to be populated.");
			StringAssert.IsMatch(expectedUpdatedSqlPattern, actualUpdatedSql, "Expected retried query to be the same as the original minus query hints.");
		}

		

		private static Exception GetQueryHintException() => MySqlExceptionBuilder.CreateException("buh", MySqlConstants.QUERY_HINT_ERROR_CODES.ToList()[0], new Exception());
	}
}
