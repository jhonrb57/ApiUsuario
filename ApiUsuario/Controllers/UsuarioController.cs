using ApiUsuario.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiUsuario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private string connectionString = "Data Source=DESKTOP-EG9BK65;Initial Catalog=Usuarios;Integrated Security=True;TrustServerCertificate=True";

        // GET: api/<UsuarioController>
        [HttpGet]
        public string Get()
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    var sql = "Select * from Usuario";
                    List<Usuario> list = connection.Query<Usuario>(sql).ToList();
                    StringBuilder sb = new StringBuilder();

                    if (list.Count > 0)
                    {
                        foreach (var i in list)
                        {
                            sb.Append("{\n\tid:" + i.Id.ToString() + ",\n\tnombre:" + i.Nombre.ToString() +
                                ",\n\tcedula:" + i.Cedula.ToString() + ",\n\ttelefono:" + i.Telefono.ToString() +
                                ",\n\tdireccion:" + i.Direccion.ToString() + ",\n\temail:" + i.Email.ToString() + "\n}");
                            sb.Append(",\n");
                        }
                        return "Listado de usuarios:\n" + sb.ToString();
                    }
                    else
                        return "No hay registros para mostrar";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public string Get(string id)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    var sql = "Select * from Usuario where Cedula = " + id;
                    List<Usuario> list = connection.Query<Usuario>(sql).ToList();

                    if (list.Count > 0)
                        return "{\n\tid:" + list[0].Id.ToString() + ",\n" + 
                            "\tnombre:" + list[0].Nombre.ToString() + ",\n" +
                            "\tcedula:" + list[0].Cedula.ToString() + ",\n" +
                            "\ttelefono:" + list[0].Telefono.ToString() + ",\n" +
                            "\tdireccion:" + list[0].Direccion.ToString() + ",\n" +
                            "\temail:" + list[0].Email.ToString() + "\n}";
                    else
                        return "No hay registros con el número de cédula " + id;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public string Post(Usuario model)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                        var sql = "Insert Into Usuario (Id,Nombre,Cedula,Telefono,Direccion,Email) Values(@Id,@Nombre,@Cedula,@Telefono,@Direccion,@Email)";
                        connection.Execute(sql, model);

                        return "Inserción correcta";
                }
            }
            catch (Exception ex)
            {

                return "La inserción ha fallado: " + ex.Message;
            }
        }
    }
}
