using EShop.Domain.Entities;

namespace EShop.Application.Interfaces.Repositories
{
    public interface ISegundoFARepository
    {
        Task<bool> RegistrarAsync(SegundoFAEntity segundoFAEntity);
        Task<bool> ActualizarAsync(SegundoFAEntity segundoFAEntity);
        Task<SegundoFAEntity?> BuscarPorIdUsuario(long idUsuario);
    }
}
