namespace TestApiUsuario
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestGet()
        {
            var api = new ApiUsuario.Controllers.UsuarioController();
            string id = "789";
            var resultado = api.Get(id);
            if(resultado == null)
                Assert.AreEqual("No hay registros para el id: ", id);
        }

        [TestMethod]
        public void TestPost()
        {
            var api = new ApiUsuario.Controllers.UsuarioController();
            var model = new Models.Usuario();
            model.Id = "123";
            model.Nombre = "Lina";
            model.Cedula = 1025549402;
            model.Telefono = "6017179025";
            model.Direccion = "Carrera 24 # 38 - 45";
            model.Email = "";
            string resultado = api.Post(model);
            Assert.AreEqual("Inserción correcta", resultado);
        }
    }
}