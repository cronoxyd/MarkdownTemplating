namespace MarkdownTemplating.ViewModels
{
    public class TypedViewModel<T> : ViewModel, ITypedViewModel<T> where T: class
    {
        public T Model { get; set; }

        public bool IsNew { get; set; } = false;

        public TypedViewModel(T Model) : base()
        {
            this.Model = Model;
        }
    }
}
