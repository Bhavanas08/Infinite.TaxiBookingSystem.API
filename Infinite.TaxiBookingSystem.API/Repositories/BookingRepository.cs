using Infinite.TaxiBookingSystem.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infinite.TaxiBookingSystem.API.Repositories
{
    public class BookingRepository : IRepository<Booking>,IGetRepository<BookingDto>, IBookingRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingRepository(ApplicationDbContext context)
        {
            _context = context;

        }
        public async Task Create(Booking obj)
        {
            if (obj != null)
            {
                _context.Bookings.Add(obj);
                await _context.SaveChangesAsync();
            }

        }
        public async Task<Booking> Update(int id, Booking obj)//api/BookingsUpdate/1
        {
            var bookingInDb = await _context.Bookings.FindAsync(id);
            if (bookingInDb != null)
            {
                bookingInDb.BookingDate = obj.BookingDate;
                bookingInDb.DestinationAddress = obj.DestinationAddress;
                bookingInDb.SourceAddress = obj.SourceAddress;
                bookingInDb.CustomerID = obj.CustomerID;
                bookingInDb.TaxiId = obj.TaxiId;
                _context.Bookings.Update(bookingInDb);
                await _context.SaveChangesAsync();
                return bookingInDb;



            }
            return null;
        }

        public async Task<Booking> Delete(int id)
        {
            var bookingInDb = await _context.Bookings.FindAsync(id);
            if (bookingInDb != null)
            {
                _context.Bookings.Remove(bookingInDb);
                await _context.SaveChangesAsync();
                return bookingInDb;
            }
            return null;
        }

        public IEnumerable<BookingDto> GetAll()
        {
            var bookings = _context.Bookings.Include(x => x.Customer).Select(x => new BookingDto
            {
                BookingId = x.BookingId,
                BookingDate = x.BookingDate,
                TripDate = x.TripDate,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                DestinationAddress = x.DestinationAddress,
                SourceAddress = x.SourceAddress,
                CustomerID = x.CustomerID,
                TaxiId = x.TaxiId,
                CustomerName = x.Customer.CustomerName
            }).ToList();


            return bookings;

        }

        public async Task<BookingDto> GetById(int id)
        {
            var bookings = await _context.Bookings.Include(x => x.Customer).Select(x => new BookingDto
            {
                BookingId = x.BookingId,
                BookingDate = x.BookingDate,
                TripDate = x.TripDate,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                DestinationAddress = x.DestinationAddress,
                SourceAddress = x.SourceAddress,
                CustomerID = x.CustomerID,
                TaxiId = x.TaxiId,
                CustomerName = x.Customer.CustomerName

            }).ToListAsync();

            var booking = bookings.FirstOrDefault(x => x.BookingId == id);
            if (booking != null)
            {
                return booking;
            }
            return null;
        }

        public async Task<IEnumerable<Booking>> SearchByTaxi(string taxiModel)
        {
            if (!string.IsNullOrWhiteSpace(taxiModel))
            {
                var bookings = await _context.Bookings.Include(x => x.Taxi).Where(x => x.Taxi.TaxiModel.Contains(taxiModel)).ToListAsync();
                return bookings;
            }

            return null;
        }

    }
}
