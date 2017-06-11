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

        [Route("{roomId}")]
        public Room Get(string roomId)
        {
            return RoomList.Rooms.Where(o => o.RoomId == roomId).FirstOrDefault();
        }

        [Route("room/{roomId}/users")]
        public List<User> GetUsers(string roomId)
        {
            var roomData = RoomList.Rooms.Where(o => o.RoomId == roomId).FirstOrDefault();
            if (roomData == null) { return null; }
            else
            {
                return roomData.Users;
            }
        }

        [Route("room/{roomId}/users/{empId}")]
        public User FindUser(string roomId, string empId)
        {
            var roomData = RoomList.Rooms.Where(o => o.RoomId == roomId && o.Users.Exists(i => i.EmpId == empId)).FirstOrDefault();
            if (roomData == null) { return null; }
            else
            {
                return roomData.Users.FirstOrDefault();
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
                userList.Add(new User { EmpId = i.ToString(), Name = $"Name_{i}" });
                var room = new Room { No = i, RoomId = i.ToString(), RoomName = $"No.{i}", Users = userList };
                RoomList.Rooms.Add(room);
            }
        }
    }
}