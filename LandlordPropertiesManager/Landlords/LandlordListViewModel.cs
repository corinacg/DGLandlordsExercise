using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using LandlordPropertiesManager.Domain;
using LandlordPropertiesManager.Services;

namespace LandlordPropertiesManager.Landlords
{
    class LandlordListViewModel : BindableBase
    {
        private ObservableCollection<Landlord> landlords;
        private ObservableCollection<Property> landlordProperties;
        private Landlord selectedLandlord;
        private Property selectedProperty;
        private readonly ILandlordRepository landlordRepository;

        public ObservableCollection<Landlord> Landlords
        {
            get { return landlords; }
            set { SetProperty(ref landlords, value); }
        }
        public Landlord SelectedLandlord
        {
            get { return selectedLandlord; }
            set
            {
                if(selectedLandlord != value)
                {
                    SetProperty(ref selectedLandlord, value);
                    LoadPropertiesForLandlord();
                    AddPropertyCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public Property SelectedProperty
        {
            get { return selectedProperty; }
            set
            {
                if (selectedProperty != value)
                {
                    SetProperty(ref selectedProperty, value);
                }
            }
        }
        public ObservableCollection<Property> LandlordProperties
        {
            get { return landlordProperties; }
            set { SetProperty(ref landlordProperties, value); }
        }
        public DelegateCommand AddPropertyCommand { get; private set; }
        public DelegateCommand<Property> EditPropertyCommand { get; private set; }

        public event Action<Property> AddPropertyRequested = delegate { };
        public event Action<Property> EditPropertyRequested = delegate { };

        public LandlordListViewModel(ILandlordRepository landlordRepository)
        {
            this.landlordRepository = landlordRepository;
            AddPropertyCommand = new DelegateCommand(AddPropertyCommandExecute, AddPropertyCommandCanExecute);
            EditPropertyCommand = new DelegateCommand<Property>(EditPropertyCommandExecute);
        }

        public async void LoadLandloards()
        {
            var landlordsList = await landlordRepository.GetLandlordsAsync();
            Landlords = new ObservableCollection<Landlord>(landlordsList);
        }
        public async void LoadPropertiesForLandlord()
        {
            if (SelectedLandlord != null)
            {
                var propertiesList = await landlordRepository.GetPropertiesAsync(SelectedLandlord.LandlordId);
                LandlordProperties = new ObservableCollection<Property>(propertiesList);
            }
            else
            {
                LandlordProperties = null;
            }
        }

        private void AddPropertyCommandExecute()
        {
            AddPropertyRequested(new Property()
            {
                LandlordId = SelectedLandlord.LandlordId,
                AvailableFrom = DateTime.Now
            });
        }
        private bool AddPropertyCommandCanExecute()
        {
            return SelectedLandlord != null;
        }
        private void EditPropertyCommandExecute(Property property)
        {
            EditPropertyRequested(property);
        }

    }
}
