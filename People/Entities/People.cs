using Common.Conventions;
using DelegateDecompiler;

namespace People.Entities
{
    public class People : IHasKey<long>
    {
        public long Id { get; set; }

        public string Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Email { get; set; }
        public string PictureMedium { get; set; }
        public string Quote { get; set; }

        [Computed]
        public string Address => $"{City} {Street}";
    }
}
