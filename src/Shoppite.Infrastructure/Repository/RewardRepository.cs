using Shoppite.Core.Entities;
using Shoppite.Core.Repositories;
using Shoppite.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Infrastructure.Repository
{
    public class RewardRepository:IRewardRepository
    {
        protected readonly Shoppite_masterContext _dbContext;
        public RewardRepository(Shoppite_masterContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<IEnumerable<Reward_Point_Log>> GetRewardBalance(int Orgid, int ProfileId)
        {
            return  _dbContext.Reward_Point_Logs.Where(r => r.UserId == ProfileId&&r.OrgId==Orgid).ToList();
        }
    }
}
