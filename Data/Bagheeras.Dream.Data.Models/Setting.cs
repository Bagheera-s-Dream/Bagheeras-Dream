namespace Bagheeras.Dream.Data.Models
{
    using Bagheeras.Dream.Data.Common.Models;

    public class Setting : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
