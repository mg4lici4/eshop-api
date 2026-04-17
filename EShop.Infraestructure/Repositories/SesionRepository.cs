using EShop.Application.Helpers;
using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using EShop.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infraestructure.Repositories
{
    public class SesionRepository : ISesionRepository
    {
        private readonly EShopDbContext _eShopDbContext;
        public SesionRepository(EShopDbContext eShopDbContext)
        {
            _eShopDbContext = eShopDbContext;
        }

        public async Task<SesionEntity> BuscarPorIdUsuarioAsync(long idUsuario)
        {
            var sesionEntity = await _eShopDbContext.Sesiones.AsNoTracking().FirstOrDefaultAsync(s => s.IdUsuario == idUsuario);
            return sesionEntity!;
        }

        public async Task<bool> ExisteSesionActivaPorIdUsuarioAsync(long idUsuario)
        {
            return await _eShopDbContext.Sesiones
                    .AsNoTracking()
                    .CountAsync(s => s.IdUsuario == idUsuario && s.Estado == 1) > 0;
        }

        public async Task<SesionEntity> BuscarPorJtiAsync(string jti)
        {
            var sesionEntity = await _eShopDbContext.Sesiones.AsNoTracking().FirstOrDefaultAsync(s => s.Jti == jti);
            return sesionEntity!;
        }

        public async Task<bool> ActualizarAsync(SesionEntity sesionEntity)
        {
            sesionEntity.FechaActualizacion = FechaHelper.ActualUTC();

            _eShopDbContext.Sesiones.Entry(sesionEntity).State = EntityState.Modified;
            var result = await _eShopDbContext.SaveChangesAsync();
            return result > 0;
        }
        public async Task<bool> RegistrarAsync(SesionEntity sesionEntity)
        {
            await _eShopDbContext.Sesiones.AddAsync(sesionEntity);
            var resultado = await _eShopDbContext.SaveChangesAsync();
            return resultado > 0;
        }
    }
}
