namespace Bagheeras.Dream.Data.Models.Cats
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Bagheeras.Dream.Data.Common.Models;
    using Bagheeras.Dream.Data.Models.Enums;

    public class Cat
    {
        public Cat()
        {
            this.CatId = Guid.NewGuid().ToString();
        }

        public string CatId { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public DateTime? Birthday { get; set; }

        [Required]
        public virtual int ColorId { get; set; }

        [EnumDataType(typeof(Color))]
        public Color Color
        {
            get
            {
                return (Color)this.ColorId;
            }

            set
            {
                this.ColorId = (int)value;
            }
        }

        [Required]
        public virtual int BreedId { get; set; }

        [EnumDataType(typeof(Breed))]
        public Breed Breed
        {
            get
            {
                return (Breed)this.BreedId;
            }

            set
            {
                this.BreedId = (int)value;
            }
        }

        [Required]
        public virtual int GenderId { get; set; }

        [EnumDataType(typeof(Gender))]
        public Gender Gender
        {
            get
            {
                return (Gender)this.GenderId;
            }

            set
            {
                this.GenderId = (int)value;
            }
        }

        public string ProfileImage { get; set; }

        public string? FatherId { get; set; }

        public virtual Male Father { get; set; }

        public string? MotherId { get; set; }

        public virtual Female Mother { get; set; }
    }
}
