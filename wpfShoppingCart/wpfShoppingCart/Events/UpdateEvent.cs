using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;

namespace wpfShoppingCart.Events
{
    public class UpdateEvent:PubSubEvent<FinalOrder>
    {
    }
}
