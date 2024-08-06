using Dapper;
using Models;
using BaseDatos;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiUsuario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ConexionBd conexion = new ConexionBd();

        // GET: api/<UsuarioController>
        [HttpGet]
        public string Get()
        {
            try
            {
                using (var connection = new SqlConnection(conexion.Conexion()))
                {
                    List<Usuario> list = connection.Query<Usuario>("pp_listar", commandType: CommandType.StoredProcedure).ToList();
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
        public Usuario? Get(string id)
        {
            try
            {
                using (var connection = new SqlConnection(conexion.Conexion()))
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@id", id);
                    return connection.QueryFirstOrDefault<Usuario>("pp_obtener", parametros, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return null;
            }
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public string Post(Usuario usuario)
        {
            try
            {
                using (var connection = new SqlConnection(conexion.Conexion()))
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@id", usuario.Id);
                    parametros.Add("@nombre", usuario.Nombre);
                    parametros.Add("@cedula", usuario.Cedula);
                    parametros.Add("@telefono", usuario.Telefono);
                    parametros.Add("@direccion", usuario.Direccion);
                    parametros.Add("@email", usuario.Email);
                    connection.Execute("pp_registrar", parametros, commandType: CommandType.StoredProcedure);

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
