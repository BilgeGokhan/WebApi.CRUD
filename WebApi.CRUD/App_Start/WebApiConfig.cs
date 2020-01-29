using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebApi.CRUD
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            /* Döngüsel REferans Yönetimi - Reference Loop Handling -- EF içerisinde birbirlerine bağlı tabloların loop a girmemesi için yapılabilecek işlemler .
             * 
             * 
                config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.All;

                3. bir yöntem ise Propery seviyesinde yapmak ilgili nesneye gidip Attribute ( [JsonIgnore] ) ile engelleme yapılabilir. 
            Örnek:
                public partial class Product
                {
                    public int ProductId { get; set; }

                    [JsonIgnore]
                    [IgnoreDataMember] Xml için
                    public virtual Category Category { get; set; }
                }    
                
                Referans ı korumak için ise korunacak class ın tepesine [JsonObject(IsReference = true)] yazıyoruz. Fakat bu çıktıda $ işaretli Json formatına uymayan serileştirme işlemini handle etmek gerekiyor. 

            */

            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();
            
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
