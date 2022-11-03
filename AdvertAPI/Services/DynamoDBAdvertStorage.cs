using AdvertApi.Models;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using AutoMapper;

namespace AdvertAPI.Services
{

    public class DynamoDBAdvertStorage : IAdvertStorageService
    {
        private readonly IMapper _mapper;



        public DynamoDBAdvertStorage(IMapper mapper)
        {
            _mapper = mapper;
        }

        async Task<string> IAdvertStorageService.Add(AdvertModel model)
        {
            var dbModel = _mapper.Map<AdvertDbModel>(model);

            //we just save our model, because its mapped, it dones't havee to pass all tje properties
            dbModel.Id = new Guid().ToString();
            dbModel.CreationDateTime = DateTime.UtcNow;
            dbModel.Status = AdvertStatus.Pending;//pending is 1

            //because we have a credentialed profile, we don't need to pass any credentials
            using (var client = new AmazonDynamoDBClient())
            {
                using (var context = new DynamoDBContext(client))
                {
                    await context.SaveAsync(dbModel);
                }
                return dbModel.Id;//it everything goes fine then return id
                //if things go wrong, then we will have to return a code is in case we return an exception or something, then be captured it in the API and then we return say http500
                //
            }
        }

       public async Task<bool> IAdvertStorageService.Confirm(ConfirmAdvertModel model)
        {
            using (var client = new AmazonDynamoDBClient())
            {
                using (var context = new DynamoDBContext(client))
                {
                    //we need to load the records from teh db first
                    var record = await context.LoadAsync<AdvertDbModel>(model.Id);
                    if (record == null)
                    {
                        throw new KeyNotFoundException($"A rcord with id={model.Id} isnot found.");
                    }
                    // if the status is active, which means everything went OK and we managed to upload the pictures to Amazon S3
                    if (model.Status == AdvertStatus.Active)
                    {
                       record.Status = AdvertStatus.Active;//set the status to Active in DB as well
                        await context.SaveAsync(record); //save it back to the db
                    }
                    else
                    {
                        //if things go wrong and cannot set it to active, send the pending back and delete the record from db
                        await context.DeleteAsync(record);  
                    }
                }

            }
        }
    }
}
