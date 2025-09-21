using System;
namespace AdProject.Collections;

public class Tree<T> : ITree<T>
{
	private Node<T> _root = new();

    public void Add(string path, T value)
	{
		var parts = path.Split("/", StringSplitOptions.RemoveEmptyEntries);
		var current = _root;

		foreach(var part in parts)
		{
			if(!current.Children.ContainsKey(part))
			{
				current.Children[part] = new Node<T>();
            }
			current = current.Children[part];
        }
        current.Platforms.Add(value);
    }

	public IEnumerable<T> Find(string path)
	{
        var result = new List<T>();
        var parts = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
        var current = _root;

        foreach (var part in parts)
		{
            if (!current.Children.ContainsKey(part))
			{
                return result;
            }
            current = current.Children[part];
            result.AddRange(current.Platforms);
        }
		return result;
    }

	public void Clear()
	{
		_root = new();
	}
}

