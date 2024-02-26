using Attract.Common.BaseResponse;
using Attract.Common.DTOs.Collection;
using Attract.Common.DTOs.Country;
using Attract.Common.Helpers;
using Attract.Framework.UoW;
using Attract.Service.IService;
using AttractDomain.Entities.Attract;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attract.Service.Service
{
    public class CollectionService : ICollectionService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor httpContextAccessor;

        public CollectionService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<BaseCommandResponse> AddCollection(AddCollectionDTO addCollectionDTO)
        {
            var response = new BaseCommandResponse();
            var productDirectoryPath = GetProductDirectoryPath();
            if (!Directory.Exists(productDirectoryPath))
            {
                Directory.CreateDirectory(productDirectoryPath);
            }
            await SaveImageAsync(productDirectoryPath, addCollectionDTO.Image1);
            await SaveImageAsync(productDirectoryPath, addCollectionDTO.Image2);
            await SaveImageAsync(productDirectoryPath, addCollectionDTO.Image3);
            var newCollection = new Collection
            {
                ProductQuantityId = addCollectionDTO.ProductQuantityId,
                Image1 = addCollectionDTO.Image1.FileName,
                Image2 = addCollectionDTO.Image2.FileName,
                Image3 = addCollectionDTO.Image3.FileName
            };
            await unitOfWork.GetRepository<Collection>().InsertAsync(newCollection);
            await unitOfWork.SaveChangesAsync();
            response.Success = true;
            response.Data = newCollection.Id;
            return response;
        }

        public async Task<BaseCommandResponse> GetAllCollections(PagingParams pagingParams)
        {
            var response = new BaseCommandResponse();
            var collections =  unitOfWork.GetRepository<Collection>().GetAll(include: s => s.Include(a => a.ProductQuantity)
            .ThenInclude(a => a.Product));
            if (collections == null)
            {
                response.Success = true;
                response.Message = "No Collections Found";
                return response;
            }
            var hostValue = httpContextAccessor.HttpContext.Request.Host.Value;

            var transformedCollections = collections.Select(collection => new Collection
            {
                Id = collection.Id,
                ProductQuantityId = collection.ProductQuantityId,
                ProductQuantity = new ProductQuantity
                {
                    ImageName = $"http://{hostValue}/Images/Product/{collection.ProductQuantity.ImageName}",
                    AvailableSizeId = collection.ProductQuantity.AvailableSizeId,
                    ColorId = collection.ProductQuantity.ColorId,
                    AvailableSize = collection.ProductQuantity.AvailableSize,
                    Id = collection.ProductQuantity.Id,
                    IsActive = collection.ProductQuantity.IsActive,
                    Price= collection.ProductQuantity.Price,
                    ProductId = collection.ProductQuantity.ProductId,
                    Quantity = collection.ProductQuantity.Quantity,
                    Color=collection.ProductQuantity.Color,
                    CartProducts=collection.ProductQuantity.CartProducts,
                    Product = collection.ProductQuantity.Product,
                },
                Image1 = GetFullImagePath(hostValue, collection.Image1),
                Image2 = GetFullImagePath(hostValue, collection.Image2),
                Image3 = GetFullImagePath(hostValue, collection.Image3)
            });

            var PagedCenter = await PagedList<Collection>.CreateAsync(transformedCollections, pagingParams.PageNumber, pagingParams.PageSize);
            response.Data = new Pagination<Collection>(PagedCenter.CurrentPage, PagedCenter.PageSize, PagedCenter.TotalCount, PagedCenter);
            response.Success = true;
//            response.Data = collections;
            return response;
        }

        public async Task<BaseCommandResponse> UpdateCollection(UpdateCollectionDTO updatedCollectionDTO)
        {
            var response = new BaseCommandResponse();
            var UpdatedCollection = await unitOfWork.GetRepository<Collection>()
                                    .GetFirstOrDefaultAsync(predicate: x => x.Id == updatedCollectionDTO.id);
            if (UpdatedCollection == null)
            {
                response.Success = false;
                response.Message = "Contact not found.";
                return response;
            }
            if (updatedCollectionDTO.Image1 != null)
            {
                var productDirectoryPath = GetProductDirectoryPath();
                if (!Directory.Exists(productDirectoryPath))
                {
                    Directory.CreateDirectory(productDirectoryPath);
                }
                await DeleteImageAsync(UpdatedCollection.Image1, productDirectoryPath);
                await SaveImageAsync(productDirectoryPath, updatedCollectionDTO.Image1);
                UpdatedCollection.Image1 = updatedCollectionDTO.Image1.FileName;
            }
            if (updatedCollectionDTO.Image2 != null)
            {
                var productDirectoryPath = GetProductDirectoryPath();
                if (!Directory.Exists(productDirectoryPath))
                {
                    Directory.CreateDirectory(productDirectoryPath);
                }
                await DeleteImageAsync(UpdatedCollection.Image2, productDirectoryPath);
                await SaveImageAsync(productDirectoryPath, updatedCollectionDTO.Image2);
                UpdatedCollection.Image2 = updatedCollectionDTO.Image2.FileName;
            }
            if (updatedCollectionDTO.Image3 != null)
            {
                var productDirectoryPath = GetProductDirectoryPath();
                if (!Directory.Exists(productDirectoryPath))
                {
                    Directory.CreateDirectory(productDirectoryPath);
                }
                await DeleteImageAsync(UpdatedCollection.Image3, productDirectoryPath);
                await SaveImageAsync(productDirectoryPath, updatedCollectionDTO.Image3);
                UpdatedCollection.Image3 = updatedCollectionDTO.Image3.FileName;
            }
            UpdatedCollection.ProductQuantityId = updatedCollectionDTO.ProductQuantityId;
            UpdatedCollection.ModifyOn = DateTime.Now;
            unitOfWork.GetRepository<Collection>().UpdateAsync(UpdatedCollection);
            await unitOfWork.SaveChangesAsync();
            response.Success = true;
            response.Data = UpdatedCollection.Id;
            return response;
        }
        public async Task<BaseCommandResponse> DeleteCollection(int id)
        {
            var response = new BaseCommandResponse();
            var Collection = await unitOfWork.GetRepository<Collection>()
                .GetFirstOrDefaultAsync(predicate: x => x.Id == id);
            if (Collection == null)
            {
                response.Success = false;
                response.Message = "Contact not found.";
                return response;
            }
            var directorypath = GetProductDirectoryPath();
            await DeleteImageAsync(Collection.Image1, directorypath);
            await DeleteImageAsync(Collection.Image2, directorypath);
            await DeleteImageAsync(Collection.Image3, directorypath);
            unitOfWork.GetRepository<Collection>().Delete(id);
            await unitOfWork.SaveChangesAsync();
            response.Success = true;
            return response;
        }
        #region private methods
        private string GetProductDirectoryPath()
        {
            return Path.Combine("wwwroot", "Images", "Collection");
        }
        private async Task DeleteImageAsync(string ImageName, string directoryPath)
        {
            var LastUnderScore = ImageName.LastIndexOf('_');
            var RealImageName = ImageName.Substring(LastUnderScore + 1);
            var ImageToBeDeleted = Path.Combine(directoryPath, RealImageName);
            File.Delete(ImageToBeDeleted);
        }
        private async Task SaveImageAsync(string directoryPath, IFormFile image)
        {

            var imagePath = Path.Combine(directoryPath, image.FileName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
        }
        private static string GetFullImagePath(string hostValue, string imageName)
        {

            return $"http://{hostValue}/Images/Collection/{imageName}";
        }


        #endregion
    }
}
