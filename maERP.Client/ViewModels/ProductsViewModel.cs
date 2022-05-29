﻿using maERP.Client.Contracts.Services;

namespace maERP.Client.ViewModels
{    
    public class ProductsViewModel : ViewModelBase
    {
        readonly INavigationService _navigationService;

        public ProductsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
