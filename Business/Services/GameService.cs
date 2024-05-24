using Business.Models;
using Business.Services.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Results;
using DataAccess.Results.Bases;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Business.Services
{
	public interface IFoodservice
	{
		IQueryable<FoodModel> Query();
		Result Add(FoodModel model);
		Result Update(FoodModel model);
		Result Delete(int id);
		List<FoodModel> GetList() => Query().ToList();
		FoodModel GetItem(int id) => Query().SingleOrDefault(g => g.Id == id);
	}

	public class Foodservice : ServiceBase, IFoodservice
	{
		public Foodservice(Db db) : base(db)
		{
		}

		public IQueryable<FoodModel> Query()
		{
			return _db.Foods.Include(g => g.Chef).Include(g => g.UserFoods).ThenInclude(ug => ug.User)
				.OrderByDescending(g => g.OrderDate).ThenByDescending(g => g.SalesPrice).ThenBy(g => g.Name)
				.Select(g => new FoodModel()
				{
					// entity properties
					Guid = g.Guid,
					Id = g.Id,
					Name = g.Name,
					OrderDate = g.OrderDate,
					ChefId = g.ChefId,
					SalesPrice = g.SalesPrice,

					// extra properties
					ChefOutput = g.Chef.Name,

                    // Way 1:
                    //SalesPriceOutput = g.SalesPrice != null ? g.SalesPrice.Value.ToString("C2", new CultureInfo("en-US")) : "", // tr-TR
                    // Way 2:
                    //SalesPriceOutput = g.SalesPrice.HasValue ? g.SalesPrice.Value.ToString("C2", new CultureInfo("en-US")) : string.Empty,
                    // Way 3: 
                    //SalesPriceOutput = g.SalesPrice.HasValue ? g.SalesPrice.Value.ToString("C2") : string.Empty, // N: number format, C: currency format, 2: number of decimal digits
                    // Way 4: Managing CultureInfo in MvcControllerBase
                    SalesPriceOutput = (g.SalesPrice ?? 0).ToString("C2"),

                    // Way 1:
                    //OrderDateOutput = g.OrderDate.HasValue ? g.OrderDate.Value.ToString("MM/dd/yyyy HH:mm:ss", new CultureInfo("en-US")) : string.Empty, // 2 digits month/2 digits day/4 digits year 2 digits hour:2 digits minute:2 digits second
                    // Way 2:
                    //OrderDateOutput = g.OrderDate.HasValue ? g.OrderDate.Value.ToString("MM/dd/yyyy", new CultureInfo("en-US")) : string.Empty
                    // Way 3: Managing CultureInfo in MvcControllerBase
					OrderDateOutput = g.OrderDate.HasValue ? g.OrderDate.Value.ToString("MM/dd/yyyy") : string.Empty,

					Users = g.UserFoods.Select(ug => new UserModel()
					{
						UserName = ug.User.UserName,
						Status = ug.User.Status
						// other properties can be assigned if needed
					}).ToList(),

					UsersInput = g.UserFoods.Select(ug => ug.UserId).ToList()
                });
		}

		public Result Add(FoodModel model)
		{
			if (_db.Foods.Any(g => g.Name.ToLower() == model.Name.ToLower().Trim()))
				return new ErrorResult("Food with the same name exists!");
			Food entity = new Food()
			{
				// Way 1: Instead of assigning Guid in services' Create method, Guid can be assigned in Record abstract base class
				//Guid = Guid.NewGuid().ToString(),
				Name = model.Name.Trim(),
				OrderDate = model.OrderDate,
				SalesPrice = model.SalesPrice,
				ChefId = model.ChefId,

				// Way 2: filling entity's UserFoods collection elements (ternary operator)
				//UserFoods = model.UsersInput is null ? null : model.UsersInput.Select(userInput => new UserGame()
				//{
				//	UserId = userInput
				//}).ToList()
				// Way 3: filling entity's UserFoods collection elements
				UserFoods = model.UsersInput?.Select(userInput => new UserFood()
				{
					UserId = userInput
				}).ToList()
			};

			// Way 1: filling entity's UserFoods collection elements
			//entity.UserFoods = new List<UserGame>();
			//foreach (int userInput in model.UsersInput)
			//{
			//	entity.UserFoods.Add(new UserGame()
			//	{
			//		UserId = userInput
			//	});
			//}

			_db.Foods.Add(entity);
			_db.SaveChanges();

			model.Id = entity.Id;

			return new SuccessResult();
		}

		public Result Update(FoodModel model)
		{
			if (_db.Foods.Any(g => g.Id != model.Id && g.Name.ToLower() == model.Name.ToLower().Trim()))
				return new ErrorResult("Food with the same name exists!");

			Food entity = _db.Foods.Include(g => g.UserFoods).SingleOrDefault(g => g.Id == model.Id);
			if (entity is null)
				return new ErrorResult("Food not found!");

			_db.UserFoods.RemoveRange(entity.UserFoods);

			entity.Name = model.Name.Trim();
			entity.OrderDate = model.OrderDate;
			entity.SalesPrice = model.SalesPrice;
			entity.ChefId = model.ChefId;
			entity.UserFoods = model.UsersInput?.Select(userInput => new UserFood()
			{
				UserId = userInput
			}).ToList();

			_db.Foods.Update(entity);
			_db.SaveChanges();

			return new SuccessResult();
		}

		public Result Delete(int id)
		{
			Food entity = _db.Foods.Include(g => g.UserFoods).SingleOrDefault(g => g.Id == id);
			if (entity is null)
				return new ErrorResult("Food not found!");

			_db.UserFoods.RemoveRange(entity.UserFoods);

			_db.Foods.Remove(entity);
			_db.SaveChanges();

			return new SuccessResult("Food deleted successfully.");
		}
	}
}
