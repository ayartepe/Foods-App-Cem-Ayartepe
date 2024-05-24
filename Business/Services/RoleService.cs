using Business.Models;
using Business.Services.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Results;
using DataAccess.Results.Bases;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public interface IRoleService
    {
        IQueryable<RoleModel> Query();
        Result Add(RoleModel model);
        Result Update(RoleModel model);
        Result Delete(int id);
    }

    public class RoleService : ServiceBase, IRoleService
    {
		public RoleService(Db db) : base(db)
		{
		}

		public IQueryable<RoleModel> Query()
        {
            return _db.Roles.Include(r => r.Users).OrderByDescending(r => r.Users.Count).ThenBy(r => r.Name).Select(roleEntity => new RoleModel()
            {
                Guid = roleEntity.Guid,
                Id = roleEntity.Id,
                Name = roleEntity.Name,

                UserCount = roleEntity.Users.Count,
                Users = string.Join("<br />", roleEntity.Users.OrderBy(u => u.UserName).Select(u => u.UserName))
            });
        }

        public Result Add(RoleModel model)
        {

            if (_db.Roles.Any(r => r.Name.ToLower() == model.Name.ToLower().Trim()))
                return new ErrorResult("Role with the same name exists!");
            Role entity = new Role()
            {

                Name = model.Name.Trim(),
            };
            _db.Roles.Add(entity);
            _db.SaveChanges();
            return new SuccessResult("Role created successfully.");
        }

        public Result Update(RoleModel model)
        {
            if (_db.Roles.Any(r => r.Id != model.Id && r.Name.ToLower() == model.Name.ToLower().Trim()))
                return new ErrorResult("Role with the same name exists!");


            Role entity = _db.Roles.Find(model.Id);

            if (entity is null)
                return new ErrorResult("Role not found!");
            entity.Name = model.Name.Trim();
            _db.Roles.Update(entity);
            _db.SaveChanges();
            return new SuccessResult("Role updated successfully.");
        }

        public Result Delete(int id)
        {
            Role entity = _db.Roles.Include(r => r.Users).SingleOrDefault(r => r.Id == id);
            if (entity is null)
                return new ErrorResult("Role not found!");
            if (entity.Users is not null && entity.Users.Any()) 
                return new ErrorResult("Role can't be deleted because it has relational users!");
            _db.Roles.Remove(entity);
            _db.SaveChanges();
            return new SuccessResult("Role deleted successfully.");
        }
    }
}
