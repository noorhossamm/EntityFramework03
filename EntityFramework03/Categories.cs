using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EntityFramework03
{
    internal class Categories
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }
        public int? ParentCategoryId { get; set; }

        public Categories? ParentCategory { get; set; }

        public ICollection<Categories> SubCategories { get; set; }
            = new List<Categories>();

        public ICollection<Courses> Courses { get; set; }
            = new List<Courses>();

    }
}
