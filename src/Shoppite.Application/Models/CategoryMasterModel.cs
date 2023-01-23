using Shoppite.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shoppite.Application.Models
{
    public class CategoryMasterModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Urlpath { get; set; }
        public int ParentCategoryId { get; set; }
        public string Image { get; set; }
        public string ProductName { get; set; }
        public IEnumerable<CategoryMasterModel> CategoryDetails { get; set; }
        public List<CategoryMasterModel> products { get; set; }
    }
}
