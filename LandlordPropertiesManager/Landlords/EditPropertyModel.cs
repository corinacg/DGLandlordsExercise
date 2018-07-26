using LandlordPropertiesManager.Support;
using System;
using System.ComponentModel.DataAnnotations;

namespace LandlordPropertiesManager.Landlords
{
    class EditPropertyModel : ValidatableBindableBase
    {
        private string housenumber;
        private string street;
        private string town;
        private string postCode;
        private DateTime availableFrom;
        private string status;

        [Required]
        public string Housenumber
        {
            get { return housenumber; }
            set { SetProperty(ref housenumber, value); }
        }

        [Required]
        public string Street
        {
            get { return street; }
            set { SetProperty(ref street, value); }
        }
        
        [Required]
        public string Town
        {
            get { return town; }
            set { SetProperty(ref town, value); }
        }

        [Required]
        public string PostCode
        {
            get { return postCode; }
            set { SetProperty(ref postCode, value); }
        }

        [Required]
        public DateTime AvailableFrom
        {
            get { return availableFrom; }
            set { SetProperty(ref availableFrom, value); }
        }

        [Required]
        public string Status
        {
            get { return status; }
            set { SetProperty(ref status, value); }
        }
    }
}
