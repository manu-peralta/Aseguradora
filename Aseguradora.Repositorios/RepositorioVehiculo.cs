using Aseguradora.Aplicacion.Entidades;
using Aseguradora.Aplicacion.Interfaces;
namespace Aseguradora.Repositorios;
public class RepositorioVehiculo : IRepositorioVehiculo
{

    public void AgregarVehiculo(Vehiculo vehiculo)
    {
        using (var context = new AseguradoraContext())
        {
            bool existe = context.Vehiculos.FirstOrDefault(a => a.Dominio == vehiculo.Dominio) != null;
            if (!existe)
            {
                context.Add(vehiculo);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Ya existe un vehiculo con ese Dominio");
            }
        }
    }
    public Vehiculo? ObtenerVehiculo(int id)
    {
        using (var context = new AseguradoraContext())
        {
            return context.Vehiculos?.FirstOrDefault(p => p.Id == id) ?? null;
        }
    }

    public void ModificarVehiculo(Vehiculo vehiculo)
    {
        using (var context = new AseguradoraContext())
        {
            var vehiculoViejo = context.Vehiculos.FirstOrDefault(v => v.Dominio == vehiculo.Dominio);
            var titular= new RepositorioTitular().ObtenerTitular(vehiculo.TitularId) ?? throw new Exception("No existe un titular con ese id");
            if (vehiculoViejo != null)
            {
                vehiculoViejo.Anio = vehiculo.Anio;
                vehiculoViejo.TitularId = vehiculo.TitularId;
                vehiculoViejo.Marca = vehiculo.Marca;
                context.SaveChanges();
            }
        }
    }

    public void EliminarVehiculo(int id)
    {
        using (var context = new AseguradoraContext())
        {
            var vehiculo = context.Vehiculos.FirstOrDefault(v => v.Id == id);
            if (vehiculo != null)
            {
                context.Remove(vehiculo);
                context.SaveChanges();
            }
        }
    }

    public List<Vehiculo> ListarVehiculos()
    {
        using (var context = new AseguradoraContext())
        {
            return context.Vehiculos.ToList();
        }
    }
    //public void ListaVehiculosDeTitular
}