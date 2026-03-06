using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApplicationApi.Core;
using WebApplicationApi.Data;
using WebApplicationApi.Data.Entities;
using WebApplicationApi.DTOs;
using WebApplicationApi.Services.Abstractions;

namespace WebApplicationApi.Services.Implementations
{
    public class RedService : IRedService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public RedService(DataContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<RedDTO>> CreateAsync(RedDTO dto)
        {
            try
            {
                Red red = _mapper.Map<Red>(dto);

                await _context.Red.AddAsync(red);
                await _context.SaveChangesAsync();

                RedDTO result = _mapper.Map<RedDTO>(red);

                return Response<RedDTO>.Success(result, "La red se creó con éxito");
            }
            catch (Exception ex)
            {
                return Response<RedDTO>.Failure(ex);
            }
        }

        public async Task<Response<object>> DeleteAsync(int id)
        {
            try
            {
                Red? red = await _context.Red.FirstOrDefaultAsync(s => s.Idr == id);

                if (red is null)
                {
                    return Response<object>.Failure($"No existe red con id: {id}");
                }

                _context.Red.Remove(red);
                await _context.SaveChangesAsync();

                return Response<object>.Success("Red eliminada con éxito");
            }
            catch (Exception ex)
            {
                return Response<object>.Failure(ex);
            }
        }

        public async Task<Response<RedDTO>> EditAsync(RedDTO dto)
        {
            try
            {
                var red = await _context.Red.FindAsync(dto.Id);

                if (red is null)
                {
                    return Response<RedDTO>.Failure($"No existe red con id: {dto.Id}");
                }

                // Mapear datos del DTO al objeto existente
                _mapper.Map(dto, red);

                await _context.SaveChangesAsync();

                RedDTO result = _mapper.Map<RedDTO>(red);

                return Response<RedDTO>.Success(result, "Red actualizada con éxito");
            }
            catch (Exception ex)
            {
                return Response<RedDTO>.Failure(ex);
            }
        }

        public async Task<Response<List<RedDTO>>> GetListAsync()
        {
            try
            {
                var count = await _context.Red.CountAsync();
                Console.WriteLine($"Total specialists in DB: {count}");

                List<Red> red = await _context.Red.ToListAsync();
                Console.WriteLine($"Specialists retrieved: {red.Count}");

                if (red.Any())
                {
                    var first = red.First();
                    Console.WriteLine($"Data of the red - ID: {first.Idr}, Name: {first.Nombre}, URL: {first.Url}, Country: {first.Pais}");
                }

                List<RedDTO> list = _mapper.Map<List<RedDTO>>(red);

                if (list.Any())
                {
                    var firstDto = list.First();
                    Console.WriteLine($"First DTO - ID: {firstDto.Id}, Name: {firstDto.Nombre}, URL: {firstDto.Url}, Country: {firstDto.Pais}");
                }

                return Response<List<RedDTO>>.Success(list);
            }
            catch (Exception ex)
            {
                return Response<List<RedDTO>>.Failure(ex.Message);
            }
        }

        public async Task<Response<RedDTO>> GetOneAsync(int id)
        {
            try
            {
                Red? red = await _context.Red.FindAsync(id);

                if (red is null)
                {
                    return Response<RedDTO>.Failure($"No existe red con id: {id}");
                }

                RedDTO dto = _mapper.Map<RedDTO>(red);

                return Response<RedDTO>.Success(dto, "Red obtenida con éxito");
            }
            catch (Exception ex)
            {
                return Response<RedDTO>.Failure(ex);
            }
        }
    }
}
