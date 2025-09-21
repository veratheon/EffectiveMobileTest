namespace AdProject.Collections
{
    public interface ITree<T>
    {
        void Add(string path, T value);
        IEnumerable<T> Find(string path);
        void Clear();
    }
}