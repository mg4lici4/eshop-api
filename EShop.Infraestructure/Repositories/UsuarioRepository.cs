using EShop.Application.Helpers;
using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using EShop.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace EShop.Infraestructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly EShopDbContext _eShopDbContext;
        public UsuarioRepository(EShopDbContext eShopDbContext)
        {
            _eShopDbContext = eShopDbContext;
        }

        public async Task<bool> ActualizarAsync(UsuarioEntity usuarioEntity)
        {
            usuarioEntity.FechaActualizacion = FechaHelper.ActualUTC();

            _eShopDbContext.Usuarios.Entry(usuarioEntity).State = EntityState.Modified;
            var result = await _eShopDbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<UsuarioEntity> BuscarPorIdPersonaAsync(long idPersona)
        {
            var usuarioEntity = await _eShopDbContext.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.IdPersona == idPersona);
            return usuarioEntity!;
        }

        public async Task<UsuarioEntity> BuscarPorNombreAsync(string nombre)
        {
            var usuarioEntity = await _eShopDbContext.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.Nombre == nombre);
            return usuarioEntity!;
        }

        public async Task<bool> RegistrarAsync(UsuarioEntity usuarioEntity)
        {
            try
            {
                await _eShopDbContext.Usuarios.AddAsync(usuarioEntity);
                var result = await _eShopDbContext.SaveChangesAsync();
                return result > 0;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("ORA-00001"))
                    throw new DbUpdateException("Ya existe un registro con el nombre proporcionado.", ex);

                throw new DbUpdateException("Ocurrio un error en la base de datos.", ex);
            }
        }
    }
}
