using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Session.Models
{
    public class Product
    {
        public String item_name;
        [DefaultValue(true)]
        public int amount;

        public String getitem_name()
        {
            return this.item_name;
        }
        public void setitem_name(String item_name)
        {
            this.item_name = item_name;
        }

        public int getamount()
        {
            return this.amount;
        }
        public void setamount()
        {
            this.amount = this.amount + 1;
        }
        public void remove_item()
        {
            this.amount = this.amount - 1;
        }


    }
}