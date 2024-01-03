using Attract.Common.BaseResponse;
using Attract.Common.DTOs.Collection;
using Attract.Framework.UoW;
using Attract.Service.IService;
using AttractDomain.Entities.Attract;
using Microsoft.AspNetCore.Http;
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

        public CollectionService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
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

        public Task<BaseCommandResponse> GetCollection()
        {
            throw new NotImplementedException();
        }


        #region private methods
        private string GetProductDirectoryPath()
        {
            return Path.Combine("wwwroot", "Images", "Collection");
        }
        private async Task SaveImageAsync(string directoryPath, IFormFile image)
        {
            
            var imagePath = Path.Combine(directoryPath, image.FileName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
        }
        #endregion
    }
}
