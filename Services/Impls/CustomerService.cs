using AutoMapper;
using DTOs;
using Repositories.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Impls
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository,
                                IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<GetCustomerDTO> GetCustomerByPhoneNumber(string phoneNumber)
        {
            try
            {
                var entity = _customerRepository.GetFirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
                return _mapper.Map<GetCustomerDTO>(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
