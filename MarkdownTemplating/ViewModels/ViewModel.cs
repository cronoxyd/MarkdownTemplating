using PropertyChanged;
using System.ComponentModel;

namespace MarkdownTemplating.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class ViewModel : IViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsModified { get; protected set; } = false;

        public ViewModel()
        {
            PropertyChanged += ViewModel_PropertyChanged;
        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            IsModified = true;
        }

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged.Invoke(this, e);
        }

        protected void OnPropertyChanged(string PropertyName) => OnPropertyChanged(new PropertyChangedEventArgs(PropertyName));
    }
}
