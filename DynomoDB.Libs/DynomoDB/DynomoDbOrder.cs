using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DynomoDB.Libs.DynomoDB
{
    public class DynomoDbOrder : IDynomoDbOrder 
    {
        private readonly IAmazonDynamoDB _dynamoDbClient;
        private static readonly string tablename = "TempDynamoDbTable";
        public DynomoDbOrder (IAmazonDynamoDB dynamoDbClient)
        {
            _dynamoDbClient = dynamoDbClient;
        }
        public void CreateDynomoDbTable()
        {
            try
            {
                CreateTempTable();
            }
            catch (Exception e)

            {
                Console.WriteLine(e);
                throw;
            }
            
        }
        public void CreateTempTable()
        {
            Console.WriteLine("Creating table");
            var request = new CreateTableRequest
            {
                AttributeDefinitions = new List<AttributeDefinition>
{
    new AttributeDefinition
    {
        AttributeName ="OrderID",
       AttributeType ="N"
    },
     new AttributeDefinition
    {
        AttributeName ="ReplyDateTime",
       AttributeType ="N"
    }
},
                KeySchema = new List<KeySchemaElement>
{
    new  KeySchemaElement
    {
        AttributeName ="OrderID" ,
        KeyType ="HASH"
    },
    new  KeySchemaElement
    {
        AttributeName ="ReplyDateTime" ,
        KeyType ="Range"
    }
},
                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 5,
                    WriteCapacityUnits = 5
                },
                TableName = tablename
            };

            var response = _dynamoDbClient.CreateTableAsync(request);
            WaitUntilTableReady(tablename);
        }
        public void WaitUntilTableReady(string tablename)
        {
            string status = null;
            do
            {
                Thread.Sleep(5000);
                try
                {
                    var res = _dynamoDbClient.DescribeTableAsync(new DescribeTableRequest
                    {
                        TableName = tablename
                    });
                    status = res.Result.Table.TableStatus;
                }
                catch (ResourceNotFoundException)
                {

                }
            } while (status != "ACTIVE");
            {
                Console.WriteLine("Table Created Successfully");
            }
        }

    }
}
