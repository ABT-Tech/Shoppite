using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shoppite.Application.Models
{
    public class UserCoverImageModel
    {
        public IFormFile ProfileImage { get; set; }
        public string CoverImage { get; set; }
        public int? OrgId { get; set; }
        public int? UserId { get; set; }
        public int ProfileId { get; set; }
    }
}
