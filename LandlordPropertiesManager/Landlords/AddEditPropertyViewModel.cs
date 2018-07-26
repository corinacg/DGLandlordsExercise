using System;
using LandlordPropertiesManager.Domain;
using LandlordPropertiesManager.Services;
using Prism.Commands;
using Prism.Mvvm;

namespace LandlordPropertiesManager.Landlords
{
    class AddEditPropertyViewModel : BindableBase
    {
        private Property property;
        private EditPropertyModel editPropertyModel;
        private bool editMode;
        private readonly ILandlordRepository landlordRepository;
        private DateTime minimumAvailableFromDate;
        private DateTime maximumAvailableFromDate;

        public EditPropertyModel EditPropertyModel
        {
            get { return editPropertyModel; }
            private set { SetProperty(ref editPropertyModel, value); }
        }
        public bool EditMode
        {
            get { return editMode; }
            set { SetProperty(ref editMode, value); }
        }
        public DateTime MinimumAvailableFromDate
        {
            get { return minimumAvailableFromDate; }
            set { SetProperty(ref minimumAvailableFromDate, value); }
        }
        public DateTime MaximumAvailableFromDate
        {
            get { return maximumAvailableFromDate; }
            set { SetProperty(ref maximumAvailableFromDate, value); }
        }

        public DelegateCommand CancelCommand { get; private set; }
        public DelegateCommand SaveCommand { get; private set; }
        public event Action Done = delegate { };

        public AddEditPropertyViewModel(ILandlordRepository landlordRepository)
        {
            CancelCommand = new DelegateCommand(CancelCommandExecute);
            SaveCommand = new DelegateCommand(SaveCommandExecute, SaveCommandCanExecute);
            this.landlordRepository = landlordRepository;
            ///TODO: read these from configurations
            this.MinimumAvailableFromDate = new DateTime(1900, 1, 1);
            this.MaximumAvailableFromDate = new DateTime(2040, 1, 1);
        }

        public void SetProperty(Property property)
        {
            this.property = property;
            if (EditPropertyModel != null)
            {
                EditPropertyModel.ErrorsChanged -= RaiseCanExecuteChanged;
            }
           
            this.EditPropertyModel = new EditPropertyModel()
            {
                Housenumber = property.Housenumber,
                Street = property.Street,
                Town = property.Town,
                PostCode = property.PostCode,
                AvailableFrom = property.AvailableFrom,
                Status = property.Status
            };

            EditPropertyModel.ErrorsChanged += RaiseCanExecuteChanged;
        }

        private void CancelCommandExecute()
        {
            Done();
        }

        private async void SaveCommandExecute()
        {
            property.Housenumber = EditPropertyModel.Housenumber;
            property.Street = EditPropertyModel.Street;
            property.Town = EditPropertyModel.Town;
            property.PostCode = EditPropertyModel.PostCode;
            property.AvailableFrom = EditPropertyModel.AvailableFrom;
            property.Status = EditPropertyModel.Status;
            if (EditMode)
                await landlordRepository.UpdatePropertyAsync(property);
            else
                await landlordRepository.AddPropertyAsync(property);
            Done();
        }

        private bool SaveCommandCanExecute()
        {
            return !EditPropertyModel.HasErrors;
        }

        private void RaiseCanExecuteChanged(object sender, EventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }

    }
}
