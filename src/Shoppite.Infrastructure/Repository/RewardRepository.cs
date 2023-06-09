using Microsoft.AspNetCore.Http;
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
        protected readonly IHttpContextAccessor _httpContext;
        public RewardRepository(Shoppite_masterContext dbContext,IHttpContextAccessor httpContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _httpContext = httpContext;
           
        }
        public async Task<IEnumerable<Reward_Point_Log>> GetRewardBalance(int Orgid)
        {
             string Username = _httpContext.HttpContext.User.Identity.Name;
            var finduser =  _dbContext.Users.Where(x => x.Email == Username&x.OrgId==Orgid).FirstOrDefault();
            return  _dbContext.Reward_Point_Logs.Where(r => r.ProfileGUID == finduser.Guid&&r.OrgId==Orgid).ToList();
        }
        public async Task AddRewards(Reward_Point_Log reward)
        {
            string Username= _httpContext.HttpContext.User.Identity.Name;

            var finduser = _dbContext.Users.Where(x=>x.Email== Username&&x.OrgId==reward.OrgId).FirstOrDefault();           
            reward.Date_created = DateTime.Now;
            reward.Reward_type = "Promotional";       
            reward.ProfileGUID = finduser.Guid;
            reward.Operation_type = "Credit";
            reward.Expired_on = DateTime.Now.AddMonths(6);
            _dbContext.Reward_Point_Logs.Add(reward);
            await  _dbContext.SaveChangesAsync();                  
        }
        public async Task ClaimReward(Reward_Point_Log reward)
        {
            string Username = _httpContext.HttpContext.User.Identity.Name;
            var finduser = _dbContext.Users.Where(x => x.Email == Username&&x.OrgId==reward.OrgId).FirstOrDefault();          
            reward.ProfileGUID = finduser.Guid;
            reward.Operation_type = "Debit";
            reward.Date_created = DateTime.Now;
            _dbContext.Reward_Point_Logs.Add(reward);
            await _dbContext.SaveChangesAsync();
        }
    }
}
