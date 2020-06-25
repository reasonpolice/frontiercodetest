using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AccountsCodingTest.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AccountsViewer.Controllers
{
    public class AccountsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Retrieve a list of accounts from the codingtest API
        /// </summary>
        /// <returns>A view displaying accounts sorted by status</returns>
        public async Task<IActionResult> ListByStatus(int responsive = 0)
        {
            //I chose to call the API serverside because I would rather utilize razor page functionality than
            //use an AJAX call and concatinate a bunch of html into a string in java
            IEnumerable<Account> accountsList = new List<Account>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://frontiercodingtests.azurewebsites.net/api/accounts/getall"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    accountsList = JsonConvert.DeserializeObject<IEnumerable<Account>>(apiResponse);
                }
            }

            if(responsive == 1)
            {
                return View("ListByStatusResponsive",accountsList.OrderBy(o => o.AccountStatusId).ToList());
            }

            return View(accountsList.OrderBy(o => o.AccountStatusId).ToList());
        }


    }
}
