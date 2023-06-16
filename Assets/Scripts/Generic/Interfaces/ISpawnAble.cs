namespace ObjectPooling.Interfaces
{
    public interface ISpawnAble<T>
    {
        void OnSpawn(in T tObject);
    }
}
