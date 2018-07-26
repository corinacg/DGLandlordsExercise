using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using LandlordPropertiesManager.Landlords;
using Unity;
using LandlordPropertiesManager.Domain;


namespace LandlordPropertiesManager
{
    class MainWindowViewModel : BindableBase
    {
        private LandlordListViewModel landlordListViewModel;
        private AddEditPropertyViewModel addEditPropertyViewModel;
        private BindableBase currentViewModel;
        public BindableBase CurrentViewModel
        {
            get { return currentViewModel; }
            set { SetProperty(ref currentViewModel, value); }
        }

        public MainWindowViewModel(LandlordListViewModel landlordListViewModel, AddEditPropertyViewModel addEditPropertyViewModel)
        {
            this.landlordListViewModel = landlordListViewModel;
            this.addEditPropertyViewModel = addEditPropertyViewModel;
            landlordListViewModel.AddPropertyRequested += NavToAddProperty;
            landlordListViewModel.EditPropertyRequested += NavToEditProperty;
            addEditPropertyViewModel.Done += AddEditPropertyViewModel_Done;
            CurrentViewModel = landlordListViewModel;
        }

        private async void AddEditPropertyViewModel_Done()
        {
            landlordListViewModel.SelectedLandlord = null;
            CurrentViewModel = landlordListViewModel;
        }

        private void NavToAddProperty(Property property)
        {
            addEditPropertyViewModel.EditMode = false;
            addEditPropertyViewModel.SetProperty(property);
            CurrentViewModel = addEditPropertyViewModel;
        }

        private void NavToEditProperty(Property property)
        {
            addEditPropertyViewModel.EditMode = true;
            addEditPropertyViewModel.SetProperty(property);
            CurrentViewModel = addEditPropertyViewModel;
        }
    }
}
