using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public record ProductEvent(string Type, Guid ProductId, Guid SellerId, string? Title, decimal? NewPrice = null);
}
