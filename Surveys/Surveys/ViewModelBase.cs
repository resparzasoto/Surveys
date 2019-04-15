using Prism.Mvvm;
using Prism.Navigation;

namespace Surveys
{
    public abstract class ViewModelBase : BindableBase, INavigationAware
    {
        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatingTo(INavigationParameters parameters)
        {
        }
    }
}
