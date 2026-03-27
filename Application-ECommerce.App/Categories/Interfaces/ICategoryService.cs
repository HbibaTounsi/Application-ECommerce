using Application_ECommerce.App.Categories.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_ECommerce.App.Categories.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryDto> AddAsync(CreateCategoryDto createCategoryDto);
        Task<CategoryDto> ReadByIdAsync(Guid categoryId);
        Task<Guid?> GetCategoryIdByNameAsync(string categoryName);
        Task<IEnumerable<CategoryDto>> ReadAllAsync();
        Task UpdateAsync(UpdateCategoryDto updateCategoryDto);
        Task DeleteAsync(Guid id);
    }
}
