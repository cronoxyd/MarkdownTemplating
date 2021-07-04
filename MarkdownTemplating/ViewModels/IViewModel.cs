using System.ComponentModel;

namespace MarkdownTemplating.ViewModels
{
    public interface IViewModel : INotifyPropertyChanged
    {
        public bool IsModified { get; }
    }
}
