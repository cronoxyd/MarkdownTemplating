namespace MarkdownTemplating.ViewModels
{
    public interface ITypedViewModel<T> : IViewModel where T : class
    {
        public T Model { get; set; }
    }
}
