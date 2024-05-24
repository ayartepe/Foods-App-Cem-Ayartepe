using Business.Models;
using Business.Services.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Results;
using DataAccess.Results.Bases;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
	public interface IUserService
	{
		IQueryable<UserModel> Query();
		Result Add(UserModel model);
		Result Update(UserModel model);
		Result Delete(int id);
		List<UserModel> GetList() => Query().ToList();
		UserModel GetItem(int id) => Query().SingleOrDefault(q => q.Id == id);
	}

	public class UserService : ServiceBase, IUserService
	{
		public UserService(Db db) : base(db)
		{
		}

		public IQueryable<UserModel> Query()
		{
			// TODO: Foods
			return _db.Users.Include(u => u.Role)
				.OrderByDescending(u => u.IsActive).ThenBy(u => u.RoleId).ThenBy(u => u.UserName)
				.Select(u => new UserModel()
				{
					// entity properties
					Guid = u.Guid,
					Id = u.Id,
					IsActive = u.IsActive,
					Password = u.Password,
					RoleId = u.RoleId,
					Status = u.Status,
					UserName = u.UserName,

					// extra properties
					IsActiveOutput = u.IsActive ? "Yes" : "No",
					RoleOutput = new RoleModel()
					{
						Guid = u.Role.Guid,
						Id = u.Role.Id,
						Name = u.Role.Name
					}
				});
		}

		public Result Add(UserModel model)
		{
			// Way 1:
			//if (_db.Users.Any(u => u.UserName.ToUpper() == model.UserName.ToUpper().Trim() && u.IsActive == true))
			// Way 2:
			if (_db.Users.Any(u => u.UserName.ToUpper() == model.UserName.ToUpper().Trim() && u.IsActive))
				return new ErrorResult("User with the same name exists!");

			User entity = new User()
			{
                // Way 1: Instead of assigning Guid in services' Create method, Guid can be assigned in Record abstract base class
                //Guid = Guid.NewGuid().ToString(),
                IsActive = model.IsActive,
				Password = model.Password.Trim(),

				// Way 1: assign 0 if model's RoleId is null
				//RoleId = model.RoleId ?? 0,
				// Way 2: since model's RoleId is required (can't be null), assign its value
				RoleId = model.RoleId.Value,

				Status = model.Status,
				UserName = model.UserName.Trim()

				// TODO: Foods
			};

			_db.Users.Add(entity);
			_db.SaveChanges();

			model.Id = entity.Id;

			return new SuccessResult("User added successfully.");
		}

		public Result Update(UserModel model)
		{
			if (_db.Users.Any(u => u.Id != model.Id && u.UserName.ToUpper() == model.UserName.ToUpper().Trim() && u.IsActive))
				return new ErrorResult("Working user with the same user name exists");

			User entity = _db.Users.SingleOrDefault(u => u.Id == model.Id);
			if (entity is null)
				return new ErrorResult("User not found!");

			entity.IsActive = model.IsActive;
			entity.Password = model.Password.Trim();
			entity.RoleId = model.RoleId.Value;
			entity.Status = model.Status;
			entity.UserName = model.UserName.Trim();

			// TODO: Foods

			_db.Users.Update(entity);
			_db.SaveChanges();

			return new SuccessResult("User updated successfully.");
		}

		public Result Delete(int id)
		{
			User entity = _db.Users.Include(u => u.UserFoods).SingleOrDefault(u => u.Id == id);
			if (entity is null)
				return new ErrorResult("User not found!");

			_db.UserFoods.RemoveRange(entity.UserFoods);
			_db.Users.Remove(entity);
			_db.SaveChanges();

			return new SuccessResult("User deleted successfully.");
		}
	}
}
