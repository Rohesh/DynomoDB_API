using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynomoDB.Libs.DynomoDB
{
   public class PutItem : IPutItem 
    {
        private readonly IAmazonDynamoDB _dynamoDbClient;
        public PutItem (IAmazonDynamoDB dynamoDbClient)
        {
            _dynamoDbClient = dynamoDbClient;
        }
        public async Task AddNewEntry(int OrderID, string replyDateTime)
        {
            var request = RequestBuilder(OrderID, replyDateTime);
            await PutItemAsync(request);
        }

        private PutItemRequest RequestBuilder(int OrderID, string replyDateTime)
        {
            var item = new Dictionary<string, AttributeValue>

            {
                { "OrderID", new AttributeValue {N= OrderID.ToString () } },
                { "ReplyDateTime", new AttributeValue {N= replyDateTime } }
        };

            return new PutItemRequest
            {
                TableName = "TempDynamoDbTable",
                Item = item
            };

            }

        private async Task PutItemAsync(PutItemRequest request )
        {
            await _dynamoDbClient.PutItemAsync(request);
        }
    }
}
