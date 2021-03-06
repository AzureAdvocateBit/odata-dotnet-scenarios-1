﻿using System.Linq;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using MongoApi.Data;
using MongoDB.Driver;

namespace MongoApi.Controllers
{
    [ODataRoutePrefix("Products")]
    [ApiController]
    public class ProductsController : ODataController
    {
        private readonly IMongoClient mongoClient;
        private readonly IMongoCollection<Product> collection;
        public ProductsController(IMongoClient mongoClient)
        {
            this.mongoClient = mongoClient;
            var db = mongoClient.GetDatabase("products");

            this.collection = db.GetCollection<Product>("catalog");
        }

        [HttpGet("")]
        [ODataRoute]
        [EnableQuery]
        public ActionResult Get()
        {
            var records = this.collection.AsQueryable();
            return Ok(records);
        }
    }
}
