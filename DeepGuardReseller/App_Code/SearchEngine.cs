//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace TermalVadiWebApp.App_Start
//{
//    public static class SearchEngine
//    {
//        public static string Build(List<SearchObject> searchObjects)
//        {
//            string temp = "";
//            foreach (SearchObject item in searchObjects)
//            {
//                if(string.IsNullOrWhiteSpace(item.columnName))
//                    throw new Exception("Column name Değişkeni boş veya null!"); 
//                if(item is BettweenNumber)
//                {
//                    BettweenNumber btwnItem = ((BettweenNumber)item);
//                    if (btwnItem.val1 == 0 && btwnItem.val2 == 0) continue;

//                    if (btwnItem.val1==0)
//                    {
                        
//                    }
//                    else if (btwnItem.val2 == 0)
//                    {

//                    }
                    
//                }
//            }
//        }
//    }
//    public class SearchObject
//    {
//        public string columnName="";
//    }
//    public class BettweenDate : SearchObject
//    {
//        public DateTime val1;
//        public DateTime val2;
//    }
//    public class BettweenNumber : SearchObject
//    {
//        public float val1;
//        public float val2;
//    }
//    public class Contains : SearchObject
//    {
//        public string value = "";
//    }
//}