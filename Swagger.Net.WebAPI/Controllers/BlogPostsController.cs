using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Swagger.Net.WebApi.Models;

namespace Swagger.Net.WebApi.Controllers
{
    public class BlogPostsController : ApiController
    {
        // GET api/blogposts
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// Gets a blog post
        /// </summary>
        /// <param name="id">Id of the blogpost</param>
        /// <param name="cheese">nom nom</param>
        /// <returns></returns>
        public string Get(int id, bool isABool, string cheese = "nom nom")
        {
            return "value";
        }

        // POST api/blogposts
        public BlogPostRequest Post([FromBody] BlogPostRequest request)
        {
            return request;
        }

        // PUT api/blogposts/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/blogposts/5
        public void Delete(int id)
        {
        }
    }
}
