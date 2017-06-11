using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace TestWebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 設定和服務

            // Web API 路由
            config.MapHttpAttributeRoutes();

            // routing will using the first.
            RegisterRoom(config);

            //config.Routes.MapHttpRoute("Default", "api/{controller}");
            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
        }

        public static void RegisterRoom(HttpConfiguration config)
        {
            //config.Routes.MapHttpRoute(
            //    name: "RoomUser",
            //    routeTemplate: "api/room/{roomId}/Users/{empId}",
            //    defaults: new { controller = "room", empId = RouteParameter.Optional }
            //);

            //config.Routes.MapHttpRoute(
            //    name: "Room",
            //    routeTemplate: "api/room/{roomId}",
            //    defaults: new { controller = "room", roomId = RouteParameter.Optional }
            //);
        }
    }
}