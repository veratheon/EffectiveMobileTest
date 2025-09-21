using System;
using AdProject.Entities;

namespace AdProject.Collections;


public class Node<T>
{
	public Dictionary<string, Node<T>> Children { get; } = new();
	public List<T> Platforms { get; } = new();
}



