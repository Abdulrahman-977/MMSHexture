using Core.Helper;
using Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Unity;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    [Models.AuthorizeUser("Admin,SystemDirector")]
    public class DeviceController : Controller
    {
        [Dependency]
        public Core.Interfaces.IExternalServices.IUserRepository _userRepository { get; set; }
        [Dependency]
        public Core.Interfaces.IExternalServices.IDeviceRepository _deviceRepository { get; set; }
        [Dependency]
        public Core.Interfaces.IExternalServices.IRoomRepository _roomRepository { get; set; }
        [Dependency]
        public Core.Interfaces.IExternalServices.IMeetingRepository _meetingRepository { get; set; }
        public async Task<ActionResult> Index()
        {
            //var list = await _deviceRepository.GetAll();
            
            //ViewBag.Users = JsonConvert.SerializeObject(await _userRepository.GetAll());
            //List<DeviceViewModel> devices = new List<DeviceViewModel>();
            //foreach(var device in list)
            //{
            //    var newmodel = new DeviceViewModel();
            //    newmodel.deviceId = device.deviceId;
            //    newmodel.roomName = await _roomRepository.GetNameById(device.roomId);
            //    newmodel.ipAddress = device.ipAddress;
            //    newmodel.isTouchScreen = device.isTouchScreen;
            //    newmodel.roomId = device.roomId;
            //    newmodel.userId = device.userId;
            //    newmodel.UserName = (await _userRepository.GetById(device.userId)).fullname;
            //    newmodel.isActive = device.isActive;
            //    devices.Add(newmodel);
            //}
            ViewBag.Rooms = await _roomRepository.GetAll();
            ViewBag.Meetings = await _meetingRepository.GetAll();
            
            return View(new List<DeviceViewModel>());
        }
        public async Task<ActionResult> RoomsDevices()
        {
            //var list = await _deviceRepository.GetAll();

            //ViewBag.Users = JsonConvert.SerializeObject(await _userRepository.GetAll());
            //List<DeviceViewModel> devices = new List<DeviceViewModel>();
            //foreach (var device in list)
            //{
            //    var newmodel = new DeviceViewModel();
            //    newmodel.deviceId = device.deviceId;
            //    newmodel.roomName = await _roomRepository.GetNameById(device.roomId);
            //    newmodel.ipAddress = device.ipAddress;
            //    newmodel.isTouchScreen = device.isTouchScreen;
            //    newmodel.roomId = device.roomId;
            //    newmodel.userId = device.userId;
            //    newmodel.UserName = (await _userRepository.GetById(device.userId)).fullname;
            //    newmodel.isActive = device.isActive;
            //    devices.Add(newmodel);
            //}
            ViewBag.Rooms = await _roomRepository.GetAll();
            return View();
        }
        public async Task<ActionResult> Add()
        {
            ViewBag.Users = await _userRepository.GetAll();
            ViewBag.Rooms = await _roomRepository.GetAll();
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> AddDevice(List<Device> list,int meetingId)
        {
            try
            {
                foreach (var device in list)
                {
                    if(device.deviceId == 0)
                    {
                        var response = await _deviceRepository.Add(device);
                    }
                    else
                    {
                        var response = await _deviceRepository.Update(device);
                    }
                        
                }
                List<Device> listd = new List<Device>();
                var meetingroom = (await _meetingRepository.GetById(meetingId)).roomId;
                var meetingroomname = await _roomRepository.GetNameById((int)meetingroom);
                var roommodel = new RoomDropDownViewModel { Id = (int)meetingroom, name = meetingroomname };
                listd = (await _deviceRepository.GetAll()).Where(x => x.roomId == meetingroom).ToList();
                List<DeviceViewModel> devices = new List<DeviceViewModel>();
                foreach (var device in listd)
                {
                    var newmodel = new DeviceViewModel();
                    newmodel.deviceId = device.deviceId;
                    newmodel.roomName = await _roomRepository.GetNameById(device.roomId);
                    newmodel.ipAddress = device.ipAddress;
                    newmodel.isTouchScreen = device.isTouchScreen;
                    newmodel.roomId = device.roomId;
                    newmodel.userId = device.userId;
                    newmodel.UserName = (await _userRepository.GetById(device.userId)).fullname;
                    newmodel.isActive = device.isActive;
                    devices.Add(newmodel);
                }
                return PartialView("_DevicesTable", new Tuple<List<DeviceViewModel>, RoomDropDownViewModel>(devices, roommodel)); ;
            }catch(Exception ex)
            {
                return Json(false);
            }
            
        }
        [HttpPost]
        public async Task<ActionResult> AddRoomDevice(List<Device> list, int roomId)
        {
            try
            {
                foreach (var device in list)
                {
                    if (device.deviceId == 0)
                    {
                        var response = await _deviceRepository.Add(device);
                    }
                    else
                    {
                        var response = await _deviceRepository.Update(device);
                    }

                }
                if(roomId != 0) { 
                List<Device> listd = new List<Device>();
                var meetingroom = roomId;
                    var meetingroomdata = await _roomRepository.GetById((int)meetingroom);
                    var roommodel = new RoomDropDownViewModel { Id = (int)meetingroom, name = meetingroomdata.roomName, DevicesCount = meetingroomdata.capacity };
                    listd = (await _deviceRepository.GetAll()).Where(x => x.roomId == meetingroom).ToList();
                List<DeviceViewModel> devices = new List<DeviceViewModel>();
                foreach (var device in listd)
                {
                    var newmodel = new DeviceViewModel();
                    newmodel.deviceId = device.deviceId;
                    newmodel.roomName = await _roomRepository.GetNameById(device.roomId);
                    newmodel.ipAddress = device.ipAddress;
                    newmodel.isTouchScreen = device.isTouchScreen;
                    newmodel.roomId = device.roomId;
                    newmodel.userId = device.userId;
                    newmodel.UserName = (await _userRepository.GetById(device.userId)).fullname;
                    newmodel.isActive = device.isActive;
                    devices.Add(newmodel);
                }
                return PartialView("_RoomDevicesTable", new Tuple<List<DeviceViewModel>, RoomDropDownViewModel>(devices, roommodel));
                }
                return RedirectToAction("RoomsDevices");
            }
            catch (Exception ex)
            {
                return Json(false);
            }

        }
        [HttpPost]
        public async Task<ActionResult> Add(Device model)
        {
            var response = await _deviceRepository.Add(model);
            if (response.isSuccess == true)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        
        public async Task<ActionResult> Edit(int Id)
        {
            var model = await _deviceRepository.GetById(Id);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int BoardId, Device model)
        {

            var response = await _deviceRepository.Update(model);
            if (response.isSuccess == true)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<ActionResult> Delete(int Id)
        {
            var model = await _deviceRepository.Delete(Id);
            if (model)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Edit");
        }
        [Models.AuthorizeUser("Admin,SystemDirector,Director")]
        [HttpPost]
        public async Task<ActionResult> Search( int roomId, int count)
        {
            List<Device> list = new List<Device>();
            list = (await _deviceRepository.GetAll()).Where(x => x.roomId == roomId).Take(count).ToList();
            var meetingroomdata = await _roomRepository.GetById((int)roomId);
            var roommodel = new RoomDropDownViewModel { Id = (int)roomId, name = meetingroomdata.roomName,DevicesCount = meetingroomdata.capacity };
            //List<DeviceViewModel> devices = new List<DeviceViewModel>();
            //foreach (var device in list)
            //{
            //    var newmodel = new DeviceViewModel();
            //    newmodel.deviceId = device.deviceId;
            //    newmodel.roomName = await _roomRepository.GetNameById(device.roomId);
            //    newmodel.ipAddress = device.ipAddress;
            //    newmodel.isTouchScreen = device.isTouchScreen;
            //    newmodel.roomId = device.roomId;
            //    newmodel.userId = device.userId;
            //    newmodel.UserName = (await _userRepository.GetById(device.userId)).fullname;
            //    newmodel.isActive = device.isActive;
            //    devices.Add(newmodel);
            //}
            return PartialView("_DevicesTable", new Tuple<List<Device>, RoomDropDownViewModel>(list, roommodel));
        }
        [HttpPost]
        public async Task<ActionResult> SearchRoomDevices( int roomId)
        {
            List<Device> list = new List<Device>();
            var meetingroom = roomId;
            var meetingroomdata = await _roomRepository.GetById((int)meetingroom);
            var roommodel = new RoomDropDownViewModel { Id = (int)meetingroom, name = meetingroomdata.roomName, DevicesCount = meetingroomdata.capacity };
            list = (await _deviceRepository.GetAll()).Where(x => x.roomId == meetingroom).ToList();
            List<DeviceViewModel> devices = new List<DeviceViewModel>();
            foreach (var device in list)
            {
                var newmodel = new DeviceViewModel();
                newmodel.deviceId = device.deviceId;
                newmodel.roomName = await _roomRepository.GetNameById(device.roomId);
                newmodel.ipAddress = device.ipAddress;
                newmodel.isTouchScreen = device.isTouchScreen;
                newmodel.roomId = device.roomId;
                newmodel.userId = device.userId;
                newmodel.UserName = (await _userRepository.GetById(device.userId)).fullname;
                newmodel.isActive = device.isActive;
                devices.Add(newmodel);
            }
            return PartialView("_RoomDevicesTable",new Tuple<List<DeviceViewModel>, RoomDropDownViewModel>(devices, roommodel));
        }
    }
}