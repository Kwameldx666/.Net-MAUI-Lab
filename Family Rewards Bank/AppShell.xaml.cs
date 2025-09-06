namespace Family_Rewards_Bank
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AddEventPage), typeof(AddEventPage));
            Routing.RegisterRoute(nameof(UpdateEventPage), typeof(UpdateEventPage));
        }
    }
}
