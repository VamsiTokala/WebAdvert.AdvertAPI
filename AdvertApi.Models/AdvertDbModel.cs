using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewAdvertApi.Models
{
    [DynamoDBTable("Adverts")] //name of the table in Dynamo DB

    //If you don't want to save any of these attributes, just of this property, just don't put any attribute.
    public class AdvertDbModel
    {
        [DynamoDBHashKey]  //primary key is Id
        public string Id { get; set; }

        [DynamoDBProperty]
        public string Title { get; set; }

        [DynamoDBProperty]
        public string Description { get; set; }

        [DynamoDBProperty]

        public double Price { get; set; }

        [DynamoDBProperty]
        public DateTime CreationDateTime { get; set; }

        [DynamoDBProperty]
        public AdvertStatus Status { get; set; }
    }
}
