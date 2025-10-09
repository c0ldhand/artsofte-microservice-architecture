using Domain.Entities;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class OrderService
    {
        public readonly UnitOfWork _unitOfWork;
        public OrderService(UnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

    }
}
