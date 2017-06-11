using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TestWebAPI.Controllers
{
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

        public Room Get(int id)
        {
            return RoomList.Rooms.Where(o => o.RoomId == id.ToString()).FirstOrDefault();
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
        public bool Delete(string id)
        {
            RoomList.Rooms.RemoveAll(i => i.RoomId == id);
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