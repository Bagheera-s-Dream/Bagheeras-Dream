namespace Bagheeras.Dream.Data.Models.Contracts
{
    using Bagheeras.Dream.Data.Models.Cats;
    using System.Collections.Generic;

    public interface IParent
    {
        ICollection<Cat> Children { get; set; }
    }
}
