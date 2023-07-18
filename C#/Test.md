<h2>C# Collections Overview</h2>

<p>In C#, collections are used to store and organize multiple elements in a single data structure. There are several types of collections available, both generic and non-generic. Each type has its own use case and performance characteristics. Here, we'll cover the most common types of collections with examples and comments.</p>

<h3>Non-Generic Collections</h3>

<h4>1. ArrayList</h4>

<p>The <code>ArrayList</code> is a non-generic collection that can store elements of any type.</p>

<pre><code>// Create an ArrayList
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
</code></pre>

<p>Use <code>ArrayList</code> when you need to store elements of different data types, but be cautious about boxing and unboxing operations, which can impact performance.</p>

<h4>2. Hashtable</h4>

<p>The <code>Hashtable</code> stores key-value pairs, and keys and values can be of any type.</p>

<pre><code>// Create a Hashtable
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
</code></pre>

<p>Use <code>Hashtable</code> when you need to map keys to values, and the key-value pairs can be of different data types.</p>

<h4>3. Stack</h4>

<p>The <code>Stack</code> represents a last-in-first-out (LIFO) collection of objects.</p>

<pre><code>// Create a Stack
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
</code></pre>

<p>Use <code>Stack</code> when you need to manage elements in a last-in-first-out order, such as handling function calls or evaluating expressions.</p>

<h4>4. Queue</h4>

<p>The <code>Queue</code> represents a first-in-first-out (FIFO) collection of objects.</p>

<pre><code>// Create a Queue
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
</code></pre>

<p>Use <code>Queue</code> when you need to manage elements in a first-in-first-out order, such as handling task scheduling or processing requests.</p>

<h3>Generic Collections</h3>

<h4>1. List&lt;T&gt;</h4>

<p>The <code>List&lt;T&gt;</code> is a generic collection that stores elements of a specific type <code>T</code>.</p>

<pre><code>// Create a List of strings
List&lt;string&gt; stringList = new List&lt;string&gt;();

// Add elements
stringList.Add("Apple");
stringList.Add("Banana");
stringList.Add("Orange");

// Remove element
stringList.Remove("Banana");

// Iterate using for loop
for (int i = 0; i &lt; stringList.Count; i++)
{
    Console.WriteLine(stringList[i]);
}
</code></pre>

<p>Use <code>List&lt;T&gt;</code> when you need a dynamic list that allows adding, removing, and iterating over elements efficiently.</p>

<h4>2. Dictionary&lt;TKey, TValue&gt;</h4>

<p>The <code>Dictionary&lt;TKey, TValue&gt;</code> stores key-value pairs, where keys and values have specific types.</p>

<pre><code>// Create a Dictionary with string keys and int values
Dictionary&lt;string, int&gt; ageDictionary = new Dictionary&lt;string, int&gt;();

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

// Iterate using foreach on KeyValuePair&lt;TKey, TValue&gt;
foreach (var kvp in ageDictionary)
{
    Console.WriteLine(kvp.Key + ": " + kvp.Value);
}
</code></pre>

<p>Use <code>Dictionary&lt;TKey, TValue&gt;</code> when you need to associate values with unique keys for quick access and retrieval.</p>

<h3>Concurrent Collections</h3>

<p>C# also provides concurrent collections that are thread-safe for multi-threaded scenarios.</p>

<h4>1. ConcurrentBag&lt;T&gt;</h4>

<p>The <code>ConcurrentBag&lt;T&gt;</code> is a thread-safe collection that allows multiple threads to add and remove elements without explicit locking.</p>

<pre><code>// Create a ConcurrentBag of integers
ConcurrentBag&lt;int&gt; intBag = new ConcurrentBag&lt;int&gt;();

// Add elements from different threads
Parallel.For(0, 10, (i) =&gt; intBag.Add(i));

// Remove elements from different threads
Parallel.ForEach(intBag, (item) =&gt;
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
</code></pre>

<p>Use <code>ConcurrentBag&lt;T&gt;</code> when you have multiple threads concurrently adding or removing items from the collection.</p>

<h4>2. ConcurrentDictionary&lt;TKey, TValue&gt;</h4>

<p>The <code>ConcurrentDictionary&lt;TKey, TValue&gt;</code> is a thread-safe dictionary that allows multiple threads to access and modify elements concurrently.</p>

<pre><code>// Create a ConcurrentDictionary with string keys and int values
ConcurrentDictionary&lt;string, int&gt; concurrentAgeDictionary = new ConcurrentDictionary&lt;string, int&gt;();

// Add key-value pairs from different threads
Parallel.For(0, 10, (i) =&gt;
{
    concurrentAgeDictionary.TryAdd("Person " + i, i * 5);
});

// Remove a key
