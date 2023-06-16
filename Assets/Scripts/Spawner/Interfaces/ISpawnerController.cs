namespace Game
{
    public interface ISpawnerController<T> where T : class
    {
        T Get();
    }
}
