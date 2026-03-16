using Microsoft.AspNetCore.Mvc.RazorPages;

using Microsoft.Data.SqlClient;

<<using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace paff.Pages
{
    public class IndexModel : PageModel
    {
        public List<Etudiant> Etudiants { get; set; } = new();
        public string ErreurBDD { get; set; } = "";

        public void OnGet()
        {
            try
            {
                string connStr = "Server=tcp:paffito.database.windows.net,1433;Initial Catalog=paffito;Persist Security Info=False;User ID=sqladmin;Password=Tp3Azure2025!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

                using var conn = new SqlConnection(connStr);
                conn.Open();
                using var cmd = new SqlCommand("SELECT * FROM Etudiants", conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Etudiants.Add(new Etudiant
                    {
                        Id = reader.GetInt32(0),
                        Nom = reader.GetString(1),
                        Prenom = reader.GetString(2),
                        Email = reader.GetString(3)
                    });
                }
            }
            catch (Exception)
            {
                ErreurBDD = "Base de données non disponible.";
            }
        }
    }

    public class Etudiant
    {
        public int Id { get; set; }
        public string Nom { get; set; } = "";
        public string Prenom { get; set; } = "";
        public string Email { get; set; } = "";
    }
}
