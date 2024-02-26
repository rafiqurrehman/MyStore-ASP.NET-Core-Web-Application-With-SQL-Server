using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyStore.Pages
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost() 
        {
            clientInfo.name = Request.Form["name"];
			clientInfo.email = Request.Form["email"];
			clientInfo.phone = Request.Form["phone"];
			clientInfo.address = Request.Form["address"];

            if (clientInfo.name.Length == 0 || clientInfo.email.Length == 0 ||

                clientInfo.phone.Length == 0 || clientInfo.address.Length == 0)
            {
                errorMessage = "All the feilds are required";
                return;
            
            }

            // save the new client into the database 

            try 
            {
                String connectionString = "Data Source=DESKTOP-JN90RJA\\SQLEXPRESS; Initial Catalog=MyStore; User Id=sa; Password=rafiq";
                using(SqlConnection connection = new SqlConnection(connectionString)) 
                {
                    connection.Open();
                    String sql = "insert into clients" +
                                 "(name,email,phone,address) values" +
                                 "(@name,@email,@phone,@address);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", clientInfo.name);
						command.Parameters.AddWithValue("@email", clientInfo.email);
						command.Parameters.AddWithValue("@phone", clientInfo.phone);
						command.Parameters.AddWithValue("@address", clientInfo.address);

                        command.ExecuteNonQuery();
					}

				}

			}

            catch (Exception ex) 
            {
                errorMessage = ex.Message;
                return;
            }


            clientInfo.name = ""; clientInfo.email = ""; clientInfo.phone = ""; clientInfo.address = "";
            successMessage = "New client added Correctly";

            Response.Redirect("/Index1");

		}
	}
}
