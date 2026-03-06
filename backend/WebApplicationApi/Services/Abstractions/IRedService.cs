using WebApplicationApi.Core;
using WebApplicationApi.DTOs;

namespace WebApplicationApi.Services.Abstractions
{
    public interface IRedService
    {
        // Crate Red
        Task<Response<RedDTO>> CreateAsync(RedDTO dto);

        // Edit red
        Task<Response<RedDTO>> EditAsync(RedDTO dto);

        // Delete red
        Task<Response<object>> DeleteAsync(int id);

        // Get one red by id
        Task<Response<RedDTO>> GetOneAsync(int id);

        // Get all red
        Task<Response<List<RedDTO>>> GetListAsync();
    }
}
