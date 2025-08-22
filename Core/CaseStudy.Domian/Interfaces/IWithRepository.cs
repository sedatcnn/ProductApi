using CaseStudy.Domian.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CaseStudy.Domian.Interfaces
{
    public interface IWithRepository
    {
        List<Product> GetProductAsync();
        List<User> GetUserAsync();

    }
}
