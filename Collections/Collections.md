## C# Collections Overview

In C#, collections are used to store and organize multiple elements in a single data structure. There are several types of collections available, both generic and non-generic. Each type has its own use case and performance characteristics. Here, we'll cover the most common types of collections with examples and comments.

### Non-Generic Collections

#### 1. ArrayList

The `ArrayList` is a non-generic collection that can store elements of any type.

```csharp
// Create an ArrayList
ArrayList arrayList = new ArrayList();

// Add elements
arrayList.Add("Apple");
arrayList.Add(42);
arrayList.Add(true);

// Remove elements
arrayList.Remove(42);

// Iterate using foreach
foreach (var item in arrayList)
{
    Console.WriteLine(item);
}
```

Use `ArrayList` when you need to store elements of different data types, but be cautious about boxing and unboxing operations, which can impact performance.

#### 2. Hashtable

The `Hashtable` stores key-value pairs, and keys and values can be of any type.

```csharp
// Create a Hashtable
Hashtable hashtable = new Hashtable();

// Add key-value pairs
hashtable.Add("Name", "John");
hashtable.Add("Age", 30);
hashtable.Add("IsEmployed", true);

// Remove a key-value pair
hashtable.Remove("Age");

// Access a value using key
if (hashtable.ContainsKey("Name"))
{
    Console.WriteLine(hashtable["Name"]);
}

// Iterate using IDictionaryEnumerator
var enumerator = hashtable.GetEnumerator();
while (enumerator.MoveNext())
{
    Console.WriteLine(enumerator.Key + ": " + enumerator.Value);
}
```

Use `Hashtable` when you need to map keys to values, and the key-value pairs can be of different data types.

#### 3. Stack

The `Stack` represents a last-in-first-out (LIFO) collection of objects.

```csharp
// Create a Stack
Stack stack = new Stack();

// Push elements
stack.Push("Red");
stack.Push("Green");
stack.Push("Blue");

// Pop elements
if (stack.Count > 0)
{
    stack.Pop();
}

// Iterate using foreach
foreach (var item in stack)
{
    Console.WriteLine(item);
}
```

Use `Stack` when you need to manage elements in a last-in-first-out order, such as handling function calls or evaluating expressions.

#### 4. Queue

The `Queue` represents a first-in-first-out (FIFO) collection of objects.

```csharp
// Create a Queue
Queue queue = new Queue();

// Enqueue elements
queue.Enqueue("Cat");
queue.Enqueue("Dog");
queue.Enqueue("Bird");

// Dequeue elements
if (queue.Count > 0)
{
    queue.Dequeue();
}

// Iterate using while loop
while (queue.Count > 0)
{
    Console.WriteLine(queue.Dequeue());
}
```

Use `Queue` when you need to manage elements in a first-in-first-out order, such as handling task scheduling or processing requests.

### Generic Collections

#### 1. List<T>

The `List<T>` is a generic collection that stores elements of a specific type `T`.

```csharp
// Create a List of strings
List<string> stringList = new List<string>();

// Add elements
stringList.Add("Apple");
stringList.Add("Banana");
stringList.Add("Orange");

// Remove element
stringList.Remove("Banana");

// Iterate using for loop
for (int i = 0; i < stringList.Count; i++)
{
    Console.WriteLine(stringList[i]);
}
```

Use `List<T>` when you need a dynamic list that allows adding, removing, and iterating over elements efficiently.

#### 2. Dictionary<TKey, TValue>

The `Dictionary<TKey, TValue>` stores key-value pairs, where keys and values have specific types.

```csharp
// Create a Dictionary with string keys and int values
Dictionary<string, int> ageDictionary = new Dictionary<string, int>();

// Add key-value pairs
ageDictionary.Add("John", 30);
ageDictionary.Add("Alice", 25);
ageDictionary.Add("Bob", 28);

// Remove a key-value pair
ageDictionary.Remove("Alice");

// Access a value using key
if (ageDictionary.TryGetValue("John", out int johnAge))
{
    Console.WriteLine("John's Age: " + johnAge);
}

// Iterate using foreach on KeyValuePair<TKey, TValue>
foreach (var kvp in ageDictionary)
{
    Console.WriteLine(kvp.Key + ": " + kvp.Value);
}
```

Use `Dictionary<TKey, TValue>` when you need to associate values with unique keys for quick access and retrieval.

### Concurrent Collections

C# also provides concurrent collections that are thread-safe for multi-threaded scenarios.

#### 1. ConcurrentBag<T>

The `ConcurrentBag<T>` is a thread-safe collection that allows multiple threads to add and remove elements without explicit locking.

```csharp
// Create a ConcurrentBag of integers
ConcurrentBag<int> intBag = new ConcurrentBag<int>();

// Add elements from different threads
Parallel.For(0, 10, (i) => intBag.Add(i));

// Remove elements from different threads
Parallel.ForEach(intBag, (item) =>
{
    if (item % 2 == 0)
    {
        intBag.TryTake(out _);
    }
});

// Iterate using foreach (may not be in a specific order)
foreach (var item in intBag)
{
    Console.WriteLine(item);
}
```

Use `ConcurrentBag<T>` when you have multiple threads concurrently adding or removing items from the collection.

#### 2. ConcurrentDictionary<TKey, TValue>

The `ConcurrentDictionary<TKey, TValue>` is a thread-safe dictionary that allows multiple threads to access and modify elements concurrently.

```csharp
// Create a ConcurrentDictionary with string keys and int values
ConcurrentDictionary<string, int> concurrentAgeDictionary = new ConcurrentDictionary<string, int>();

// Add key-value pairs from different threads
Parallel.For(0, 10, (i) =>
{
    concurrentAgeDictionary.TryAdd("Person " + i, i * 5);
});

// Remove a key-value pair from different threads
Parallel.ForEach(concurrentAgeDictionary, (kvp) =>
{
    if (kvp.Value % 2 == 0)
    {
        concurrentAgeDictionary.TryRemove(kvp.Key, out _);
    }
});

// Iterate using foreach (may not be in a specific order)
foreach (var kvp in concurrentAgeDictionary)
{
    Console.WriteLine(kvp.Key + ": " + kvp.Value);
}
```

Use `ConcurrentDictionary<TKey, TValue>` when you have multiple threads concurrently modifying a dictionary.

### There are more Concurrent Collections in C# . Google it Buddy !

### Conclusion

Choosing the right collection type depends on the specific requirements of your application. Consider the data type you want to store, the expected operations (add, remove, access), and whether multi-threading scenarios are involved. Generic collections provide type safety and better performance, while non-generic collections offer more flexibility when dealing with different data types. Concurrent collections ensure thread safety in multi-threaded environments.
