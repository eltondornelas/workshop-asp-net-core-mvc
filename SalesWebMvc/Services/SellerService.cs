using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        } //desse formato a operação é sincrona, ou seja, previsa esperar finalizar o processo para prosseguir

        public void Insert(Seller obj)
        {
            obj.Department = _context.Department.First(); //associa com o vendedor o primeiro dado do banco de dados
            _context.Add(obj);
            _context.SaveChanges();
        }
    }
}
