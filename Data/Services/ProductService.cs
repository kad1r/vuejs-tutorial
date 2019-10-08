using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repository;
using Data.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Models;

namespace Data.Services
{
	public class ProductService //: IBaseService<Product>
	{
		private readonly IUnitOfWork _uow;
		private readonly Repository<Product> _productRepository;

		public ProductService(IUnitOfWork uow, Repository<Product> productRepository)
		{
			_uow = uow;
			_productRepository = productRepository;
		}

		public async Task DeleteAllProducts()
		{
			var products = await _productRepository
				.QueryNoTracking(x => x.IsActive)
				.ToListAsync();

			if (products != null)
			{
				foreach (var product in products)
				{
					product.IsActive = false;
					product.IsDeleted = true;
					_uow.Repository<Product>().ChangeState(product, EntityState.Modified);
				}

				await _uow.SaveChangesAsync();
			}
		}
	}
}
