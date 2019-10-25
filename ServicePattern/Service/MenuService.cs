using Data.Repository;
using Model.Models;
using ServicePattern.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicePattern.Service
{
    public class MenuService : IService<Menu>, IMenuService
    {
		private readonly IRepository<Menu> _repository;

		public MenuService(IRepository<Menu> repository)
		{
			_repository = repository;
		}
    }
}
