using System;
using System.Collections.Generic;
using Family_Rewards_Bank.Models;
using Family_Rewards_Bank.ViewModels;
using Microsoft.Maui.Controls;

namespace Family_Rewards_Bank
{
    public partial class Organizer : ContentPage
    {
        public Organizer()
        {
            InitializeComponent();
            BindingContext = new OrganizerViewModel();
        }
    }

}
