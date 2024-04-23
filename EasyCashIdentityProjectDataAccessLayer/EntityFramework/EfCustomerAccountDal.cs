using EasyCashIdentityProjectDataAccessLayer.Abstract;
using EasyCashIdentityProjectDataAccessLayer.Concrete;
using EasyCashIdentityProjectDataAccessLayer.Repostories;
using EasyCashIdentityProjectEntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCashIdentityProjectDataAccessLayer.EntityFramework
{
    public class EfCustomerAccountDal : GenericRepostory<CustomerAccount>, ICustomerAccountDal
    {
        public List<CustomerAccount> GetCustomerAccountsList(int id)
        {
            using var context = new Context();
            var values = context.CustomerAccounts.Where(x=>x.AppUserID == id).ToList();
            return values;
        }
    }
}
