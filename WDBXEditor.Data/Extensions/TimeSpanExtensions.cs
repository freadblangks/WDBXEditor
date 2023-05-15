using Acmil.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Acmil.Data.Extensions
{
	/// <summary>
	/// Class for <see cref="TimeSpan"/> extension methods.
	/// </summary>
	public static class TimeSpanExtensions
	{
		/// <summary>
		/// Scales the <see cref="TimeSpan"/> by a specified scalar value.
		/// </summary>
		/// <param name="span">The <see cref="TimeSpan"/> to scale.</param>
		/// <param name="scalar">The scalar value.</param>
		/// <returns>The <see cref="TimeSpan"/> scaled.</returns>
		public static TimeSpan Scale(this TimeSpan span, double scalar) => TimeSpan.FromTicks((long)(span.Ticks * scalar));

		/// <summary>
		/// Gets the sum of the <see cref="TimeSpan"/>s in an enumerable.
		/// </summary>
		/// <param name="spans">The enumerable of <see cref="TimeSpan"/>s.</param>
		/// <returns>Their sum as a <see cref="TimeSpan"/>.</returns>
		public static TimeSpan Sum(this IEnumerable<TimeSpan> spans) => TimeSpan.FromTicks(spans.Sum(span => span.Ticks));
	}
}
