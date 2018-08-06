# Flatten Array

C# extension method to flatten N-dimensional arrays of any data type.

## Samples

```
var intArray = new[, ,] { { { 1, 1 }, { 2, 3 } } };
CollectionAssert.AreEqual(intArray.Flatten<int>(), new[] { 1, 1, 2, 3 });

var dynamicJaggedArray = new dynamic[] { 1, new dynamic[] { "2", 3, new { prop = 123 } } };
CollectionAssert.AreEqual(dynamicJaggedArray.Flatten<dynamic>(), new dynamic[] { 1, "2", 3, new { prop = 123 } });

var intJaggedArray = new int[][] { new int[] { 1, 1 }, new int[] { 2, 3, 4 } };
CollectionAssert.AreEqual(intJaggedArray.Flatten<int>(), new[] { 1, 1, 2, 3, 4 });

var stringJaggedArray = new dynamic[] { new[] { "a" }, new dynamic[] { "b", new[] { "c" } } };
CollectionAssert.AreEqual(stringJaggedArray.Flatten<string>(), new[] { "a", "b", "c" });
```

## Considerations

- Developed using Visual Studio Community and .NET Framework 4.7.1.
- Compatible with all types of arrays.
- Generics was used only to define the return type.
- Unit testing done through MSTest.