using Common.Conventions.Commands;

namespace Common.Conventions
{
    public interface ICreateEntityContext : ICommandContext
    {
        long IdAfterCreate { get; set; }
    }
}
