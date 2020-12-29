namespace Bagheeras.Dream.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Bagheeras.Dream.Data;
    using Bagheeras.Dream.Data.Models.Cats;
    using Bagheeras.Dream.Services.Data.Contracts;
    using Bagheeras.Dream.Web.ViewModels.Cats;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;

    public class CatsService : ICatsService
    {
        private readonly ApplicationDbContext db;
        private readonly IHostingEnvironment hostingEnvironment;

        public CatsService(ApplicationDbContext db, IHostingEnvironment hostingEnvironment)
        {
            this.db = db;
            this.hostingEnvironment = hostingEnvironment;
        }

        public async Task AddCat(CatInputModel input)
        {
            string fileName = this.UploadFile(input);

            var cat = new Cat()
            {
                Name = input.Name,
                Age = input.Age,
                Birthday = input.Birthday,
                Gender = input.Gender,
                Breed = input.Breed,
                Color = input.Color,
                ProfileImage = fileName,
            };

            await this.db.Cats.AddAsync(cat);
            await this.db.SaveChangesAsync();

            Male father = null;
            Female mother = null;

            if (!string.IsNullOrEmpty(input.FatherName) || !string.IsNullOrWhiteSpace(input.FatherName))
            {
                var fatherCat = await this.db.Males.FirstOrDefaultAsync(c => c.Name == input.FatherName);

                if (fatherCat == null)
                {
                    var catNotFather = await this.db.Cats.FirstOrDefaultAsync(c => c.Name == input.FatherName);
                    this.db.Cats.Remove(catNotFather);

                    father = new Male()
                    {
                        Name = catNotFather.Name,
                        Age = catNotFather.Age,
                        Birthday = catNotFather.Birthday,
                        Gender = catNotFather.Gender,
                        Breed = catNotFather.Breed,
                        Color = catNotFather.Color,
                        FatherId = catNotFather.FatherId,
                        Father = catNotFather.Father,
                        MotherId = catNotFather.MotherId,
                        Mother = catNotFather.Mother,
                        ProfileImage = catNotFather.ProfileImage,
                    };

                    await this.db.Males.AddAsync(father);
                    await this.db.SaveChangesAsync();

                    cat.FatherId = father.CatId;
                    cat.Father = father;

                    father.Children.Add(cat);
                }
                else
                {
                    cat.FatherId = fatherCat.CatId;
                    cat.Father = fatherCat;
                    fatherCat.Children.Add(cat);
                }
            }

            if (!string.IsNullOrEmpty(input.MotherName) || !string.IsNullOrWhiteSpace(input.MotherName))
            {
                var motherCat = await this.db.Females.FirstOrDefaultAsync(c => c.Name == input.MotherName);

                if (motherCat == null)
                {
                    var catNotMother = await this.db.Cats.FirstOrDefaultAsync(c => c.Name == input.MotherName);
                    this.db.Cats.Remove(catNotMother);

                    mother = new Female()
                    {
                        Name = catNotMother.Name,
                        Age = catNotMother.Age,
                        Birthday = catNotMother.Birthday,
                        Gender = catNotMother.Gender,
                        Breed = catNotMother.Breed,
                        Color = catNotMother.Color,
                        FatherId = catNotMother.FatherId,
                        Father = catNotMother.Father,
                        MotherId = catNotMother.MotherId,
                        Mother = catNotMother.Mother,
                        ProfileImage = catNotMother.ProfileImage,
                    };

                    await this.db.Females.AddAsync(mother);
                    await this.db.SaveChangesAsync();

                    cat.MotherId = mother.CatId;
                    cat.Mother = mother;

                    mother.Children.Add(cat);
                }
                else
                {
                    cat.MotherId = motherCat.CatId;
                    cat.Mother = motherCat;

                    motherCat.Children.Add(cat);
                }
            }

            await this.db.SaveChangesAsync();
        }

        public async Task<ICollection<CatViewModel>> GetAll()
        {
            return await this.db.Cats.Select(c => new CatViewModel()
            {
                CatId = c.CatId,
                Name = c.Name,
                Age = c.Age,
                ProfileImage = c.ProfileImage,
            }).ToListAsync();
        }

        public async Task<CatDetailsViewModel> GetCat(string id)
        {
            var cat = await this.db.Cats.FirstOrDefaultAsync(c => c.CatId == id);

            if (cat == null)
            {
                throw new ArgumentException("There is no cat with given id!");
            }

            var model = new CatDetailsViewModel()
            {
                Name = cat.Name,
                Age = cat.Age,
                Birthday = (DateTime)cat.Birthday,
                Gender = cat.Gender,
                Breed = cat.Breed,
                Color = cat.Color,
                ProfileImage = cat.ProfileImage,
            };

            if (!string.IsNullOrEmpty(cat.FatherId))
            {
                var father = await this.db.Cats.FirstOrDefaultAsync(c => c.CatId == cat.FatherId);
                model.Father = new ParentPartialViewModel()
                {
                    CatId = father.CatId,
                    Name = father.Name,
                    ProfileImage = father.ProfileImage,
                };
            }

            if (!string.IsNullOrEmpty(cat.MotherId))
            {
                var mother = await this.db.Cats.FirstOrDefaultAsync(c => c.CatId == cat.MotherId);
                model.Mother = new ParentPartialViewModel()
                {
                    CatId = mother.CatId,
                    Name = mother.Name,
                    ProfileImage = mother.ProfileImage,
                };
            }

            return model;
        }

        public async Task<ICollection<CatViewModel>> GetFemales()
        {
            return await this.db.Cats.Where(c => c.Gender == Dream.Data.Models.Enums.Gender.Female).Select(c => new CatViewModel()
            {
                CatId = c.CatId,
                Name = c.Name,
                Age = c.Age,
                ProfileImage = c.ProfileImage,
            }).ToListAsync();
        }

        public async Task<ICollection<CatViewModel>> GetMales()
        {
            return await this.db.Cats.Where(c => c.Gender == Dream.Data.Models.Enums.Gender.Male).Select(c => new CatViewModel()
            {
                CatId = c.CatId,
                Name = c.Name,
                Age = c.Age,
                ProfileImage = c.ProfileImage,
            }).ToListAsync();
        }

        private string UploadFile(CatInputModel input)
        {
            string fileName = null;
            if (input.ProfileImage != null)
            {
                string uploadDir = Path.Combine(this.hostingEnvironment.WebRootPath, "catImages");
                fileName = Guid.NewGuid().ToString() + "-" + input.ProfileImage.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    input.ProfileImage.CopyTo(fileStream);
                }
            }

            return fileName;
        }
    }
}
