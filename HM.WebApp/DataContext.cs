using HM.DataModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HM.WebApp
{
    public class DataContext
    {
        public static DataContext Instance = new DataContext();

        #region Hotel

        public IEnumerable<Hotel> GetHotels()
        {
            var hotels = HttpClientHelper.Instance.GetObjects<IEnumerable<Hotel>>(ApiUtils.HOTEL, ApiUtils.GETALL);
            return hotels != null ? hotels.Where(a => !a.Inactive).Select(a => a) : null;
        }

        public Hotel GetHotel(int id)
        {
            var hotel = HttpClientHelper.Instance.GetObject<Hotel>(ApiUtils.HOTEL, ApiUtils.GETBYID, id);
            return hotel != null && !hotel.Inactive ? hotel : null;
        }

        public bool CreateHotel(Hotel h)
        {
            h.Id = 0;
            h.CreatedBy = AppContext.Instance.GetLoggedUserId();
            h.CreatedOn = DateTime.Now;

            if (!AppUtils.IsValidObject(h))
                return false;

            return HttpClientHelper.Instance.Post<Hotel>(ApiUtils.HOTEL, ApiUtils.CREATE, h).IsSuccess();
        }

        public bool UpdateHotel(Hotel h)
        {
            h.ModifiedBy = AppContext.Instance.GetLoggedUserId();
            h.ModifiedOn = DateTime.Now;

            if (!AppUtils.IsValidObject(h))
                return false;

            return HttpClientHelper.Instance.Post<Hotel>(ApiUtils.HOTEL, ApiUtils.UPDATE, h).IsSuccess();
        }

        public bool DeleteHotel(int id)
        {
            var roomTypeCount = DataContext.Instance.GetRoomTypes()?.Where(a => a.HotelId == id)?.Count() ?? 0;
            var userCount = DataContext.Instance.GetUsers()?.Where(a => a.HotelId == id)?.Count() ?? 0;

            if (roomTypeCount == 0 && userCount == 0)
                return HttpClientHelper.Instance.Delete<Hotel>(ApiUtils.HOTEL, ApiUtils.DELETE, id).IsSuccess();

            return false;
        }
        #endregion

        #region User

        public IEnumerable<User> GetUsers()
        {
            var us = HttpClientHelper.Instance.GetObjects<IEnumerable<User>>(ApiUtils.USER, ApiUtils.GETALL);
            return us != null ? us.Where(a => !a.Inactive && a.HotelId == AppContext.Instance.GetLoggedHotelId()).Select(a => a) : null;
        }

        public User GetUser(int id)
        {
            var u = HttpClientHelper.Instance.GetObject<User>(ApiUtils.USER, ApiUtils.GETBYID, id);
            return u != null && !u.Inactive ? u : null;
        }

        public bool CreateUser(User u)
        {
            u.Id = 0;
            u.HotelId = AppContext.Instance.GetLoggedHotelId();
            u.CreatedBy = AppContext.Instance.GetLoggedUserId();
            u.CreatedOn = DateTime.Now;

            var passwordHashed = System.Text.Encoding.UTF8.GetBytes(u.Password);
            u.Password = Convert.ToBase64String(passwordHashed);

            if (!AppUtils.IsValidObject(u))
                return false;

            return HttpClientHelper.Instance.Post<User>(ApiUtils.USER, ApiUtils.CREATE, u).IsSuccess();
        }

        public bool UpdateUser(User u)
        {
            u.ModifiedBy = AppContext.Instance.GetLoggedUserId();
            u.ModifiedOn = DateTime.Now;

            if (!AppUtils.IsValidObject(u))
                return false;

            return HttpClientHelper.Instance.Post<User>(ApiUtils.USER, ApiUtils.UPDATE, u).IsSuccess();
        }

        public bool DeleteUser(int id)
        {
            return HttpClientHelper.Instance.Delete<User>(ApiUtils.USER, ApiUtils.DELETE, id).IsSuccess();
        }

        #endregion

        #region RoomType
        public IEnumerable<RoomType> GetRoomTypes()
        {
            var roomtypes = HttpClientHelper.Instance.GetObjects<IEnumerable<RoomType>>(ApiUtils.ROOMTYPE, ApiUtils.GETALL);

            if (roomtypes != null)
            {
                return roomtypes.Where(a => !a.Inactive && a.HotelId == AppContext.Instance.GetLoggedHotelId()).Select(a => a);
            }
            return null;
        }

        public RoomType GetRoomType(int id)
        {
            var room = HttpClientHelper.Instance.GetObject<RoomType>(ApiUtils.ROOMTYPE, ApiUtils.GETBYID, id);
            return room != null && !room.Inactive ? room : null;
        }

        public bool CreateRoomType(RoomType roomType)
        {
            roomType.Id = 0;
            roomType.HotelId = AppContext.Instance.GetLoggedHotelId();
            roomType.CreatedBy = AppContext.Instance.GetLoggedUserId();
            roomType.CreatedOn = DateTime.Now;

            if (!AppUtils.IsValidObject(roomType))
                return false;

            return HttpClientHelper.Instance.Post<RoomType>(ApiUtils.ROOMTYPE, ApiUtils.CREATE, roomType).IsSuccess();
        }

        public bool UpdateRoomType(RoomType roomType)
        {
            roomType.ModifiedBy = AppContext.Instance.GetLoggedUserId();
            roomType.ModifiedOn = DateTime.Now;

            if (!AppUtils.IsValidObject(roomType))
                return false;

            return HttpClientHelper.Instance.Post<RoomType>(ApiUtils.ROOMTYPE, ApiUtils.UPDATE, roomType).IsSuccess();
        }

        public bool DeleteRoomType(int id)
        {
            var acceptToDelete = DataContext.Instance.GetRooms()?.Where(a => a.RoomTypeId == id)?.Count() == 0;

            if (acceptToDelete)
                return HttpClientHelper.Instance.Delete<RoomType>(ApiUtils.ROOMTYPE, ApiUtils.DELETE, id).IsSuccess();

            return false;
        }
        #endregion

        #region Room
        public IEnumerable<Room> GetRooms()
        {
            var rooms = HttpClientHelper.Instance.GetObjects<IEnumerable<Room>>(ApiUtils.ROOM, ApiUtils.GETALL);
            if (rooms != null)
            {
                return rooms.Where(a => !a.Inactive && a.HotelId == AppContext.Instance.GetLoggedHotelId()).Select(a => a);
            }
            return null;
        }

        public Room GetRoom(int id)
        {
            var room = HttpClientHelper.Instance.GetObject<Room>(ApiUtils.ROOM, ApiUtils.GETBYID, id);
            return room != null && !room.Inactive ? room : null;
        }

        public bool CreateRoom(Room room)
        {
            room.Id = 0;
            room.HotelId = AppContext.Instance.GetLoggedHotelId();
            room.CreatedBy = AppContext.Instance.GetLoggedUserId();
            room.CreatedOn = DateTime.Now;

            if (!AppUtils.IsValidObject(room))
                return false;

            return HttpClientHelper.Instance.Post<Room>(ApiUtils.ROOM, ApiUtils.CREATE, room).IsSuccess();
        }

        public bool UpdateRoom(Room room)
        {
            room.ModifiedBy = AppContext.Instance.GetLoggedUserId();
            room.ModifiedOn = DateTime.Now;

            if (!AppUtils.IsValidObject(room))
                return false;

            return HttpClientHelper.Instance.Post<Room>(ApiUtils.ROOM, ApiUtils.UPDATE, room).IsSuccess();
        }

        public bool DeleteRoom(int id)
        {
            var acceptToDelete = DataContext.Instance.GetRoom(id)?.Status == 1;

            if (!acceptToDelete)
                return HttpClientHelper.Instance.Delete<Room>(ApiUtils.ROOM, ApiUtils.DELETE, id).IsSuccess();

            return false;
        }
        #endregion

        #region Customer Type
        public IEnumerable<CustomerType> GetCustomerTypes()
        {
            var types = HttpClientHelper.Instance.GetObjects<IEnumerable<CustomerType>>(ApiUtils.CUSTOMERTYPE, ApiUtils.GETALL);
            if (types != null)
            {
                return types.Where(a => !a.Inactive && a.HotelId == AppContext.Instance.GetLoggedHotelId()).Select(a => a);
            }
            return null;
        }

        public CustomerType GetCustomerType(int id)
        {
            var type = HttpClientHelper.Instance.GetObject<CustomerType>(ApiUtils.CUSTOMERTYPE, ApiUtils.GETBYID, id);
            return type != null && !type.Inactive ? type : null;
        }

        public bool CreateCustomerType(CustomerType type)
        {
            type.Id = 0;
            type.HotelId = AppContext.Instance.GetLoggedHotelId();
            type.CreatedBy = AppContext.Instance.GetLoggedUserId();
            type.CreatedOn = DateTime.Now;

            if (!AppUtils.IsValidObject(type))
                return false;

            return HttpClientHelper.Instance.Post<CustomerType>(ApiUtils.CUSTOMERTYPE, ApiUtils.CREATE, type).IsSuccess();
        }

        public bool UpdateCustomerType(CustomerType type)
        {
            type.ModifiedBy = AppContext.Instance.GetLoggedUserId();
            type.ModifiedOn = DateTime.Now;

            if (!AppUtils.IsValidObject(type))
                return false;

            return HttpClientHelper.Instance.Post<CustomerType>(ApiUtils.CUSTOMERTYPE, ApiUtils.UPDATE, type).IsSuccess();
        }

        public bool DeleteCustomerType(int id)
        {
            var acceptToDelete = DataContext.Instance.GetCustomers()?.Where(a => a.CustomerTypeId == id)?.Count() == 0;

            if (acceptToDelete)
                return HttpClientHelper.Instance.Delete<CustomerType>(ApiUtils.CUSTOMERTYPE, ApiUtils.DELETE, id).IsSuccess();

            return false;
        }
        #endregion

        #region Customer
        public IEnumerable<Customer> GetCustomers()
        {
            var customers = HttpClientHelper.Instance.GetObjects<IEnumerable<Customer>>(ApiUtils.CUSTOMER, ApiUtils.GETALL);
            return customers != null ? customers.Where(a => !a.Inactive && a.HotelId == AppContext.Instance.GetLoggedHotelId()).Select(a => a) : null;
        }

        public Customer GetCustomer(int id)
        {
            var customer = HttpClientHelper.Instance.GetObject<Customer>(ApiUtils.CUSTOMER, ApiUtils.GETBYID, id);
            return customer != null && !customer.Inactive ? customer : null;
        }

        public bool CreateCustomer(Customer customer)
        {
            customer.Id = 0;
            customer.HotelId = AppContext.Instance.GetLoggedHotelId();
            customer.CreatedBy = AppContext.Instance.GetLoggedUserId();
            customer.CreatedOn = DateTime.Now;

            if (!AppUtils.IsValidObject(customer))
                return false;

            return HttpClientHelper.Instance.Post<Customer>(ApiUtils.CUSTOMER, ApiUtils.CREATE, customer).IsSuccess();
        }

        public bool UpdateCustomer(Customer customer)
        {
            customer.ModifiedBy = AppContext.Instance.GetLoggedUserId();
            customer.ModifiedOn = DateTime.Now;

            if (!AppUtils.IsValidObject(customer))
                return false;

            return HttpClientHelper.Instance.Post<Customer>(ApiUtils.CUSTOMER, ApiUtils.UPDATE, customer).IsSuccess();
        }

        public bool DeleteCustomer(int id)
        {
            var acceptToDelete = DataContext.Instance.GetRooms()?.Where(a => a.CurrentCustomerId == id)?.Count() == 0;

            if (acceptToDelete)
                return HttpClientHelper.Instance.Delete<Customer>(ApiUtils.CUSTOMER, ApiUtils.DELETE, id).IsSuccess();

            return false;
        }
        #endregion

        #region Extra Service
        public IEnumerable<ExtraService> GetExtraServices()
        {
            var svs = HttpClientHelper.Instance.GetObjects<IEnumerable<ExtraService>>(ApiUtils.EXTRASERVICE, ApiUtils.GETALL);
            return svs != null ? svs.Where(a => !a.Inactive && a.HotelId == AppContext.Instance.GetLoggedHotelId()).Select(a => a) : null;
        }

        public ExtraService GetExtraService(int id)
        {
            var sv = HttpClientHelper.Instance.GetObject<ExtraService>(ApiUtils.EXTRASERVICE, ApiUtils.GETBYID, id);
            return sv != null && !sv.Inactive ? sv : null;
        }

        public bool CreateExtraService(ExtraService sv)
        {
            sv.Id = 0;
            sv.HotelId = AppContext.Instance.GetLoggedHotelId();
            sv.CreatedBy = AppContext.Instance.GetLoggedUserId();
            sv.CreatedOn = DateTime.Now;

            if (!AppUtils.IsValidObject(sv))
                return false;

            return HttpClientHelper.Instance.Post<ExtraService>(ApiUtils.EXTRASERVICE, ApiUtils.CREATE, sv).IsSuccess();
        }

        public bool UpdateExtraService(ExtraService sv)
        {
            sv.ModifiedBy = AppContext.Instance.GetLoggedUserId();
            sv.ModifiedOn = DateTime.Now;

            if (!AppUtils.IsValidObject(sv))
                return false;

            return HttpClientHelper.Instance.Post<ExtraService>(ApiUtils.EXTRASERVICE, ApiUtils.UPDATE, sv).IsSuccess();
        }

        public bool DeleteExtraService(int id)
        {
            return HttpClientHelper.Instance.Delete<ExtraService>(ApiUtils.EXTRASERVICE, ApiUtils.DELETE, id).IsSuccess();
        }
        #endregion

        #region Order
        public IEnumerable<Order> GetOrders()
        {
            var ords = HttpClientHelper.Instance.GetObjects<IEnumerable<Order>>(ApiUtils.ORDER, ApiUtils.GETALL);
            return ords != null ? ords.Where(a => !a.Inactive && a.HotelId == AppContext.Instance.GetLoggedHotelId()).Select(a => a) : null;
        }

        public Order GetOrder(int id)
        {
            var ord = HttpClientHelper.Instance.GetObject<Order>(ApiUtils.ORDER, ApiUtils.GETBYID, id);
            return ord != null && !ord.Inactive ? ord : null;
        }

        public bool CreateOrder(Order ord)
        {
            ord.Id = 0;
            ord.HotelId = AppContext.Instance.GetLoggedHotelId();
            ord.CreatedBy = AppContext.Instance.GetLoggedUserId();
            ord.CreatedOn = DateTime.Now;

            if (!AppUtils.IsValidObject(ord))
                return false;

            return HttpClientHelper.Instance.Post<Order>(ApiUtils.ORDER, ApiUtils.CREATE, ord).IsSuccess();
        }

        public bool UpdateOrder(Order ord)
        {
            ord.ModifiedBy = AppContext.Instance.GetLoggedUserId();
            ord.ModifiedOn = DateTime.Now;

            if (!AppUtils.IsValidObject(ord))
                return false;

            return HttpClientHelper.Instance.Post<Order>(ApiUtils.ORDER, ApiUtils.UPDATE, ord).IsSuccess();
        }

        public bool DeleteOrder(int id)
        {
            return HttpClientHelper.Instance.Delete<Order>(ApiUtils.ORDER, ApiUtils.DELETE, id).IsSuccess();
        }
        #endregion

        #region Payment
        public IEnumerable<Payment> GetPayments()
        {
            var pms = HttpClientHelper.Instance.GetObjects<IEnumerable<Payment>>(ApiUtils.PAYMENT, ApiUtils.GETALL);
            return pms != null ? pms.Where(a => !a.Inactive && a.HotelId == AppContext.Instance.GetLoggedHotelId()).Select(a => a) : null;
        }

        public Payment GetPayment(int id)
        {
            var pm = HttpClientHelper.Instance.GetObject<Payment>(ApiUtils.PAYMENT, ApiUtils.GETBYID, id);
            return pm != null && !pm.Inactive ? pm : null;
        }

        public bool CreatePayment(Payment pm)
        {
            pm.Id = 0;
            pm.HotelId = AppContext.Instance.GetLoggedHotelId();
            pm.CreatedBy = AppContext.Instance.GetLoggedUserId();
            pm.CreatedOn = DateTime.Now;

            if (!AppUtils.IsValidObject(pm))
                return false;

            return HttpClientHelper.Instance.Post<Payment>(ApiUtils.PAYMENT, ApiUtils.CREATE, pm).IsSuccess();
        }

        public bool UpdatePayment(Payment pm)
        {
            pm.ModifiedBy = AppContext.Instance.GetLoggedUserId();
            pm.ModifiedOn = DateTime.Now;

            if (!AppUtils.IsValidObject(pm))
                return false;

            return HttpClientHelper.Instance.Post<Payment>(ApiUtils.PAYMENT, ApiUtils.UPDATE, pm).IsSuccess();
        }

        public bool DeletePayment(int id)
        {
            return HttpClientHelper.Instance.Delete<Payment>(ApiUtils.PAYMENT, ApiUtils.DELETE, id).IsSuccess();
        }
        #endregion

        #region Role
        public Role GetRole(int id)
        {
            var role = HttpClientHelper.Instance.GetObject<Role>(ApiUtils.ROLE, ApiUtils.GETBYID, id);
            return role != null && !role.Inactive ? role : null;
        }

        public IEnumerable<Role> GetRoles()
        {
            var roles = HttpClientHelper.Instance.GetObjects<IEnumerable<Role>>(ApiUtils.ROLE, ApiUtils.GETALL);
            return roles != null ? roles.Where(a => !a.Inactive).Select(a => a) : null;
        }
        #endregion
    }
}