using Family_Rewards_Bank.Views.Lab3;


namespace Family_Rewards_Bank
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Organizer), typeof(Organizer));
            Routing.RegisterRoute(nameof(AddEventPage), typeof(AddEventPage));
            Routing.RegisterRoute(nameof(UpdateEventPage), typeof(UpdateEventPage));
            Routing.RegisterRoute(nameof(WebServiceEmulation), typeof(WebServiceEmulation));
        


        }
    }
}
