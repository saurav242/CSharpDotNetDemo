
# LINQ :: Language Integrated Query

There are 2 Ways to write LINQ Queries

1. Method Syntex (using Lambda expression)
2. Query Syntex (SQL like)

Behind the Scene LINQ querire written in SQL like Query syntex are transalted into Method Syntex (using Lambda expression).

The Standary Query Operators are implemented as extension method on `IEnumerable<T>` Interface

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

### E. Partioning Operators

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
   Attempts to convert all the item in collection to another type and return them in a new collection. If an item failsconversion exception will be thrown. It used Deffered Excetion
6. OffType  
   Will return the elemnt of the specified Type. Other Elemnt are ignored and excluded from the result list.
7. AsEnumerable  
   It breaks the query in two Part. Every thing before AsEnumerable is executed in SQL and fetches the data. and eveything asfter that will be execute in memory data. It moves Query Processing to client side.
8. AsQuerable  
   The main use of AsQueriable operator is in Unit Testing to mock a queriable data source using an in-memory data store.

### G. Grouping Operators

1. GroupBy  
   It takes a flat sequence of items and organizes the sequence into gorups(IGrouping<K,V>) based on specific key and returns group of sequence.

### H. Element Operators

In General, It retrives a single elemnet from a sequence using the elemnt index or based on condition

1. First
   Get First Element. Throws InvalidOperationException, If Sequence does not conatins any elements or no elemnt statisfies the condition
2. FirstOrDefault  
   Get First Element. Do not Throws exception, Insted default value of that type is returned, If Sequence does not conatins any elements or no elemnt statisfies the condition
3. Last  
   Get Last Element. Throws InvalidOperationException, If Sequence does not conatins any elements or no elemnt statisfies the condition
4. LastOrDefault  
   Get Last Element. Do not Throws exception, Insted default value of that type is returned, If Sequence does not conatins any elements or no elemnt statisfies the condition
5. ElementAt  
   Get an Element At specified index. Throws ArgumentOurOfRangeException, If Sequence is empty or if the provided index is out of range.
6. ElementAtOrDefault  
   Get an Element At specified index. Do not Throws exception, Insted default value of that type is returned, If Sequence is empty or if the provided index is out of range.
7. Single
   * Single() =>  Get the only Element (one) in sequence. Throws InvalidOperationException,  If Sequence is empty or conatins more than one elements 
   * Single(with_some_condition) =>  Get the only Element (one) in sequence that statisfies the condition. Throws InvalidOperationException, If sequence does not conatain any elemnt or no elemnt satisfies condition or more than one elemnt statisfies the condition
8. SingleOrDefault => Similar To Single() but
   * does not throw an exception when Sequence is Empty or no Elemnt Satisfies the condition
   * will still throw an exception if more than one elemnt statisfies the cndition
9. DefaultIfEmpty
   * If the Sequence is not empty , then the original Sequence is returned
   * If Sequence is Empty , then returns a Sequence with default value of Expected Type
   * Can also Provide default value to return, In case of empty

### I. Join Operators

1. Group Join => Produces heirarchical result set
2. Inner Join => Flat, 1-1 mapping, only the matching element are included in the result set. 
3. Left Outer Join
4. Cross Join


### J. Set Operators

1. Distinct  
   Returns Distinct elements from Sequence
For Complex Type like Employee, Department , etc default comparer just checkes for object reference being equal amd not the individual property values 
   * Use overloaded Method of Distinct and pass a coustom class that implemnt IEqualityComparer
   * Overrice Equal and GetHashCode methid in Employee Class
   * Project (Select) the properties into a new anonymous type which ovverride Equal() and GetHashCode() Methods
2. Union  
   Two Sequence are combined and duplicate elemnts are removed.
For Complex Type like Employee, Same Issue/Solution as Distict
3. Intersect  
   Returns the common Elements in two Sequence.
4. Except  
   Returns elemnt thst are preset in first Sequence but not in Second Collection

### K. Generation Operators

1. Range => Generates a sequence of integert within a specified range.
2. Repeat => Generates a sequence thatcontains one repeated value
3. Empty => Generates an empty sequence of specifies type

### L. Concat Operator

It concatenates/ cobmines two sequence into one sequence. Deplicate Elemnt are not removed. Item of Seq1 followed by items of Seq2.

### M. SequenceEqual Operator

Used to determine if two Sequence are equal. Returns True/ False. For Sequence to be equal

* Both sequence should have sma eno of elements
* Same value should be present in same order in sequence.
* default comparer is CaseSensetive
* For Complex Type like Employee, Same Issue/Solution as Distict

### N. Quantifier Operators

1. All  
   Returns True if all element in the Given Sequence Statisfy a given condition
2. Any
   * Any() => Returns True if sequence conatins any elemnt
   * Any(with_some_conditon) => Returns True if Sequence conatins any elemnt with given condition
3. Contains
   * Contains() => Returns True if sequence conatins given element
   * Contains(equaltiy_comparer) => Returns True if Sequence conatins given elemnt with given equaltiy comparer
   * For Complex Type like Employee, Same Issue/Solution as Distict

## Types Of LINQ Operatore Based on excetion behaviour

1. Deferred or Lazy Operators  
These query operaotrs uses deferred execution
   * Executed where the variable is used/ iterrated, not where defined
   * Ex: Select, where .take, skip
2. Immediate or Greedy Operators  
These query operatrs uses immediate execution
   * Executed where defined 
   * Ex: Count, Min, Max, Average ,ToList, 

