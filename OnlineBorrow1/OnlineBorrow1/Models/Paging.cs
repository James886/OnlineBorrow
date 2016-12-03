using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using OnlineBorrow1.Models;




namespace OnlineBorrow1.Models
{
    public class Paging
    {
        public int sum_pages;
        public int itemsCount_in_apage;
        public int sum_items;
        public int curr_page_index;
        public int start_item_index;
        public int end_item_index;
        public int start_page_index;
        public int end_page_index;
        public int link_num_in_apage;
        public Object data = null;
        public Object curr_page_data = null;

        public Paging(Object obj, int itemsCount_in_apage, int link_num_in_apage,int curr_page_index )
        {
            data = obj;
            this.itemsCount_in_apage = itemsCount_in_apage;
            this.curr_page_index = curr_page_index;
            this.link_num_in_apage = link_num_in_apage;

            GetData();
        }

        public void GetRelatedValues(int data_item_count)
        {
            sum_items = data_item_count;
            sum_pages = (int)Math.Ceiling(sum_items / (itemsCount_in_apage * 1.0));
            if (sum_pages == 0)
            {
                sum_pages = 1;
                start_item_index = 0;
                end_item_index = 0;
                start_page_index = 1;
                end_page_index = 1;
            }
            else
            {

                if (curr_page_index <= 0)
                    curr_page_index = 1;
                if (curr_page_index >= sum_pages)
                    curr_page_index = sum_pages;

                start_item_index = (curr_page_index - 1) * itemsCount_in_apage;
                end_item_index = Math.Min(curr_page_index * itemsCount_in_apage, sum_items) - 1;

                start_page_index = ((curr_page_index - 1) / link_num_in_apage) * link_num_in_apage + 1;
                end_page_index = Math.Min(start_page_index + link_num_in_apage - 1, sum_pages);
            }
        }

        public void GetData()
        {
            string objTypeStr = data.GetType().ToString();
            switch (objTypeStr)
            {
                case "System.Data.Entity.Infrastructure.DbQuery`1[OnlineBorrow1.Models.borrowInformation]":
                    IEnumerable<borrowInformation> borrowItems = (IEnumerable<borrowInformation>)data;
                    GetRelatedValues(borrowItems.Count());
                    curr_page_data = borrowItems.ToList().GetRange(start_item_index, end_item_index - start_item_index);
                        break;
                case "System.Data.Entity.Infrastructure.DbQuery`1[OnlineBorrow1.Models.User]":
                    IEnumerable<User> userItems = (IEnumerable<User>)data;
                    GetRelatedValues(userItems.Count());
                    curr_page_data = userItems.ToList().GetRange(start_item_index, end_item_index - start_item_index);
                        break;
                case "System.Data.Entity.Infrastructure.DbQuery`1[OnlineBorrow1.Models.Advice]":
                        IEnumerable<Advice> adviceItems = (IEnumerable<Advice>)data;
                        GetRelatedValues(adviceItems.Count());
                        curr_page_data = adviceItems.ToList().GetRange(start_item_index, end_item_index - start_item_index);
                        break;
            }
        }
    }
}