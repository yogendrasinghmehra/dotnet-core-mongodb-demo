using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace MongoDbApp
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoCRUD db= new MongoCRUD("AddressBook");
            PersonModal modal =new PersonModal{
             FirstName="Yog",
             LastName ="Mehra",
             Address=new AddressModel{
                 StreetAddress="104",
                 City="Ranikhet",
                 State="Uttarakhand",
                 ZipCode="263645"  
             }

            };

           // db.InsertRecord("users", modal);
            Console.WriteLine("record added successfully");
        }
    }

    public class PersonModal
    {
        [BsonId]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }  
        public AddressModel Address { get; set; }   
    }

public class AddressModel
{
    public string StreetAddress { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
}
    public class MongoCRUD
    {
        private IMongoDatabase db;
        public MongoCRUD(string database)
        {
            var client= new MongoClient();
            db=client.GetDatabase(database);

        }

        public void InsertRecord<T>(string table, T record)
        {
            var collection= db.GetCollection<T>(table);
            collection.InsertOne(record);

        }

        public List<T> LoadRecords<T>(string table)
        {
            var collection = db.GetCollection<T>(table);
            return collection.Find(new BsonDocument()).ToList();
            
        }



    }
}
