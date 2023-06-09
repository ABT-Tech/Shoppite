using System;
using System.Collections.Generic;

namespace Shoppite.Application.Models
{ 
    public class CartModel
    {
        public  List<OrderBasicModel> OrderBasicModels { get; set; }
        public  List<f_getproduct_CartDetails_By_OrgidModel> F_Getproduct_CartDetails_By_Orgids { get; set; }
        public OrderShippingModel OrderShippingModel { get; set; }
        public OrderBasicModel OrderBasicModel { get; set; }
        public UsersProfileModal UsersProfileModal { get; set; }
        public Reward_Point_LogModel reward_Point_Log { get; set; }
        public List<Reward_Point_LogModel> myreward { get; set; }
        public List<f_order_masterDetailsModel> MyOrderDetails { get; set; }
        public bool IsPaytm { get; set; }
        public bool IsReward { get; set; }
        public decimal? TotalAmount { get; set; }
    }
}
