using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Activities
{
    public interface IRoutingSlipCreated
    {
        Guid RoutingSlipId { get; }
        DateTime CreatedAt { get; }
    }
}
