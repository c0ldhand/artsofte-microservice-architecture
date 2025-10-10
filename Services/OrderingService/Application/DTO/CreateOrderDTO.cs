using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public record CreateOrderDTO(Guid BuyerId, Guid SellerId, List<CreateOrderItemDTO> Items);
    
}
