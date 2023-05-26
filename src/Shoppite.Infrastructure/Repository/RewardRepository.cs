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
            var finduser =  _dbContext.Users.Where(x => x.Email == Username).FirstOrDefault();
            return  _dbContext.Reward_Point_Logs.Where(r => r.UserId == finduser.UserId&&r.OrgId==Orgid).ToList();
        }
        public async Task AddRewards(Reward_Point_Log reward)
        {
            string Username= _httpContext.HttpContext.User.Identity.Name;

            var finduser = _dbContext.Users.Where(x=>x.Email== Username).FirstOrDefault();
            // Reward_Point_Log reward_Point = new Reward_Point_Log();
            reward.Date_created = DateTime.Now;
            reward.Reward_type = "Promotional";
            reward.Reward_points = 100;
            reward.UserId = finduser.UserId;
            reward.Operation_type = "Credit";
            reward.Expired_on = DateTime.Now.AddMonths(6);
            _dbContext.Reward_Point_Logs.Add(reward);
            await  _dbContext.SaveChangesAsync();                  
        }
        public async Task ClaimReward(Reward_Point_Log reward)
        {
            string Username = _httpContext.HttpContext.User.Identity.Name;
            var finduser = _dbContext.Users.Where(x => x.Email == Username).FirstOrDefault();          
            reward.UserId = finduser.UserId;
            reward.Operation_type = "Debit";
            reward.Date_created = DateTime.Now;
            _dbContext.Reward_Point_Logs.Add(reward);
            await _dbContext.SaveChangesAsync();
        }
    }
}
