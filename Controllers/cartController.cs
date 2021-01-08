using Session.Methods;
using Session.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace Session.Controllers
{
    public class cartController : ApiController
    {
        // GET: api/cart

        Shopping_Cart_Handler handler = new Shopping_Cart_Handler();
        JObject obj = new JObject();
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
            
        }

        // GET: api/cart/5
        [Route("cart/add/{item_name}")]
        [HttpPost]
        public HttpResponseMessage add_item_to_cart(String item_name)
        {
           
            try
            {
                List<string> list = new List<string>(Request.Headers.GetValues("session_id"));
                obj["session_id"] = list[0];
                HttpStatusCode code = handler.add_item_to_cart(obj, item_name);
                return Request.CreateResponse(code, handler.return_session_id());
            }
            catch(Exception E) {
                obj = null;
                HttpStatusCode code = handler.add_item_to_cart(obj, item_name);
                return Request.CreateResponse(code, handler.return_session_id());

            }
                             
        }

        // POST: api/cart
        [Route("cart")]
        [HttpGet]
        public HttpResponseMessage GetCard()
        {
            return Request.CreateResponse(HttpStatusCode.OK, handler.get_cart());
            
        }


        [Route("cart/decrease/{item_name}")]
        [HttpDelete]
        public HttpResponseMessage decrease_item_from_cart(String item_name)
        {
            try {
                List<string> list = new List<string>(Request.Headers.GetValues("session_id"));
                obj["session_id"] = list[0];
                HttpStatusCode code = handler.decrease_item_from_cart(obj, item_name);
                return Request.CreateResponse(code, handler.return_session_id());
            }
            catch(Exception e)
            {
                obj = null;
                HttpStatusCode code = handler.decrease_item_from_cart(obj, item_name);
                return Request.CreateResponse(code, handler.return_session_id());
            }
            

        }
        
        [Route("cart/remove/{item_name}")]
        [HttpDelete]
        public HttpResponseMessage remove_item_from_cart(String item_name)
        {
            try
            {
                List<string> list = new List<string>(Request.Headers.GetValues("session_id"));
                obj["session_id"] = list[0];
                HttpStatusCode code = handler.remove_item_from_cart(obj, item_name);
                return Request.CreateResponse(code, handler.return_session_id());
            }
            catch(Exception e)
            {
                obj = null;
                HttpStatusCode code = handler.remove_item_from_cart(obj, item_name);
                return Request.CreateResponse(code, handler.return_session_id());
            }
            
        }

    }
}
