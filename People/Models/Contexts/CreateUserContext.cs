using Common.Conventions;

namespace People.Models.Contexts
{
    public class CreateUserContext : ICreateEntityContext
    {
        public long IdAfterCreate { get; set; }
    }
}
