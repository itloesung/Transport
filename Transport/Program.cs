using System;
using System.Collections.Generic;
using Transport;

var routes = new List<string> { "Route A", "Route B", "Route C", "Route D" };
var cursor = new Cursor<string>(routes);

Console.WriteLine("Transport Cursor Demo");
Console.WriteLine("=====================");

Console.WriteLine("Forward traversal:");
while (cursor.MoveNext())
    Console.WriteLine($"  [{cursor.Position}] {cursor.Current}");

Console.WriteLine("Backward traversal:");
while (cursor.MovePrevious())
    Console.WriteLine($"  [{cursor.Position}] {cursor.Current}");

cursor.MoveFirst();
Console.WriteLine($"First: {cursor.Current}");

cursor.MoveLast();
Console.WriteLine($"Last: {cursor.Current}");

cursor.Reset();
Console.WriteLine($"After reset, position: {cursor.Position}");
