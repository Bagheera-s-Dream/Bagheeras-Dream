namespace Bagheeras.Dream.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Bagheeras.Dream.Web.ViewModels.Cats;

    public interface ICatsService
    {
        public Task<ICollection<CatViewModel>> GetAll();

        public Task<ICollection<CatViewModel>> GetMales();

        public Task<ICollection<CatViewModel>> GetFemales();

        public Task<CatDetailsViewModel> GetCat(string id);

        public Task AddCat(CatInputModel cat);
    }
}
