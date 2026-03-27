using Application_ECommerce.App.Categories.Interfaces;
using Application_ECommerce.App.Products.Dtos;
using Application_ECommerce.App.Products.Interfaces;
using Application_ECommerce.Core.Entities.Product;
using Application_ECommerce.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Application_ECommerce.Core.Interfaces.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_ECommerce.App.Products.Services
{
    public class ProductServices : IProductServices
    {
          private readonly ICategoryService _categoryService;
        private readonly IProductRepository _productRepository;
        private readonly IFileHelper _fileHelper;
        private readonly IMapper _mapper;
        private const string PRODUCT_IMAGES_FOLDER = "images/products";

        public ProductServices(IProductRepository productRepository, ICategoryService categoryService, IFileHelper fileHelper, IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryService = categoryService;
            _fileHelper = fileHelper;
            _mapper = mapper;
        }

        public async Task<ProductDto?> AddAsync(CreateProductDto createProductDto)
        {
            //traitement de l'image
            if (createProductDto.ImageFile != null)
            {
                string imagepach = _fileHelper.UploadFile(createProductDto.ImageFile, PRODUCT_IMAGES_FOLDER);
           
               if (!string.IsNullOrEmpty(imagepach))
               {
                createProductDto.ImageUrl = imagepach;
               }
            }

            var product = _mapper.Map<Product>(createProductDto);
             var addedproduct = await _productRepository.AddAsync(product);
            return _mapper.Map<ProductDto>(addedproduct);
            
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                // Récupérer le produit pour obtenir l'URL de l'image à supprimer
                var product = await _productRepository.ReadByIdAsync(id);
                if (product != null && !string.IsNullOrEmpty(product.ImageUrl))
                {
                    // Supprimer l'image associée au produit
                    _fileHelper.DeleteFile(product.ImageUrl, PRODUCT_IMAGES_FOLDER);
                }

                // Supprimer le produit
                await _productRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erreur lors de la suppression du produit : {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<ProductDto>> ReadAllAsync()
        {
            try
            {
                var products = await _productRepository.ReadAllAsync();
                return _mapper.Map<IEnumerable<ProductDto>>(products);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erreur lors de la récupération des produits : {ex.Message}", ex);
            }
        }

        public async Task<ProductDto> ReadByIdAsync(Guid productId)
        {
            var product = await _productRepository.ReadByIdAsync(productId);
            if (product == null)
            {
                throw new KeyNotFoundException("product Not Found");
            }
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>>? ReadProductsByCategoryName(string categoryName)
        {
            var categoryId = await _categoryService.GetCategoryIdByNameAsync(categoryName);

            if (categoryId == null)
            {
                throw new KeyNotFoundException("Catégorie non trouvée");
            }

            var productsList = await _productRepository.GetProductsByCategoryId(categoryId.Value);

            return _mapper.Map<IEnumerable<ProductDto>>(productsList);
        }

        public async Task UpdateAsync(UpdateProductDto updateProductDto)
        {
            try
            {
                // Vérifier si le produit existe
                var existingProduct = await _productRepository.ReadByIdAsync(updateProductDto.Id);
                if (existingProduct == null)
                {
                    throw new KeyNotFoundException("Produit non trouvé");
                }

                // Conserver l'ancienne URL d'image pour la supprimer si nécessaire
                string oldImageUrl = existingProduct.ImageUrl;

                // Mettre à jour les propriétés
                existingProduct.Name = updateProductDto.Name;
                existingProduct.Price = updateProductDto.Price;
                existingProduct.Description = updateProductDto.Description;
                existingProduct.CategoryId = updateProductDto.CategoryId;

                // Si une nouvelle image est téléchargée
                if (updateProductDto.ImageFile != null)
                {
                    // Téléchargement de la nouvelle image
                    string newFileName = _fileHelper.UploadFile(updateProductDto.ImageFile, PRODUCT_IMAGES_FOLDER);
                    if (!string.IsNullOrEmpty(newFileName))
                    {
                        // Supprimer l'ancienne image si elle existe
                        if (!string.IsNullOrEmpty(oldImageUrl))
                        {
                            _fileHelper.DeleteFile(oldImageUrl, PRODUCT_IMAGES_FOLDER);
                        }

                        existingProduct.ImageUrl = newFileName;
                    }
                }

                await _productRepository.UpdateAsync(existingProduct);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erreur lors de la mise à jour du produit : {ex.Message}", ex);
            }
        }
       

    }
}
