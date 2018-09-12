using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Conventions;
using Common.Conventions.Queries;
using Common.Models.Criterions;
using DelegateDecompiler.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Queries
{
    public class GetQuery<TEntity, TResult> : IQuery<GetCriterion, TEntity, IQueryable<TResult>>
        where TEntity : class
    {
        private readonly ILinqProvider linqProvider;
        private readonly IMapper mapper;

        public GetQuery(ILinqProvider linqProvider, IMapper mapper)
        {
            this.linqProvider = linqProvider;
            this.mapper = mapper;
        }

        public Task<IQueryable<TResult>> AskAsync(GetCriterion criterion)
        {
            return Task.FromResult(
                linqProvider.Query<TEntity>()
                    .ProjectTo<TResult>(mapper.ConfigurationProvider)
                    .DecompileAsync()
            );
        }
    }
}
