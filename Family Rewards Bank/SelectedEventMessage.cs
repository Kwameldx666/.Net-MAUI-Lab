using CommunityToolkit.Mvvm.Messaging.Messages;
using Family_Rewards_Bank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Family_Rewards_Bank
{
    public class SelectedEventMessage : ValueChangedMessage<EventItem>
    {
        public SelectedEventMessage(EventItem eventItem) : base(eventItem) { }
    }

}
