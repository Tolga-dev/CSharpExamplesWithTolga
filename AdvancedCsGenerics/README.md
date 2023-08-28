
# Concepts of Variance, Constraints, and Covariance

### Variance

.NET Framework 4 introduced variance support for several existing generic interfaces. Variance support enables implicit conversion of classes that implement these interfaces.

```c#
IEnumerable<String> strings = new List<String>();
IEnumerable<Object> objects = strings;
```

IEnumerable<T> (T is covariant)

IEnumerator<T> (T is covariant)

IQueryable<T> (T is covariant)

IGrouping<TKey,TElement> (TKey and TElement are covariant)

IComparer<T> (T is contravariant)

IEqualityComparer<T> (T is contravariant)

IComparable<T> (T is contravariant)

Starting with .NET Framework 4.5, the following interfaces are variant:

IReadOnlyList<T> (T is covariant)

IReadOnlyCollection<T> (T is covariant)


### Covariance and Contravariance (C#)

While covariance enables you to use a more derived type, 
contravariance allows you to take advantage of a more generic type that that was specified

