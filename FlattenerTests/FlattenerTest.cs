using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlattenerTests {
	[TestClass]
	public class FlattenerTests {
		[TestMethod]
		[Description("Should return a one-dimensional with the right data type")]
		public void Flatten_ReturnsRightDataType() {
			Assert.IsInstanceOfType(new int[,,,,] { }.Flatten<int>(), typeof(int[]));
			Assert.IsInstanceOfType(new char[][] { }.Flatten<char>(), typeof(char[]));
			Assert.IsInstanceOfType(new string[] { }.Flatten<string>(), typeof(string[]));
		}
		[TestMethod]
		[Description("The elements' order should be kept for multidimensional/jagged arrays")]
		public void Flatten_MultidimensionalArray_KeepsElementsOrder() {
			var intArray = new[, ,] { { { 1, 1 }, { 2, 3 } } };
			CollectionAssert.AreEqual(intArray.Flatten<int>(), new[] { 1, 1, 2, 3 });

			var dynamicJaggedArray = new dynamic[] { 1, new dynamic[] { "2", 3, new { prop = 123 } } };
			CollectionAssert.AreEqual(dynamicJaggedArray.Flatten<dynamic>(), new dynamic[] { 1, "2", 3, new { prop = 123 } });

			var intJaggedArray = new int[][] { new int[] { 1, 1 }, new int[] { 2, 3, 4 } };
			CollectionAssert.AreEqual(intJaggedArray.Flatten<int>(), new[] { 1, 1, 2, 3, 4 });

			var stringJaggedArray = new dynamic[] { new[] { "a" }, new dynamic[] { "b", new[] { "c" } } };
			CollectionAssert.AreEqual(stringJaggedArray.Flatten<string>(), new[] { "a", "b", "c" });
		}
		[TestMethod]
		[Description("An empty array should return an empty array")]
		public void Flatten_EmptyArray_ReturnsEmptyArray() {
			var jaggedIntArray = new int[][][] { };
			Assert.AreEqual(jaggedIntArray.Flatten<int>().Length, 0);
			var intArray = new int[,,] { };
			Assert.AreEqual(intArray.Flatten<int>().Length, 0);
		}
		[TestMethod]
		[Description("Flattening an array of a given type into a different type should fail")]
		public void Flatten_IncompatibleType_Fail() {
			var dynamicArray = new dynamic[] { 1, "2", 3, new { prop = 123 } };
			Assert.ThrowsException<InvalidCastException>(() => dynamicArray.Flatten<string>());
			var shortArray = new short[] { 1, 2, 3 };
			Assert.ThrowsException<InvalidCastException>(() => shortArray.Flatten<int>());
		}
	}
}