using EShop.Domain.Entities;

namespace EShop.Application.Interfaces.Repositories
{
    public interface ISesionRepository
    {
        Task<bool> RegistrarAsync(SesionEntity sesionEntity);
        Task<SesionEntity> BuscarPorIdUsuarioAsync(long idUsuario);
        Task<SesionEntity> BuscarPorJtiAsync(string jti);
        Task<bool> ExisteSesionActivaPorIdUsuarioAsync(long idUsuario);
    }
}
