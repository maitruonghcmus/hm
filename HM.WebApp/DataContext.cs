using HM.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HM.WebApp
{
    public class DataContext
    {
        public static DataContext Instance = new DataContext();

        #region Hotel

        public int GetLoggedHotelId()
        {
            var user = this.GetUser(this.GetLoggedUserId());
            return user != null ? user.HotelId : -1;
        }

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
            h.CreatedBy = this.GetLoggedUserId();
            h.CreatedOn = DateTime.Now;

            return HttpClientHelper.Instance.Post<Hotel>(ApiUtils.HOTEL, ApiUtils.CREATE, h).IsSuccess();
        }

        public bool UpdateHotel(Hotel h)
        {
            h.ModifiedBy = this.GetLoggedUserId();
            h.ModifiedOn = DateTime.Now;

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

        public int GetLoggedUserId()
        {
            //TODO: Truongmv
            return 1;
        }

        public IEnumerable<User> GetUsers()
        {
            var us = HttpClientHelper.Instance.GetObjects<IEnumerable<User>>(ApiUtils.USER, ApiUtils.GETALL);
            return us != null ? us.Where(a => !a.Inactive && a.HotelId == this.GetLoggedHotelId()).Select(a => a) : null;
        }

        public User GetUser(int id)
        {
            var u = HttpClientHelper.Instance.GetObject<User>(ApiUtils.USER, ApiUtils.GETBYID, id);
            return u != null && !u.Inactive ? u : null;
        }

        public bool CreateUser(User u)
        {
            u.Id = 0;
            u.HotelId = GetLoggedHotelId();
            u.CreatedBy = GetLoggedUserId();
            u.CreatedOn = DateTime.Now;

            return HttpClientHelper.Instance.Post<User>(ApiUtils.USER, ApiUtils.CREATE, u).IsSuccess();
        }

        public bool UpdateUser(User u)
        {
            u.ModifiedBy = GetLoggedUserId();
            u.ModifiedOn = DateTime.Now;

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
                return roomtypes.Where(a => !a.Inactive && a.HotelId == this.GetLoggedHotelId()).Select(a => a);
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
            roomType.HotelId = GetLoggedHotelId();
            roomType.CreatedBy = GetLoggedUserId();
            roomType.CreatedOn = DateTime.Now;

            return HttpClientHelper.Instance.Post<RoomType>(ApiUtils.ROOMTYPE, ApiUtils.CREATE, roomType).IsSuccess();
        }

        public bool UpdateRoomType(RoomType roomType)
        {
            roomType.ModifiedBy = GetLoggedUserId();
            roomType.ModifiedOn = DateTime.Now;

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
                return rooms.Where(a => !a.Inactive && a.HotelId == this.GetLoggedHotelId()).Select(a => a);
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
            room.HotelId = GetLoggedHotelId();
            room.CreatedBy = GetLoggedUserId();
            room.CreatedOn = DateTime.Now;

            return HttpClientHelper.Instance.Post<Room>(ApiUtils.ROOM, ApiUtils.CREATE, room).IsSuccess();
        }

        public bool UpdateRoom(Room room)
        {
            room.ModifiedBy = GetLoggedUserId();
            room.ModifiedOn = DateTime.Now;

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
                return types.Where(a => !a.Inactive && a.HotelId == this.GetLoggedHotelId()).Select(a => a);
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
            type.HotelId = GetLoggedHotelId();
            type.CreatedBy = GetLoggedUserId();
            type.CreatedOn = DateTime.Now;

            return HttpClientHelper.Instance.Post<CustomerType>(ApiUtils.CUSTOMERTYPE, ApiUtils.CREATE, type).IsSuccess();
        }

        public bool UpdateCustomerType(CustomerType type)
        {
            type.ModifiedBy = GetLoggedUserId();
            type.ModifiedOn = DateTime.Now;

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
            return customers != null ? customers.Where(a => !a.Inactive && a.HotelId == this.GetLoggedHotelId()).Select(a => a) : null;
        }

        public Customer GetCustomer(int id)
        {
            var customer = HttpClientHelper.Instance.GetObject<Customer>(ApiUtils.CUSTOMER, ApiUtils.GETBYID, id);
            return customer != null && !customer.Inactive ? customer : null;
        }

        public bool CreateCustomer(Customer customer)
        {
            customer.Id = 0;
            customer.HotelId = GetLoggedHotelId();
            customer.CreatedBy = GetLoggedUserId();
            customer.CreatedOn = DateTime.Now;

            return HttpClientHelper.Instance.Post<Customer>(ApiUtils.CUSTOMER, ApiUtils.CREATE, customer).IsSuccess();
        }

        public bool UpdateCustomer(Customer customer)
        {
            customer.ModifiedBy = GetLoggedUserId();
            customer.ModifiedOn = DateTime.Now;

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
            return svs != null ? svs.Where(a => !a.Inactive && a.HotelId == this.GetLoggedHotelId()).Select(a => a) : null;
        }

        public ExtraService GetExtraService(int id)
        {
            var sv = HttpClientHelper.Instance.GetObject<ExtraService>(ApiUtils.EXTRASERVICE, ApiUtils.GETBYID, id);
            return sv != null && !sv.Inactive ? sv : null;
        }

        public bool CreateExtraService(ExtraService sv)
        {
            sv.Id = 0;
            sv.HotelId = GetLoggedHotelId();
            sv.CreatedBy = GetLoggedUserId();
            sv.CreatedOn = DateTime.Now;

            return HttpClientHelper.Instance.Post<ExtraService>(ApiUtils.EXTRASERVICE, ApiUtils.CREATE, sv).IsSuccess();
        }

        public bool UpdateExtraService(ExtraService sv)
        {
            sv.ModifiedBy = GetLoggedUserId();
            sv.ModifiedOn = DateTime.Now;

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
            return ords != null ? ords.Where(a => !a.Inactive && a.HotelId == this.GetLoggedHotelId()).Select(a => a) : null;
        }

        public Order GetOrder(int id)
        {
            var ord = HttpClientHelper.Instance.GetObject<Order>(ApiUtils.ORDER, ApiUtils.GETBYID, id);
            return ord != null && !ord.Inactive ? ord : null;
        }

        public bool CreateOrder(Order ord)
        {
            ord.Id = 0;
            ord.HotelId = GetLoggedHotelId();
            ord.CreatedBy = GetLoggedUserId();
            ord.CreatedOn = DateTime.Now;

            return HttpClientHelper.Instance.Post<Order>(ApiUtils.ORDER, ApiUtils.CREATE, ord).IsSuccess();
        }

        public bool UpdateOrder(Order ord)
        {
            ord.ModifiedBy = GetLoggedUserId();
            ord.ModifiedOn = DateTime.Now;

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
            return pms != null ? pms.Where(a => !a.Inactive && a.HotelId == this.GetLoggedHotelId()).Select(a => a) : null;
        }

        public Payment GetPayment(int id)
        {
            var pm = HttpClientHelper.Instance.GetObject<Payment>(ApiUtils.PAYMENT, ApiUtils.GETBYID, id);
            return pm != null && !pm.Inactive ? pm : null;
        }

        public bool CreatePayment(Payment pm)
        {
            pm.Id = 0;
            pm.HotelId = GetLoggedHotelId();
            pm.CreatedBy = GetLoggedUserId();
            pm.CreatedOn = DateTime.Now;

            return HttpClientHelper.Instance.Post<Payment>(ApiUtils.PAYMENT, ApiUtils.CREATE, pm).IsSuccess();
        }

        public bool UpdatePayment(Payment pm)
        {
            pm.ModifiedBy = GetLoggedUserId();
            pm.ModifiedOn = DateTime.Now;

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