using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowledgeBase.WPF.Manager;

namespace KnowledgeBase.WPF.Services
{
    public class NavigationService<TViewModel> where TViewModel : ObservableRecipient     
    {
        private readonly NavigationManager _navigationManager;
        private readonly Func<TViewModel> _viewModelFactory;

        public NavigationService(NavigationManager navigationManager, Func<TViewModel> viewModelFactory)
        {
            _navigationManager = navigationManager;
            _viewModelFactory = viewModelFactory;
        }

        public void Navigate()
        {
            _navigationManager.CurrentViewModel = _viewModelFactory();
        }
    }
}
