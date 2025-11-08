using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Messages
{

    public record ReleaseInventoryCommand(Guid OrderId, Guid ProductId, int Quantity);
}
