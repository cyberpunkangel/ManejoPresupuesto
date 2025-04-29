using System.Security.Claims;

namespace ManejoPresupuesto.Servicios
{

    public interface IServicioUsuarios
    {
        int ObtenerUsuarioId();
    }

    public class ServicioUsuarios: IServicioUsuarios
    {
        private readonly HttpContext httpContext;

        public ServicioUsuarios(IHttpContextAccessor httpContextAccessor)
        {
            httpContext = httpContextAccessor.HttpContext;
        }

        public int ObtenerUsuarioId()
        {
            //if (httpContext.User.Identity.IsAuthenticated)
            //{
            //    var idClaim = httpContext.User
            //            .Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
            //    var id = int.Parse(idClaim.Value);
            //    return id;
            //}
            // Validar si el HttpContext está disponible
            if (httpContext == null)
            {
                throw new ApplicationException("El contexto HTTP no está disponible.");
            }

            // Validar si el usuario está autenticado
            if (httpContext.User.Identity != null && httpContext.User.Identity.IsAuthenticated)
            {
                var idClaim = httpContext.User.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                // Validar si el claim del ID existe
                if (idClaim == null || string.IsNullOrWhiteSpace(idClaim.Value))
                {
                    throw new ApplicationException("No se pudo obtener el ID del usuario.");
                }

                return int.Parse(idClaim.Value);
            }
            else
            {
                throw new ApplicationException("El usuario no está autenticado");
            }
        }
    }
}
