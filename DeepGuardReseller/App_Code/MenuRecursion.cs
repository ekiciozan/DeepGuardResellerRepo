using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace TermalVadiWebApp.MenuRecursion
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParenetId { get; set; }
        public string Url { get; set; }
    }

    public class MenuRecursion
    {
       public  List<Menu> allMenuItems;
        public const string OPEN_LIST_TAG = "<ul>";
        public const string CLOSE_LIST_TAG = "</ul>";
        public const string OPEN_LIST_ITEM_TAG = "<li>";
        public const string CLOSE_LIST_ITEM_TAG = "</li>";

        public MenuRecursion()
        {
           // allMenuItems = GetMenuItems();
        }
        public List<Menu> GetMenuItems()
        {
            List<Menu> MenuItmes = new List<Menu>();
            Menu item1 = new Menu { Id = 1, Name = "Item1" };
            Menu item2 = new Menu { Id = 2, Name = "Item2" };
            Menu item3 = new Menu { Id = 3, Name = "Item2_1", ParenetId = 2 };
            Menu item4 = new Menu { Id = 4, Name = "Item2_2", ParenetId = 2 };
            Menu item5 = new Menu { Id = 5, Name = "Item2_2_1", ParenetId = 4 };
            Menu item6 = new Menu { Id = 6, Name = "Item2_2_2", ParenetId = 4 };
            Menu item7 = new Menu { Id = 7, Name = "Item2_2_1_1", ParenetId = 5 };
            Menu item8 = new Menu { Id = 8, Name = "Item1_1", ParenetId = 1 };

            MenuItmes.Add(item1);
            MenuItmes.Add(item2);
            MenuItmes.Add(item3);
            MenuItmes.Add(item4);
            MenuItmes.Add(item5);
            MenuItmes.Add(item6);
            MenuItmes.Add(item7);
            MenuItmes.Add(item8);

            return MenuItmes;

        }

        public string GenerateMenuUi()
        {
            var strBuilder = new StringBuilder();
            List<Menu> parentItems = (from a in allMenuItems where a.ParenetId == 0 select a).ToList();
            strBuilder.Append(OPEN_LIST_TAG);
            foreach (var parentcat in parentItems)
            {
                strBuilder.Append(OPEN_LIST_ITEM_TAG);
                strBuilder.Append(parentcat.Name);
                List<Menu> childItems = (from a in allMenuItems where a.ParenetId == parentcat.Id select a).ToList();
                if (childItems.Count > 0)
                    AddChildItem(parentcat, strBuilder);
                strBuilder.Append(CLOSE_LIST_ITEM_TAG);
            }
            strBuilder.Append(CLOSE_LIST_TAG);
            return strBuilder.ToString();
        }

        private void AddChildItem(Menu childItem, StringBuilder strBuilder)
        {
            strBuilder.Append(OPEN_LIST_TAG);
            List<Menu> childItems = (from a in allMenuItems where a.ParenetId == childItem.Id select a).ToList();
            foreach (Menu cItem in childItems)
            {
                strBuilder.Append(OPEN_LIST_ITEM_TAG);
                strBuilder.Append(cItem.Name);
                List<Menu> subChilds = (from a in allMenuItems where a.ParenetId == cItem.Id select a).ToList();
                if (subChilds.Count > 0)
                {
                    AddChildItem(cItem, strBuilder);
                }
                strBuilder.Append(CLOSE_LIST_ITEM_TAG);
            }
            strBuilder.Append(CLOSE_LIST_TAG);
        }
    }
}