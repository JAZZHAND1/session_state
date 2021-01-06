using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Session.Models
{
    public class ShoppingCart
    {
        public String session_id;
        public List<Product> product =  new List<Product>();
        public void setsession_id()
        {
            this.session_id = Guid.NewGuid().ToString();
        }
        public String getsessionid()
        {
            return session_id;
        }
        public void addproduct(Product product)
        {
            this.product.Add(product);
        }
        public void removeproduct(Product product)
        {
            this.product.Remove(product);
        }
        
    }

}