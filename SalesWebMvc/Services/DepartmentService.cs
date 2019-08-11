using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMvcContext _context;

        public DepartmentService(SalesWebMvcContext context)
        {
            _context = context;
        }

        /*
        public List<Department> FindAll()
        {
            return _context.Department.OrderBy(x => x.Name).ToList();
        }
        //Modelo síncrono
        */

        public async Task<List<Department>> FindAllAsync() //esse sufixo é recomendado pela linguagem C#
        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync(); //ToList é uma operação síncrona do Linq. a versão assíncrona é do entity framework e não do linq, precisa declarar a biblioteca: Microsoft.EntityFrameworkCore;
            //necessário escrever o await para informar ao compilador que é assíncrona
        }
    }
}
