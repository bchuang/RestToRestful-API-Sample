using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TestWebAPI.Controllers
{
    [RoutePrefix("api/Room")]
    [Route("{roomId?}")]
    /// <summary> Rest WebAPI - Room </summary>
    public class RoomController : ApiController
    {
        public RoomController()
        {
            InitialRoomList();
        }

        public List<Room> Get()
        {
            return RoomList.Rooms;
        }

        public Room Get(string roomId)
        {
            return RoomList.Rooms.Where(o => o.RoomId == roomId).FirstOrDefault();
        }

        [Route("{roomId}/Users")]
        public List<User> GetUsers(string roomId)
        {
            var roomData = RoomList.Rooms.Where(o => o.RoomId == roomId).FirstOrDefault();
            if (roomData == null) { return null; }
            else
            {
                return roomData.Users;
            }
        }

        [Route("{roomId}/Users/{empId}")]
        [HttpGet]
        public User FindUser(string roomId, string empId)
        {
            var roomData = RoomList.Rooms.Where(o => o.RoomId == roomId && o.Users.Exists(i => i.EmpId == empId)).FirstOrDefault();
            if (roomData == null) { return null; }
            else
            {
                return roomData.Users.Where(o => o.EmpId == empId).FirstOrDefault();
            }
        }

        /// <summary> Create </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public bool Post(Room room)
        {
            if (room != null && RoomList.Rooms.Exists(i => i.RoomName == room.RoomName))
            { return false; }
            else
            {
                RoomList.Rooms.Add(room);
                return true;
            }
        }

        /// <summary> Update </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public bool Put([FromBody]Room room)
        {
            var roomData = RoomList.Rooms.Where(i => i.RoomId == room.RoomId).FirstOrDefault();
            if (roomData == null)
            { return false; }
            else
            {
                roomData = room;
                return true;
            }
        }

        /// <summary> Delete </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(string roomId)
        {
            RoomList.Rooms.RemoveAll(i => i.RoomId == roomId);
            return true;
        }

        /// <summary> Partial Update </summary>
        /// <param name="roomId"></param>
        /// <param name="room"></param>
        /// <returns></returns>
        public bool Patch()
        {
            throw new NotImplementedException();
        }

        private void InitialRoomList()
        {
            RoomList.Rooms = new List<Room>();
            for (var i = 1; i < 10; i++)
            {
                var userList = new List<User>();
                for (var id = i; id < 10; id++)
                {
                    userList.Add(new User { EmpId = id.ToString(), Name = $"Name_{id}" });
                }
                var room = new Room { No = i, RoomId = i.ToString(), RoomName = $"No.{i}", Users = userList };
                RoomList.Rooms.Add(room);
            }
        }
    }
}