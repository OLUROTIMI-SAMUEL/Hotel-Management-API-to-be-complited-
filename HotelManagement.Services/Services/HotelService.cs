﻿using AutoMapper;
using HotelManagement.Core;
using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using HotelManagement.Core.IRepositories;
using HotelManagement.Core.IServices;
using System.Collections.Generic;

namespace HotelManagement.Services.Services
{
    public class HotelService : IHotelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HotelService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<GetHotelsDto>> GetHotelById(string Id)
        {
            var getHotel = await _unitOfWork.hotelRepository.GetByIdAsync(x => x.Id == Id);
            var mappedHotel = _mapper.Map<GetHotelsDto>(getHotel);
            if (mappedHotel == null)
            {
                return new Response<GetHotelsDto>
                {
                    StatusCode = 404,
                    Succeeded = false,
                    Data = null,
                    Message = "Hotel not fund"
                };
            }
            return new Response<GetHotelsDto>
            {
                StatusCode = 202,
                Succeeded = true,
                Data = mappedHotel,
                Message = "Successful"
            };
        }

        public async Task<Response<List<GetHotelsDto>>> GetHotels()
        {
            var hotels = await _unitOfWork.hotelRepository.GetAllAsync();
            var allHotels = _mapper.Map<List<GetHotelsDto>>(hotels);
            if (allHotels.Count == 0)
            {
                return new Response<List<GetHotelsDto>>
                {
                    StatusCode = 404,
                    Succeeded = false,
                    Data = null,
                    Message = "Hotels not found"
                };
            }
            return new Response<List<GetHotelsDto>>
            {
                StatusCode = 202,
                Succeeded = true,
                Data = allHotels,
                Message = "Successful"
            };
        }

        public async Task<Response<UpdateHotelDto>> UpdateHotel(UpdateHotelDto update, string Id)
        {
            var updateHotel = await _unitOfWork.hotelRepository.GetByIdAsync(x => x.Id == Id);
            var mappedUpdate = _mapper.Map(update, updateHotel);

            if (updateHotel == null)
            {
                return new Response<UpdateHotelDto>
                {
                    StatusCode = 404,
                    Succeeded = false,
                    Data = null,
                    Message = "Hotel not found"
                };
            }
            _unitOfWork.SaveChanges();
            return Response<UpdateHotelDto>.Success("Updated Successfully", update);
        }
        public async Task<Response<List<GetHotelByRatingsDto>>> GetHotelRating(string HotelName)
        {
            try
            {
                var hotelRatings = _unitOfWork.hotelRepository.GetByIdAsync(x => x.Name == HotelName).Result.Ratings;
                var mappedHotelRating = _mapper.Map<List<GetHotelByRatingsDto>>(hotelRatings);

                if (mappedHotelRating == null) return Response<List<GetHotelByRatingsDto>>.Fail($"Hotel with {HotelName} Not Found");
                return Response<List<GetHotelByRatingsDto>>.Success(HotelName, mappedHotelRating);
            }
            catch (Exception ex)
            {

                return Response<List<GetHotelByRatingsDto>>.Fail(ex.Message);
            }
        }

        public async Task<Response<List<GetRoomDto>>> GetRoomsByAvailability(string HotelNmae, string RoomType)
        {
            try
            {
                var roomsByAvailability = _unitOfWork.hotelRepository.GetByIdAsync(x => x.Name == HotelNmae)
                .Result.RoomTypes.Where(x => x.Name == RoomType).SelectMany(x => x.Rooms);
                var rooms = roomsByAvailability.Where(x => x.IsBooked == false).Select(x => x);
                var data = _mapper.Map<List<GetRoomDto>>(rooms);
                if (data == null) return Response<List<GetRoomDto>>.Fail($"{HotelNmae} Has No Room Available For {RoomType} RoomType");
                return Response<List<GetRoomDto>>.Success(HotelNmae, data);
            }
            catch (Exception ex)
            {

                return Response<List<GetRoomDto>>.Fail(ex.Message);
            }
           
        }

        public async Task<Response<string>> DeleteHotelById(string id)
        {
            try
            {
                var hotelTodelete = _unitOfWork.hotelRepository.DeleteAsync<string>(id);
                if (hotelTodelete == null)
                    return Response<string>.Fail($"Hotel with {id} doesnot exist");
                _unitOfWork.SaveChanges();
                return Response<string>.Success($"Hotel with {id} Sucessful Deleted", id);
 
    }
            catch (Exception ex)
            {

                return Response<string>.Fail(ex.Message);
            };
           
        }
    }
}

       