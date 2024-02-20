using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using TS.Models;
using System.Text;

namespace TS.Controllers
{
    public class AccountController : Controller
    {
        private readonly string _connectionString = "Server=DESKTOP-GT13BSE;Database=TS;Trusted_Connection=True;MultipleActiveResultSets=true";
        private readonly UserManager<RegisteredUser> _userManager;
        private readonly SignInManager<RegisteredUser> _signInManager; // Make sure this is properly injected

        public AccountController(UserManager<RegisteredUser> userManager, SignInManager<RegisteredUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SaveChange(RegisteredUser model)
        {
            var oldEmail = User.Identity.Name;

            // Start with the base query
            var queryBuilder = new StringBuilder("UPDATE [Identity].[RegisteredUser] SET");

            // Collect parameters for the dynamic query
            var parameters = new List<SqlParameter>();

            // Add non-null parameters to the query


            if (!string.IsNullOrEmpty(model.UserPhone))
            {
                queryBuilder.Append(" UserPhone = @NewUserPhone,");
                parameters.Add(new SqlParameter("@NewUserPhone", model.UserPhone));
            }

            if (!string.IsNullOrEmpty(model.LastName))
            {
                queryBuilder.Append(" LastName = @NewLastName,");
                parameters.Add(new SqlParameter("@NewLastName", model.LastName));
            }

            if (!string.IsNullOrEmpty(model.FirstName))
            {
                queryBuilder.Append(" FirstName = @NewFirstName,");
                parameters.Add(new SqlParameter("@NewFirstName", model.FirstName));
            }

            if (!string.IsNullOrEmpty(model.FirstName))
            {
                queryBuilder.Append(" UserName = @NewUserName,");
                parameters.Add(new SqlParameter("@NewUserName", model.FirstName));
            }

            // Remove the trailing comma
            if (queryBuilder[queryBuilder.Length - 1] == ',')
            {
                queryBuilder.Length--;
            }

            // Add the WHERE clause
            queryBuilder.Append(" WHERE email = @OldEmail;");

            // Add the old email parameter
            parameters.Add(new SqlParameter("@OldEmail", oldEmail));

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(queryBuilder.ToString(), connection))
                {
                    // Add parameters to the query
                    command.Parameters.AddRange(parameters.ToArray());

                    // Execute the query
                    command.ExecuteNonQuery();
                }
            }

  

            // Sign out the user
            await _signInManager.SignOutAsync();

            // Redirect to a success page
            return RedirectToAction("SaveChange");
        }



        public ActionResult SaveChange()
        {
            // Action for a successful update
            return View();
        }
    }
}
