namespace MVVM
{
    public interface ICreateWindowService
    {
        T CreateService<T>() where T : class;
    }
}
