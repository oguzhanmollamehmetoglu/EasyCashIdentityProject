using EasyCashIdentityProject.DtoLayer.Dtos.CustomerAccountsProcressDtos;
using EasyCashIdentityProjectBusinessLayer.Abstract;
using EasyCashIdentityProjectDataAccessLayer.Concrete;
using EasyCashIdentityProjectEntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EasyCashIdentityProject.PresentationLayer.Controllers
{
	public class SendMoneyController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly ICustomerAccountProcessService _customerAccountProcessService;

        public SendMoneyController(UserManager<AppUser> userManager, ICustomerAccountProcessService customerAccountProcessService)
        {
            _userManager = userManager;
            _customerAccountProcessService = customerAccountProcessService;
        }

        [HttpGet]
		public IActionResult Index(string mycurrency)
		{
			ViewBag.currency = mycurrency;
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Index(SendMoneyForCustomerAccountProcressDto sendMoneyForCustomerAccountProcressDto)
		{
			var context = new Context();

			var user = await _userManager.FindByNameAsync(User.Identity.Name);
			var receiverAccountNumberID = context.CustomerAccounts.Where(x => x.CustomerAccountNumber == 
			sendMoneyForCustomerAccountProcressDto.ReceiverAccountNumber).Select(y => y.CustomerAccountID).FirstOrDefault();

			var senderAccountNumberID = context.CustomerAccounts.Where(x=>x.AppUserID == user.Id).Where(y=>y.CustomerAccountCurrency ==
			    "Türk Liarsı").Select(z=>z.CustomerAccountID).FirstOrDefault();

			var values = new CustomerAccountProcess();
			values.ProcessDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
			values.SenderID = senderAccountNumberID;
			values.ProcessType = "Havale";
			values.ReceiverID = receiverAccountNumberID;
			values.Amount = sendMoneyForCustomerAccountProcressDto.Amount;
			values.Description = sendMoneyForCustomerAccountProcressDto.Description;

            _customerAccountProcessService.TInsert(values);

            return RedirectToAction("Index","Deneme");
		}


	}
}
