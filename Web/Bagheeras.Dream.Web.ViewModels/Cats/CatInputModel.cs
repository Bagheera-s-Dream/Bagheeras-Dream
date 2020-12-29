namespace Bagheeras.Dream.Web.ViewModels.Cats
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Bagheeras.Dream.Data.Models.Enums;
    using Microsoft.AspNetCore.Http;

    public class CatInputModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        public Breed Breed { get; set; }

        [Required]
        public Color Color { get; set; }

        [Required]
        public DateTime? Birthday { get; set; }

        [Required]
        public IFormFile ProfileImage { get; set; }

        public string FatherName { get; set; }

        public string MotherName { get; set; }
    }
}
