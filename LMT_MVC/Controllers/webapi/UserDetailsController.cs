using LMT_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LMT_MVC.Controllers.webapi
{
    public class UserDetailsController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<object> Get()
        {
            DCDataContext DbLMS = new DCDataContext();
            var UserDetails = DbLMS.Innovation_Management_Users.Select(x => new {x.user_firstname,x.user_lastname,x.user_picture,x.user_email,x.user_isadmin,x.user_isdeleted,x.pk_user_id });
            return UserDetails;
        }//user_firstname user_lastname user_picture user_email user_isadmin user_isdeleted pk_user_id

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}