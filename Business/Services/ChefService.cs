using Business.Models;
using Business.Services.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Results;
using DataAccess.Results.Bases;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public interface IChefservice
    {
        IQueryable<ChefModel> Query();
        Result Add(ChefModel model);
        Result Update(ChefModel model);
        Result Delete(int id);
    }

    public class ChefService : ServiceBase, IChefservice
    {

		public ChefService(Db db) : base(db)
		{
		}

		public IQueryable<ChefModel> Query()
        {
            return _db.Chefs.Include(p => p.Foods).Select(p => new ChefModel()
            {

                Guid = p.Guid,
                Id = p.Id,
                Name = p.Name,

                Foods = string.Join("<br />", p.Foods.Select(g => g.Name))
            });
        }

        public Result Add(ChefModel model)
        {
            if (_db.Chefs.Any(p => p.Name.ToLower() == model.Name.ToLower().Trim()))
                return new ErrorResult("Chef with the same name exists!");
            Chef entity = new Chef()
            {

                Name = model.Name.Trim()
            };


            _db.Add(entity);

            _db.SaveChanges();
            return new SuccessResult();
        }

        public Result Update(ChefModel model)
        {
            if (_db.Chefs.Any(p => p.Id != model.Id && p.Name.ToLower() == model.Name.ToLower().Trim()))
                return new ErrorResult("Chef with the same name exists!");
            Chef entity = _db.Chefs.Find(model.Id);
            if (entity is null)
                return new ErrorResult("Chef not found!");
            entity.Name = model.Name.Trim();

            _db.Update(entity);

            _db.SaveChanges();
            return new SuccessResult();
        }

        public Result Delete(int id)
        {
            Chef entity = _db.Chefs.Include(r => r.Foods).SingleOrDefault(p => p.Id == id);
            if (entity is null)
                return new ErrorResult("Chef not found!");
            if (entity.Foods is not null && entity.Foods.Any())
                return new ErrorResult("Chef can't be deleted because it has relational foods!");
            
            _db.Remove(entity);

            _db.SaveChanges();
            return new SuccessResult();
        }
    }
}
