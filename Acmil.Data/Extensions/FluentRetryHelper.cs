using Acmil.Data.Contexts.QueryRetry;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Acmil.Data.Extensions
{
	public static class FluentRetryHelper
	{
		#region WrapWithFailover

		public static Action WrapWithFailover(this Action baseLogic, Action failoverLogic, Func<Exception, bool> checkToFailOver)
		{
			return new Action(() => RetryHelper.TryWithFailover(baseLogic, failoverLogic, checkToFailOver));
		}

		public static Func<Task> WrapWithFailoverAsync(this Func<Task> baseLogic, Func<Task> failoverLogic, Func<Exception, bool> checkToFailover, CancellationToken cancellationToken)
		{
			return new Func<Task>(() => RetryHelper.TryWithFailoverAsync(baseLogic, failoverLogic, checkToFailover, cancellationToken));
		}

		public static Func<T> WrapWithFailover<T>(this Func<T> baseLogic, Func<T> failoverLogic, Func<Exception, bool> checkToFailover)
		{
			return new Func<T>(() => RetryHelper.TryWithFailover(baseLogic, failoverLogic, checkToFailover));
		}

		public static Func<Task<T>> WrapWithFailoverAsync<T>(this Func<Task<T>> baseLogic, Func<Task<T>> failoverLogic, Func<Exception, bool> checkToFailover, CancellationToken cancellationToken)
		{
			return new Func<Task<T>>(() => RetryHelper.TryWithFailoverAsync(baseLogic, failoverLogic, checkToFailover, cancellationToken));
		}

		#endregion

		#region WrapWithRetryLogic

		public static Action WrapWithBasicRetry(this Action func, Func<int> retries, Func<Exception, bool> retryLogic, Func<Exception, IEnumerable<Exception>, Exception> exceptionAggregator, Func<int, TimeSpan> waitLogic)
		{
			return new Action(() => RetryHelper.BasicRetry(func, retries, retryLogic, exceptionAggregator, waitLogic));
		}

		public static Action WrapWithBasicRetry(this Action func, Func<int> retries, Func<Exception, bool> retryLogic, Func<Exception, IEnumerable<Exception>, Exception> exceptionAggregator)
		{
			return new Action(() => RetryHelper.BasicRetry(func, retries(), retryLogic, exceptionAggregator));
		}

		public static Func<Task> WrapWithBasicRetryAsync(this Func<Task> func, Func<int> retries, Func<Exception, bool> retryLogic, Func<Exception, IEnumerable<Exception>, Exception> exceptionAggregator, Func<int, TimeSpan> waitLogic, CancellationToken cancelToken)
		{
			return new Func<Task>(() => RetryHelper.BasicRetryAsync(func, retries, retryLogic, exceptionAggregator, waitLogic, cancelToken));
		}

		public static Func<Task> WrapWithBasicRetryAsync(this Func<Task> func, Func<int> retries, Func<Exception, bool> retryLogic, Func<Exception, IEnumerable<Exception>, Exception> exceptionAggregator, CancellationToken cancelToken)
		{
			return new Func<Task>(() => RetryHelper.BasicRetryAsync(func, retries(), retryLogic, exceptionAggregator, cancelToken));
		}

		public static Func<T> WrapWithBasicRetry<T>(this Func<T> func, Func<int> retries, Func<Exception, bool> retryLogic, Func<Exception, IEnumerable<Exception>, Exception> exceptionAggregator, Func<int, TimeSpan> waitLogic)
		{
			return new Func<T>(() => RetryHelper.BasicRetry(func, retries, retryLogic, exceptionAggregator, waitLogic));
		}

		public static Func<T> WrapWithBasicRetry<T>(this Func<T> func, int retries, Func<Exception, bool> retryLogic, Func<Exception, IEnumerable<Exception>, Exception> exceptionAggregator)
		{
			return new Func<T>(() => RetryHelper.BasicRetry(func, retries, retryLogic, exceptionAggregator));
		}

		public static Func<Task<T>> WrapWithBasicRetryAsync<T>(Func<Task<T>> func, Func<int> retries, Func<Exception, bool> retryLogic, Func<Exception, IEnumerable<Exception>, Exception> exceptionAggregator, Func<int, TimeSpan> waitLogic, CancellationToken cancelToken)
		{
			return new Func<Task<T>>(() => RetryHelper.BasicRetryAsync(func, retries, retryLogic, exceptionAggregator, waitLogic, cancelToken));
		}

		public static Func<Task<T>> WrapWithBasicRetryAsync<T>(Func<Task<T>> func, int retries, Func<Exception, bool> retryLogic, Func<Exception, IEnumerable<Exception>, Exception> exceptionAggregator, CancellationToken cancelToken)
		{
			return new Func<Task<T>>(() => RetryHelper.BasicRetryAsync(func, retries, retryLogic, exceptionAggregator, cancelToken));
		}

		#endregion

	}
}
