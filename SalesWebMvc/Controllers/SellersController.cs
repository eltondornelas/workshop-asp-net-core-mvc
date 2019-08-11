using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;


namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        //public IActionResult Index()
        public async Task<IActionResult> Index() //como o controlador precisa respeitar o padrão de nomes do framework, não vamos mudar no nome do Index
        {
            //var list = _sellerService.FindAll();
            var list = await _sellerService.FindAllAsync();
            return View(list);
        }

        //public IActionResult Create()
        public async Task<IActionResult> Create()
        {
            //var departments = _departmentService.FindAll();
            var departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //public IActionResult Create(Seller seller) //isso é uma ação de POST e não de get. É após a execução
        public async Task<IActionResult> Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                //var departments = _departmentService.FindAll();
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }
            
            await _sellerService.InsertAsync(seller);
            //return RedirectToAction("Index");
            return RedirectToAction(nameof(Index));
        }

        //public IActionResult Delete(int? id)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            //var obj = _sellerService.FindById(id.Value);
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //public IActionResult Delete(int id)
        public async Task<IActionResult> Delete(int id)
        {
            //_sellerService.Remove(id);
            await _sellerService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        //public IActionResult Details(int? id)
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            //var obj = _sellerService.FindById(id.Value);
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        //public IActionResult Edit(int? id)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            //var obj = _sellerService.FindById(id.Value);
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            //List<Department> departments = _departmentService.FindAll();
            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //public IActionResult Edit(int id, Seller seller)
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                //var departments = _departmentService.FindAll();
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }

            if (id != seller.Id)
            {                
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }

            try
            {
                //_sellerService.Update(seller);
                await _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {//ApplicationException é um supertipo dos dois erros abaixo.
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            /*
            catch (NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            catch (DbConcurrencyException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            */
        }

        public IActionResult Error(string message) //não precisa ser assíncrona porque não tem nenhum acesso a dados
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}