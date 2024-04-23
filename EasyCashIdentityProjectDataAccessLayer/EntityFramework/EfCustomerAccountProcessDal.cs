using EasyCashIdentityProjectDataAccessLayer.Abstract;
using EasyCashIdentityProjectDataAccessLayer.Concrete;
using EasyCashIdentityProjectDataAccessLayer.Repostories;
using EasyCashIdentityProjectEntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCashIdentityProjectDataAccessLayer.EntityFramework
{
    public class EfCustomerAccountProcessDal : GenericRepostory<CustomerAccountProcess>, ICustomerAccountProcessDal
    {
        public List<CustomerAccountProcess> MyLastProcess(int id)
        {
            using var context = new Context();
            var values = context.customerAccountProcesses.Include(y=>y.SenderCustomer).Include(w=>w.ReceiverCustomer).ThenInclude(z=>z.AppUser).Where(x=> x.ReceiverID == id || x.SenderID == id).ToList();
            return values;
        }
    }
}
