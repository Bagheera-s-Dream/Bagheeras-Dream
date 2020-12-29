namespace Bagheeras.Dream.Web.ViewModels.Cats
{
    using System;

    using Bagheeras.Dream.Data.Models.Enums;

    public class CatDetailsViewModel
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public string ProfileImage { get; set; }

        public Gender Gender { get; set; }

        public Color Color { get; set; }

        public Breed Breed { get; set; }

        public DateTime Birthday { get; set; }

        public ParentPartialViewModel Mother { get; set; }

        public ParentPartialViewModel Father { get; set; }
    }
}
