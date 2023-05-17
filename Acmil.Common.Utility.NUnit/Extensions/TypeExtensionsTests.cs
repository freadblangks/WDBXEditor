using Acmil.Common.Utility.Extensions;
using NUnit.Framework;
using System;

namespace Acmil.Common.Utility.NUnit.Extensions
{
	[TestFixture]
	public class TypeExtensionsTests
	{
		[TestCase(typeof(NonGenericTestClassImplementation), true, Description = "Class is non-generic and an implementation of the interface")]
		[TestCase(typeof(NonGenericTestClassNonImplementation), false, Description = "Class is non-generic and not an implementation of the interface")]
		[TestCase(typeof(GenericTestClassImplementation<>), true, Description = "Class is generic and an implementation of the interface")]
		[TestCase(typeof(GenericTestClassImplementation<int>), true, Description = "Class is generic with type specified and an implementation of the interface")]
		[TestCase(typeof(GenericTestClassNonImplementation<>), false, Description = "Class is generic and not an implementation of the interface")]
		[TestCase(typeof(GenericTestClassNonImplementation<int>), false, Description = "Class is generic with type specified and not an implementation of the interface")]
		public void TypeExtensions_ImplementsInterface_ReturnsCorrectValueForNonGenericInterface(Type type, bool expectedResult)
		{
			// ARRANGE //

			// ACT //
			bool actualResult = type.ImplementsInterface<IBasicTestClass>();

			// ASSERT //
			Assert.AreEqual(expectedResult, actualResult);
		}


		[TestCase(typeof(NonGenericTestClassImplementation), true, true, Description = "Class is non-generic and an implementation of the interface; checking that the interface type arguments match")]
		[TestCase(typeof(NonGenericTestClassNonImplementation), true, false, Description = "Class is non-generic and not an implementation of the interface; checking that the interface type arguments match")]
		[TestCase(typeof(GenericTestClassImplementation<>), true, true, Description = "Class is generic and an implementation of the interface; checking that the interface type arguments match")]
		[TestCase(typeof(GenericTestClassImplementation<int>), true, true, Description = "Class is generic with type specified and an implementation of the interface; checking that the interface type arguments match")]
		[TestCase(typeof(GenericTestClassNonImplementation<>), true, false, Description = "Class is generic and not an implementation of the interface; checking that the interface type arguments match")]
		[TestCase(typeof(GenericTestClassNonImplementation<int>), true, false, Description = "Class is generic with type specified and not an implementation of the interface; checking that the interface type arguments match")]
		[TestCase(typeof(NonGenericTestClassImplementation), false, true, Description = "Class is non-generic and an implementation of the interface; not checking that the interface type arguments match")]
		[TestCase(typeof(NonGenericTestClassNonImplementation), false, false, Description = "Class is non-generic and not an implementation of the interface; not checking that the interface type arguments match")]
		[TestCase(typeof(GenericTestClassImplementation<>), false, true, Description = "Class is generic and an implementation of the interface; not checking that the interface type arguments match")]
		[TestCase(typeof(GenericTestClassImplementation<int>), false, true, Description = "Class is generic with type specified and an implementation of the interface; not checking that the interface type arguments match")]
		[TestCase(typeof(GenericTestClassNonImplementation<>), false, false, Description = "Class is generic and not an implementation of the interface; not checking that the interface type arguments match")]
		[TestCase(typeof(GenericTestClassNonImplementation<int>), false, false, Description = "Class is generic with type specified and not an implementation of the interface; not checking that the interface type arguments match")]
		public void TypeExtensions_ImplementsInterface_ReturnsCorrectValueForTypeImplementingGenericInterface(Type type, bool checkInterfaceTypeArgs, bool expectedResult)
		{
			// ARRANGE //

			// ACT //
			bool actualResult = type.ImplementsInterface<IBasicTestClassWithType<int>>(checkInterfaceTypeArgs);

			// ASSERT //
			Assert.AreEqual(expectedResult, actualResult);
		}

		[TestCase(typeof(NonGenericTestClassImplementationWithStringArg), true, true, Description = "Class is an implementation of the interface and the interface type args match; checking that the interface type arguments match")]
		[TestCase(typeof(NonGenericTestClassImplementation), true, false, Description = "Class is non-generic and an implementation of the interface; checking that the interface type arguments match")]
		[TestCase(typeof(NonGenericTestClassNonImplementation), true, false, Description = "Class is non-generic and not an implementation of the interface; checking that the interface type arguments match")]
		[TestCase(typeof(GenericTestClassImplementation<>), true, false, Description = "Class is generic and an implementation of the interface; checking that the interface type arguments match")]
		[TestCase(typeof(GenericTestClassImplementation<int>), true, false, Description = "Class is generic with type specified and an implementation of the interface; checking that the interface type arguments match")]
		[TestCase(typeof(GenericTestClassNonImplementation<>), true, false, Description = "Class is generic and not an implementation of the interface; checking that the interface type arguments match")]
		[TestCase(typeof(GenericTestClassNonImplementation<int>), true, false, Description = "Class is generic with type specified and not an implementation of the interface; checking that the interface type arguments match")]
		[TestCase(typeof(NonGenericTestClassImplementationWithStringArg), false, true, Description = "Class is an implementation of the interface and the interface type args match; not checking that the interface type arguments match")]
		[TestCase(typeof(NonGenericTestClassImplementation), false, true, Description = "Class is non-generic and an implementation of the interface; not checking that the interface type arguments match")]
		[TestCase(typeof(NonGenericTestClassNonImplementation), false, false, Description = "Class is non-generic and not an implementation of the interface; not checking that the interface type arguments match")]
		[TestCase(typeof(GenericTestClassImplementation<>), false, true, Description = "Class is generic and an implementation of the interface; not checking that the interface type arguments match")]
		[TestCase(typeof(GenericTestClassImplementation<int>), false, true, Description = "Class is generic with type specified and an implementation of the interface; not checking that the interface type arguments match")]
		[TestCase(typeof(GenericTestClassNonImplementation<>), false, false, Description = "Class is generic and not an implementation of the interface; not checking that the interface type arguments match")]
		[TestCase(typeof(GenericTestClassNonImplementation<int>), false, false, Description = "Class is generic with type specified and not an implementation of the interface; not checking that the interface type arguments match")]
		public void TypeExtensions_ImplementsInterface_ReturnsCorrectValueForTypeImplementingGenericInterfaceWhenTypeImplementsWithDifferentTypeArg(Type type, bool checkInterfaceTypeArgs, bool expectedResult)
		{
			// ARRANGE //

			// ACT //
			bool actualResult = type.ImplementsInterface<IBasicTestClassWithType<string>>(checkInterfaceTypeArgs);

			// ASSERT //
			Assert.AreEqual(expectedResult, actualResult);
		}

		[Test]
		public void TypeExtensions_ImplementsInterface_ThrowsArgumentExceptionWhenTypeParamIsNotAnInterface()
		{
			// ARRANGE //
			Type testType = typeof(NonGenericTestClassNonImplementation);
			string expectedExceptionMessage = $"Type parameter {typeof(NonGenericTestClassImplementation)} is not an interface";

			// ACT AND ASSERT //
			string actualExceptionMessage = Assert.Throws<ArgumentException>(() => testType.ImplementsInterface<NonGenericTestClassImplementation>()).Message;
			StringAssert.Contains(expectedExceptionMessage, actualExceptionMessage);
		}

		#region Private Test Classes and Interfaces

		private interface IBasicTestClass { }

		private interface IBasicTestClassWithType<T> { }

		private class NonGenericTestClassImplementation : IBasicTestClass, IBasicTestClassWithType<int> { }

		private class NonGenericTestClassNonImplementation { }

		private class GenericTestClassImplementation<T> : IBasicTestClass, IBasicTestClassWithType<int> { }

		private class GenericTestClassNonImplementation<T> { }

		private class NonGenericTestClassImplementationWithStringArg : IBasicTestClassWithType<string> { }

		#endregion
	}
}