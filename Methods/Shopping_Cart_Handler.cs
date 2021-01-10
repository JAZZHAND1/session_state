using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Session.Models;

namespace Session.Methods
{
    public class Shopping_Cart_Handler
    {

        JObject Jsonobject = new JObject();
        String statuscode="";
        String message = "";
        String session_id = "";
        List<ShoppingCart> carts = new List<ShoppingCart>();
        public List<ShoppingCart> Load_Cart_Data()
        {
            var dataString = File.ReadAllText(@"D:\Books\3-2\Software Design and Architechture\Microservice\Session\Data\ShoppingCart.json");
            if (dataString != null)
            {
                return JsonConvert.DeserializeObject<List<ShoppingCart>>(dataString);
            }
            else
                return null;
        }
       
        public void Save_Cart_Data(List<ShoppingCart> data)
        {
            var dataString = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(@"D:\Books\3-2\Software Design and Architechture\Microservice\Session\Data\ShoppingCart.json", dataString);
        }
        public JObject return_message()
        {
           Jsonobject[statuscode]=message;
           return Jsonobject;
        }
        public String return_session_id()
        {
            return session_id;
        }
        public List<Product> get_cart(string session_id)
        {
            carts = Load_Cart_Data();
            var result = carts.Find(p => p.session_id == session_id);
            return result.product;
        }

        public HttpStatusCode add_item_to_cart(JObject obj,String item_name)
        {

            if (obj==null ||obj.Value<String>("session_id") == "" || obj["session_id"] == null)
            {
                var data = Load_Cart_Data();
                Product product = new Product();
                product.setitem_name(item_name);
                product.setamount();
                ShoppingCart cart = new ShoppingCart();
                cart.setsession_id();
                cart.product.Add(product);
                data.Add(cart);
                Save_Cart_Data(data);
                session_id = cart.getsessionid();
                statuscode = "Successful";
                message = "Item added to cart";
                return HttpStatusCode.OK;
            } 
            else
            {
                var data = Load_Cart_Data();
                var checksessionid = data.Find(p => p.session_id == obj.Value<String>("session_id"));
                if (checksessionid == null)
                {
                    message = "Incorrect session id.Please try again";
                    statuscode = "Error";
                    return HttpStatusCode.NotFound;
                }
                else
                {
                    int flag = 0;
                    foreach (Product p in checksessionid.product)
                    {
                        if (p.item_name == item_name)
                        {
                            p.setamount();
                            Save_Cart_Data(data);
                            flag = 1;
                            break;
                        }
                    }
                    if (flag == 1)
                    {
                        statuscode = "Successful";
                        message = "No of items increased in cart";
                        return HttpStatusCode.OK;
                    }
                    else
                    {
                        Product product = new Product();
                        product.setitem_name(item_name);
                        product.setamount();
                        checksessionid.product.Add(product);
                        Save_Cart_Data(data);
                        statuscode = "Successful";
                        message = "Item added to cart";
                        return HttpStatusCode.OK;
                    }
                    
                   
                }

            }
         }

        public HttpStatusCode decrease_item_from_cart(JObject obj, String item_name)
        {
          
            if (obj == null)
            {
                message = "No session id was found in the header";
                statuscode = "Error";
                return HttpStatusCode.NotFound;
            }
            var data = Load_Cart_Data();
            var checksessionid = data.Find(p => p.session_id == obj.Value<String>("session_id"));
            if (checksessionid == null)
            {
                statuscode = "Error";
                message = "Session id does not match.Please try again";
                return HttpStatusCode.NotFound;
            }
            else
            {
                int flag = 0;
                foreach (Product p in checksessionid.product)
                {
                    if (p.item_name == item_name)
                    {
                        p.remove_item();
                        Save_Cart_Data(data);
                        flag = 1;
                        break;
                    }
                }
                if (flag == 1)
                {
                    statuscode = "Decreased";
                    message = "Item decremented from cart";
                    remove_product_when_zero(obj.Value<String>("session_id"));
                    return HttpStatusCode.OK;
                }
                else
                {
                    statuscode = "Error";
                    message = "Item not found";
                    return HttpStatusCode.OK;
                }
            }
        }

        public void remove_product_when_zero(String session_id)
        {
       
            var data = Load_Cart_Data();
            var checksessionid = data.Find(p => p.session_id == session_id);
            foreach (Product p in checksessionid.product)
            {
                if (p.amount == 0)
                {
                    checksessionid.removeproduct(p);
                    statuscode = "Decreased";
                    message = "Item decremented from cart";
                    Save_Cart_Data(data);
                  
                 break;
                }
            }
   
        }

        public HttpStatusCode remove_item_from_cart(JObject obj, String item_name)
        {
            if (obj == null || obj.Value<String>("session_id") == "" || obj["session_id"] == null)
            {
                statuscode = "Error";
                message = "No valid session id was found";
                return HttpStatusCode.BadRequest;
            }
            else
            {
                var data = Load_Cart_Data();
                var checksessionid = data.Find(p => p.session_id == obj.Value<String>("session_id"));
                var findproduct_to_remove = checksessionid.product.Find(p => p.item_name == item_name);
                if (findproduct_to_remove == null)
                {
                    statuscode = "Error";
                    message = "Item not found";
                    return HttpStatusCode.NotFound;
                }
                else
                {
                    checksessionid.product.Remove(findproduct_to_remove);
                    Save_Cart_Data(data);
                    statuscode = "Success";
                    message = "Item removed";
                    return HttpStatusCode.OK;
                }
             

            }
           
        }

    }
}