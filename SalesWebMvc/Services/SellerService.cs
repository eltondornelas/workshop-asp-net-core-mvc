using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        /*
        public List<Seller> FindAll()
        {
            return _context.Seller.ToList(); //Acessa o banco de dados. Esse acesso é de forma lenta.
            //desse formato a operação é sincrona, ou seja, precisa esperar finalizar o processo para prosseguir a aplicação.
            //por esse motivo o ideal é que ela seja assíncrona para executar a operação de forma separada e a aplicação continua desbloqueada, permitindo assim a aplicação continuar funcionando.
        }
        */
        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync(); 
        }

        /*
        public void Insert(Seller obj)
        {            
            _context.Add(obj);
            _context.SaveChanges();
        }
        */
        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj); //essa operação é feita somente em memória não precisa ser assíncrona.
            await _context.SaveChangesAsync(); //essa operação é que efetivamente vai acessar o banco de dados, então é ela que necessita do async
        }

        /*
        public Seller FindById(int id)
        {
            //return _context.Seller.FirstOrDefault(obj => obj.Id == id);
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
        }
        */
        public async Task<Seller> FindByIdAsync(int id)
        {            
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        /*
        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }
        */
        public async Task RemoveAsync(int id)
        {
            //var obj = _context.Seller.Find(id);
            var obj = await _context.Seller.FindAsync(id);
            _context.Seller.Remove(obj);
            //_context.SaveChanges();
            await _context.SaveChangesAsync();
        }

        /*
        public void Update(Seller obj)
        {
            if (!_context.Seller.Any(x => x.Id == obj.Id))
            {
                throw new NotFoundException("Id not found");
            }

            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch (DbConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }            
        }
        */

        public async Task UpdateAsync(Seller obj)
        {
            //bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id)
            if (!await _context.Seller.AnyAsync(x => x.Id == obj.Id))
            {
                throw new NotFoundException("Id not found");
            }

            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

    }
}
