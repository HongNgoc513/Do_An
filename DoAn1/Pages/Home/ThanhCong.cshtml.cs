using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DoAn1.Pages.Home
{
    public class ThanhCongModel : PageModel
    {
        public string ResponseCode { get; set; }
        public void OnGet([FromQuery(Name = "responseCode")] string responseCode)
        {
            ResponseCode = responseCode;

        }
    }
}
