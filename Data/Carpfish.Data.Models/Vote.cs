namespace Carpfish.Data.Models
{
    using Carpfish.Data.Common.Models;

    public class Vote : BaseModel<int>
    {
        public byte Value { get; set; }
    }
}
