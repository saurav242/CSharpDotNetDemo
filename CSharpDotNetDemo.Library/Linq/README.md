
# LINQ :: Language Integrated Query

There are 2 Ways to write LINQ Queries

1. Method Syntax (using Lambda expression)
2. Query Syntax (SQL like)

Behind the Scene LINQ queries written in SQL like Query syntax are translated into Method Syntax (using Lambda expression).

The Standard Query Operators are implemented as extension method on `IEnumerable<T>` Interface

---

## Types Of LINQ Operators Based on execution behavior

### 1. Deferred or Lazy Operators  

These query operators uses deferred execution

* Executed where the variable is used/ iterated, not where defined
* Ex : Select, where .take, skip

### 2. Immediate or Greedy Operators  

These query operators uses immediate execution

* Executed where defined
* Ex : Count, Min, Max, Average ,ToList

---

## Types Of LINQ Operators

### A. Aggregate Operators

1. Min
2. Max
3. Sum
4. Count
5. Average
6. Aggregate

### B. Restriction / Filter Operators

1. Where

### C. Projection Operators

1. Select
2. SelectMany

### D. Ordering Operators

1. OrderBy
2. OrderByDescending
3. ThenBy
4. ThenByDescending
5. Reverse

### E. Partitioning Operators

1. Take
2. Skip
3. TakeWhile
4. SkipWhile

### F. Conversion Operators

1. ToList
2. ToArray
3. ToDictionary  
   Key must be unique within a dict, else it will throw an exception
4. ToLookup  
   LookUp is Same as Dict but Can have duplicate key
5. Cast  
   Attempts to convert all the item in collection to another type and return them in a new collection. If an item fails conversion exception will be thrown. It used Differed Execution
6. OffType  
   Will return the element of the specified Type. Other Element are ignored and excluded from the result list.
7. AsEnumerable  
   It breaks the query in two Part. Every thing before AsEnumerable is executed in SQL and fetches the data. and everything after that will be execute in memory data. It moves Query Processing to client side.
8. AsQueryable  
   The main use of AsQueryable operator is in Unit Testing to mock a Queryable data source using an in-memory data store.

### G. Grouping Operators

1. GroupBy  
   It takes a flat sequence of items and organizes the sequence into groups(IGrouping<K,V>) based on specific key and returns group of sequence.

### H. Element Operators

In General, It retrieves a single element from a sequence using the element index or based on condition

1. First
   Get First Element. Throws InvalidOperationException, If Sequence does not contains any elements or no element satisfies the condition
2. FirstOrDefault  
   Get First Element. Do not Throws exception, Instead default value of that type is returned, If Sequence does not contains any elements or no element satisfies the condition
3. Last  
   Get Last Element. Throws InvalidOperationException, If Sequence does not contains any elements or no element satisfies the condition
4. LastOrDefault  
   Get Last Element. Do not Throws exception, Instead default value of that type is returned, If Sequence does not contains any elements or no element satisfies the condition
5. ElementAt  
   Get an Element At specified index. Throws ArgumentOurOfRangeException, If Sequence is empty or if the provided index is out of range.
6. ElementAtOrDefault  
   Get an Element At specified index. Do not Throws exception, Instead default value of that type is returned, If Sequence is empty or if the provided index is out of range.
7. Single
   * Single() =>  Get the only Element (one) in sequence. Throws InvalidOperationException,  If Sequence is empty or contains more than one elements
   * Single(with_some_condition) =>  Get the only Element (one) in sequence that satisfies the condition. Throws InvalidOperationException, If sequence does not contain any element or no element satisfies condition or more than one element satisfies the condition
8. SingleOrDefault => Similar To Single() but
   * does not throw an exception when Sequence is Empty or no Element Satisfies the condition
   * will still throw an exception if more than one element satisfies the condition
9. DefaultIfEmpty
   * If the Sequence is not empty , then the original Sequence is returned
   * If Sequence is Empty , then returns a Sequence with default value of Expected Type
   * Can also Provide default value to return, In case of empty

### I. Join Operators

1. Group Join  
   Produces hierarchical result set
2. Inner Join  
   Flat, 1-1 mapping, only the matching element are included in the result set.
3. Left Outer Join
4. Cross Join

### J. Set Operators

1. Distinct  
   Returns Distinct elements from Sequence
   * For Complex Type like Employee, Department , etc default comparer just checks for object reference being equal amd not the individual property values
     * Use overloaded Method of Distinct and pass a custom class that implement IEqualityComparer
     * Override Equal and GetHashCode method in Employee Class
     * Project (Select) the properties into a new anonymous type which override Equal() and GetHashCode() Methods
2. Union  
   Two Sequence are combined and duplicate elements are removed.
   * For Complex Type like Employee, Same Issue/Solution as Distinct
3. Intersect  
   Returns the common Elements in two Sequence.
4. Except  
   Returns element that are preset in first Sequence but not in Second Collection

### K. Generation Operators

1. Range => Generates a sequence of integers within a specified range.
2. Repeat => Generates a sequence that contains one repeated value
3. Empty => Generates an empty sequence of specifies type

### L. Concat Operator

It concatenates/ combines two sequence into one sequence. Duplicate Element are not removed. Item of Seq1 followed by items of Seq2.

### M. SequenceEqual Operator

Used to determine if two Sequence are equal. Returns True/ False. For Sequence to be equal

* Both sequence should have same no of elements
* Same value should be present in same order in sequence.
* default comparer is CaseSensitive
* For Complex Type like Employee, Same Issue/Solution as Distinct

### N. Quantifier Operators

1. All  
   Returns True if all element in the Given Sequence Satisfy a given condition
2. Any
   * Any() => Returns True if sequence contains any element
   * Any(with_some_condition) => Returns True if Sequence contains any element with given condition
3. Contains
   * Contains() => Returns True if sequence contains given element
   * Contains(equality_comparer) => Returns True if Sequence contains given element with given equality comparer
   * For Complex Type like Employee, Same Issue/Solution as Distinct
